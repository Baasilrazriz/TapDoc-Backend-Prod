using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TapDoc_Mobile_App_Backend.Data;
using TapDoc_Mobile_App_Backend.Models;

namespace TapDoc_Mobile_App_Backend.Services
{
    public class PatientService
    {
        public readonly ApplicationDbContext _dbContext;
        public PatientService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PatientHomeScreenDetailsDTO> GetPatientHomeDetails(int PatientID)
        {
            var patient = _dbContext.PatientDetails.Where(x => x.PatientID == PatientID && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (patient == null)
            {
                throw new Exception("Patient not found");
            }
            var userDetails = _dbContext.Users.Where(x => x.UserID == patient.UserID && x.IsActive == true && x.IsDeleted == false).FirstOrDefault();
            if (userDetails == null)
            {
                throw new Exception("User not found");
            }

            return new PatientHomeScreenDetailsDTO
            {
                PatientID = patient.PatientID,
                UserID = patient.UserID,
                PatientName = patient.FullName,
                PatientEmail = userDetails.Email,
                PatientProfileUrl = patient.PatientImageUrl,
                PatientUpcomingAppointments = _dbContext.Appointments
                                               .Count(x => x.PatientID == PatientID && x.AppointmentStatus == AppointmentEnums.Upcoming),
                PatientCompletedAppointments = _dbContext.Appointments
                                                .Count(x => x.PatientID == PatientID && x.AppointmentStatus == AppointmentEnums.Completed),

            };
        }
        public async Task<List<HomeScreenBestRatedDoctorsDTO>> GetHomeScreenBestDoctors(int pageNo, int pageSize, string City)
        {
            var doctorRatings = await _dbContext.DoctorRatings
                .GroupBy(dr => dr.DoctorID)
                .Select(g => new
                {
                    DoctorID = g.Key,
                    TotalRating = g.Sum(dr => dr.Rating),
                    TotalReviews = g.Count(),
                    AverageRating = g.Count() > 0 ? g.Sum(dr => dr.Rating) / (double)g.Count() : 0
                })
                .OrderByDescending(x => x.AverageRating)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var DoctorIDs = doctorRatings.Select(x => x.DoctorID).ToList();

            // Declare doctors outside the if-block
            List<DoctorDetails> doctors;

            if (!string.IsNullOrWhiteSpace(City))
            {
                doctors = await _dbContext.DoctorDetails
                    .Where(d => DoctorIDs.Contains(d.DoctorID) &&
                                (d.City ?? "").ToLower() == City.ToLower())
                    .ToListAsync();
            }
            else
            {
                doctors = await _dbContext.DoctorDetails
                    .Where(d => DoctorIDs.Contains(d.DoctorID))
                    .ToListAsync();
            }

            if (doctors == null || !doctors.Any())
                return null;

            var result = doctorRatings
                .Select(rating =>
                {
                    var doctor = doctors.FirstOrDefault(d => d.DoctorID == rating.DoctorID);
                    return doctor == null ? null : new HomeScreenBestRatedDoctorsDTO
                    {
                        DoctorID = rating.DoctorID,
                        DoctorRating = rating.AverageRating,
                        DoctorExperience = doctor.TotalExperience,
                        DoctorName = doctor.FullName,
                        DoctorSpeciality = doctor.Speciality,
                        DoctorProfileUrl = doctor.Image
                    };
                })
                .Where(x => x != null)
                .ToList();

            return result.Any() ? result : null;
        }
        public async Task<List<HomeUpcomingAppointmentsDTO>> GetHomeUpcomingAppointmentDetails(int PatientID, int pageNo, int pageSize)
        {
            DateTime currentDateTime = DateTime.UtcNow;

            DateTime startOfWeek = currentDateTime.Date.AddDays(-(int)currentDateTime.DayOfWeek + (int)DayOfWeek.Monday); 
            DateTime endOfWeek = startOfWeek.AddDays(6).AddHours(23).AddMinutes(59).AddSeconds(59); 

            var upcomingAppointments = await _dbContext.Appointments
                .Where(a => a.PatientID == PatientID && a.CreatedOn >= currentDateTime.Date && a.CreatedOn <= endOfWeek && a.AppointmentStatus == AppointmentEnums.Upcoming)
                .OrderBy(a => a.CreatedOn)
                .ThenBy(a => a.AppointmentDetails.StartTime) 
                .Take(5) 
                .Select(a => new HomeUpcomingAppointmentsDTO
                {
                    AppointmentID = a.AppointmentID,
                    DoctorProfileUrl = a.Doctor.Image,
                    DoctorName = a.Doctor.FullName,
                    DoctorSpeciality = a.Doctor.Speciality,

                    DoctorCategory = _dbContext.DoctorCategories
                                       .Where(dc => dc.CategoryID == a.Doctor.CategoryID)
                                       .Select(dc => dc.CategoryName)
                                       .FirstOrDefault(),

                    DoctorExperience = a.Doctor.TotalExperience,

                    AppointmentDate = a.AppointmentDetails.StartTime.ToString("dddd, MMMM dd"), 
                    AppointmentDay = a.AppointmentDetails.StartTime.DayOfWeek.ToString(),
                    AppointmentTime = a.AppointmentDetails.StartTime,
                    RemainingTime = (int)(a.AppointmentDetails.StartTime - currentDateTime).TotalMinutes 
                })
                .ToListAsync();

            return upcomingAppointments;
        }


        public async Task<PatientDTO> CreatePatient(int UserID, PatientDTO patientDTO)
        {
            var newPatient = new PatientDetails
            {
                UserID = UserID,
                UserName = patientDTO.UserName,
                FullName = patientDTO.FullName,
                Cnic = patientDTO.Cnic,
                GenderTypeID = patientDTO.GenderTypeID,
                Dob = patientDTO.Dob,
                Weight = patientDTO.Weight,
                Height = patientDTO.Height,
                HeightUnitID = patientDTO.HeightUnitID,
                WeightUnitID = patientDTO.WeightUnitID,
                BloodGroup = patientDTO.BloodGroup,
                PatientImageUrl = patientDTO.Image,
                Address = patientDTO.Address,
                City = patientDTO.City,
                Country = patientDTO.Country
            };

            await _dbContext.PatientDetails.AddAsync(newPatient);
            await _dbContext.SaveChangesAsync();

            return new PatientDTO
            {
                PatientID = newPatient.PatientID,
                UserID = newPatient.UserID,
                UserName = newPatient.UserName,
                FullName = newPatient.FullName,
                Cnic = newPatient.Cnic,
                GenderTypeID = newPatient.GenderTypeID,
                Dob = newPatient.Dob,
                Weight = newPatient.Weight,
                Height = newPatient.Height,
                HeightUnitID = newPatient.HeightUnitID,
                WeightUnitID = newPatient.WeightUnitID,
                BloodGroup = newPatient.BloodGroup,
                Image = newPatient.PatientImageUrl,
                Address = newPatient.Address
            };
        }
        public async Task<IActionResult> UpdatePatientImage(int PatientID, string ImageUrl)
        {
            var patient = _dbContext.PatientDetails.Where(x => x.PatientID == PatientID).FirstOrDefault();
            if (patient != null)
            {
                patient.PatientImageUrl = ImageUrl;
                patient.UpdatedAt = DateTime.UtcNow;
            }
            await _dbContext.SaveChangesAsync();
            return new OkObjectResult(patient);
        }
        public async Task<IActionResult> UpdatePatientPersonalDetails(int PatientID, UpdatePatientPersonalDetailsDTO reqDTO)
        {
            var patient = _dbContext.PatientDetails.Where(x => x.PatientID == PatientID).FirstOrDefault();
            if (patient != null)
            {
                patient.FullName = reqDTO.FullName;
                patient.Cnic = reqDTO.CNIC;
                patient.Address = reqDTO.Address;
                patient.Dob = reqDTO.DOB;
            }
            await _dbContext.SaveChangesAsync();
            var userPatient = _dbContext.Users.Where(x => x.UserID == patient.UserID).FirstOrDefault();
            if (userPatient != null)
            {
                userPatient.Email = reqDTO.Email;
            }
            await _dbContext.SaveChangesAsync();
            return new OkObjectResult(patient);
        }

        public async Task<PatientDetails> UpdatePatientBMIDetails(int PatientID, double height, double weight)
        {
            var patient = _dbContext.PatientDetails.Where(x => x.PatientID == PatientID).FirstOrDefault();
            if (patient != null)
            {
                patient.Height = height;
                patient.Weight = weight;
            }
            await _dbContext.SaveChangesAsync();
            return patient;
        }
    }
}

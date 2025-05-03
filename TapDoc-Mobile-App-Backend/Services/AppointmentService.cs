using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using TapDoc_Mobile_App_Backend.Data;
using TapDoc_Mobile_App_Backend.Models;

namespace TapDoc_Mobile_App_Backend.Services
{
    public class AppointmentService
    {
        public readonly ApplicationDbContext _dbContext;
        public AppointmentService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<AppointmentHistoryListDTO>> GetPatientAppointmentHistory(int? AppointmentStatus, int pageNo, int pageSize, int PatientID)
        {
            DateTime currentDateTime = DateTime.UtcNow;

            var query = _dbContext.Appointments
                .Where(a => a.PatientID == PatientID && a.IsActive && !a.IsDeleted);

            if (AppointmentStatus.HasValue)
            {
                query = query.Where(a => a.AppointmentStatus == AppointmentStatus.Value);
            }

            var appointments = await query
                .OrderBy(a => a.CreatedOn)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new
                {
                    a.AppointmentID,
                    a.Doctor.Image,
                    a.Doctor.FullName,
                    a.Doctor.Speciality,
                    a.Doctor.CategoryID,
                    a.Doctor.TotalExperience,
                    a.AppointmentDetails.StartTime,
                    a.AppointmentType,
                    a.AppointmentStatus
                })
                .ToListAsync();

            var appointmentDTOs = appointments.Select(a => new AppointmentHistoryListDTO
            {
                AppointmentID = a.AppointmentID,
                DoctorProfileUrl = a.Image,
                DoctorName = a.FullName,
                DoctorSpeciality = a.Speciality,
                DoctorCategory = _dbContext.DoctorCategories
                                    .Where(dc => dc.CategoryID == a.CategoryID)
                                    .Select(dc => dc.CategoryName)
                                    .FirstOrDefault() ?? "Unknown",
                DoctorExperience = a.TotalExperience,
                AppointmentDate = a.StartTime.ToString("dddd, MMMM dd"),
                AppointmentDay = a.StartTime.DayOfWeek.ToString(),
                AppointmentTime = a.StartTime,
                RemainingTime = (int)(a.StartTime - currentDateTime).TotalMinutes,

                AppointmentStatus = MapAppointmentStatus(a.AppointmentStatus),
                AppointmentType = MapAppointmentType(a.AppointmentType)

            }).ToList();

            return appointmentDTOs;
        }
        private string MapAppointmentStatus(int status)
        {
            return status switch
            {
                1 => "Upcoming",
                2 => "Cancelled",
                3 => "Completed",
                4 => "Pending",
                _ => "Unknown"
            };
        }

        private string MapAppointmentType(int type)
        {
            return type switch
            {
                7 => "Chat + On-Premise",
                8 => "VideoConsultation + Chat",
                9 => "Chat Consultation",
                _ => "Unknown"
            };
        }
        public async Task<List<BookAnAppointmentDoctorsDTO>> GetBookAnAppointmentDoctors(int? CategoryID, string? City, int pageNo, int pageSize)
        {
            var query = _dbContext.DoctorDetails
                .Where(d => d.IsDeleted == false && d.IsActive == true);

            if (CategoryID.HasValue)
            {
                query = query.Where(d => d.CategoryID == CategoryID.Value);
            }

            if (!string.IsNullOrEmpty(City))
            {
                query = query.Where(d => d.City.ToLower() == City.ToLower());
            }

            var doctors = await query
                .Select(d => new
                {
                    d.DoctorID,
                    d.Image,
                    d.FullName,
                    d.Speciality,
                    d.CategoryID,
                    d.TotalExperience,
                    d.Address,
                    d.City,
                    TotalRatings = _dbContext.DoctorRatings.Count(r => r.DoctorID == d.DoctorID && r.IsDeleted == false && r.IsActive == true),
                    SumRatings = _dbContext.DoctorRatings
                                  .Where(r => r.DoctorID == d.DoctorID && r.IsDeleted == false && r.IsActive == true)
                                  .Sum(r => (double?)r.Rating) ?? 0
                })
                .OrderByDescending(d => d.TotalRatings > 0 ? d.SumRatings / d.TotalRatings : 0)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var doctorDTOs = doctors.Select(d => new BookAnAppointmentDoctorsDTO
            {
                DoctorID = d.DoctorID,
                DoctorProfileUrl = d.Image,
                DoctorName = d.FullName,
                DoctorSpeciality = d.Speciality,
                DoctorCategory = _dbContext.DoctorCategories
                                            .Where(dc => dc.CategoryID == d.CategoryID)
                                            .Select(dc => dc.CategoryName)
                                            .FirstOrDefault() ?? "Unknown",
                DoctorExperience = d.TotalExperience,
                DoctorAddress = $"{d.Address}, {d.City}",
                DoctorRating = d.TotalRatings > 0 ? d.SumRatings / d.TotalRatings : 0
            }).ToList();

            return doctorDTOs;
        }

        public async Task<DoctorDetailsForCreateAppointmentDTO> GetDoctorDetailsForAppointmentBooking(int DoctorID)
        {
            if (DoctorID == 0)
            {
                throw new Exception("DoctorID not found");
            }

            var doctorDetails = await _dbContext.DoctorDetails
                .FirstOrDefaultAsync(x => x.DoctorID == DoctorID);

            if (doctorDetails == null)
            {
                return null;
            }

            var doctorRatings = await _dbContext.DoctorRatings
                .Where(x => x.DoctorID == DoctorID)
                .ToListAsync();

            double averageRating = doctorRatings.Any() ? doctorRatings.Average(r => r.Rating) : 0;

            var patientIds = doctorRatings.Select(r => r.PatientID).Distinct().ToList();
            var patients = await _dbContext.PatientDetails
                .Where(p => patientIds.Contains(p.PatientID))
                .ToListAsync();

            var reviews = doctorRatings.Select(rating =>
            {
                var patient = patients.FirstOrDefault(p => p.PatientID == rating.PatientID);
                return new DoctorReviews
                {
                    PatientID = rating.PatientID,
                    PatientName = patient?.FullName ?? "Unknown",
                    PatientImageURL = patient?.PatientImageUrl ?? "",
                    Rating = rating.Rating,
                    ReviewDescription = rating.Description
                };
            }).ToList();

            var availabilityRecords = await _dbContext.AvailabilityDetails
                .Where(x => x.DoctorID == DoctorID)
                .ToListAsync();

            var availabilityList = availabilityRecords.Select(av => new DoctorAvailability
            {
                Availability = new Dictionary<string, string>
    {
        { av.DayOfWeek, $"{av.StartTime.ToString("hh:mm tt")} - {av.EndDate.ToString("hh:mm tt")}" }
    }
            }).ToList();


            var category = await _dbContext.DoctorCategories
                .Where(c => c.CategoryID == doctorDetails.CategoryID)
                .Select(c => c.CategoryName)
                .FirstOrDefaultAsync();

            return new DoctorDetailsForCreateAppointmentDTO
            {
                UserID = doctorDetails.UserID,
                DoctorID = doctorDetails.DoctorID,
                DoctorProfileURL = doctorDetails.Image,
                DoctorName = doctorDetails.FullName,
                DoctorCategory = category ?? "General",
                DoctorRating = averageRating,
                DoctorExperience = doctorDetails.TotalExperience,
                DoctorFee = doctorDetails.DoctorFee,
                DoctorDescription = doctorDetails.DoctorDescription,
                DoctorSpeciality = doctorDetails.Speciality,
                DoctorReviews = reviews,
                DoctorAvailabilities = availabilityList
            };
        }

        public async Task<InnerDocDetailsForAppointmentDTO> GetDoctorDetailsForInnerPage(int DoctorID)
        {
            if (DoctorID == 0)
            {
                throw new Exception("DoctorID not found");
            }

            var doctorDetails = await _dbContext.DoctorDetails
                .FirstOrDefaultAsync(x => x.DoctorID == DoctorID);

            if (doctorDetails == null)
            {
                return null;
            }

            var categoryName = await _dbContext.DoctorCategories
                .Where(c => c.CategoryID == doctorDetails.CategoryID)
                .Select(c => c.CategoryName)
                .FirstOrDefaultAsync();

            var availabilityRecords = await _dbContext.AvailabilityDetails
                .Where(a => a.DoctorID == DoctorID)
                .ToListAsync();

            var availabilityList = availabilityRecords.Select(av => new DoctorAvailabilityInnerPage
            {
                Availability = new Dictionary<string, string>
        {
            { av.DayOfWeek, $"{av.StartTime.ToString("hh:mm tt")} - {av.EndDate.ToString("hh:mm tt")}" }
        }
            }).ToList();

            return new InnerDocDetailsForAppointmentDTO
            {
                UserID = doctorDetails.UserID,
                DoctorID = doctorDetails.DoctorID,
                DoctorProfileURL = doctorDetails.Image,
                DoctorName = doctorDetails.FullName,
                DoctorCategory = categoryName ?? "General",
                DoctorExperience = doctorDetails.TotalExperience,
                DoctorFee = doctorDetails.DoctorFee,
                DoctorDescription = doctorDetails.DoctorDescription,
                DoctorSpeciality = doctorDetails.Speciality,
                DoctorAvailabilities = availabilityList
            };
        }
        public async Task<string> CreateAppointment(CreateAppointmentDTO reqDTO)
        {
            var appointment = new Appointment
            {
                PatientID = reqDTO.PatientID,
                DoctorID = reqDTO.DoctorID,
                AppointmentStatus = (int)AppointmentEnums.Pending,
                AppointmentType = reqDTO.AppointmentType,
            };
            await _dbContext.AddAsync(appointment);
            await _dbContext.SaveChangesAsync();

            var appointmentDetails = new AppointmentDetails
            {
                AppointmentID = appointment.AppointmentID,
                PatientID = appointment.PatientID,
                DoctorID = appointment.DoctorID,
                AppointmentDate = reqDTO.AppointmentDate,
                StartTime = reqDTO.StartTime,
                EndTime = reqDTO.EndTime,
                AppointmentDescription = reqDTO.AppointmentDescription,
                AppointmentFee = reqDTO.AppointmentFee,
                NoOfSlots = reqDTO.NoOfSlots,
                PaymentID = reqDTO.PaymentID
            };
            await _dbContext.AddAsync(appointmentDetails);
            await _dbContext.SaveChangesAsync();
            return "Appointment created successfully";

        }




    }
}

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


    }
}

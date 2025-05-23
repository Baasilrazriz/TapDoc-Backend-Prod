using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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
        public async Task<List<AppointmentHistoryListDTO>> GetPatientAppointmentHistory(int? AppointmentStatus, int? pageNo, int? pageSize, int PatientID)
        {
            DateTime currentDateTime = DateTime.Now;

            int currentPage = pageNo > 0 ? pageNo.Value : 1;
            int currentPageSize = pageSize > 0 ? pageSize.Value : 10;

            var query = _dbContext.Appointments
                .Where(a => a.PatientID == PatientID && a.IsActive && !a.IsDeleted);

            if (AppointmentStatus.HasValue)
            {
                query = query.Where(a => a.AppointmentStatus == AppointmentStatus.Value);
            }

            var appointments = await query
                .OrderBy(a => a.CreatedOn)
                .Skip((currentPage - 1) * currentPageSize)
                .Take(currentPageSize)
                .Select(a => new
                {
                    a.AppointmentID,
                    DoctorImage = a.Doctor.Image,
                    DoctorName = a.Doctor.FullName,
                    DoctorSpeciality = a.Doctor.Speciality,
                    a.Doctor.CategoryID,
                    DoctorExperience = a.Doctor.TotalExperience,
                    StartTime = a.AppointmentDetails.StartTime,
                    a.AppointmentType,
                    a.AppointmentStatus,
                    a.AppointmentDetails.AppointmentFee,
                    DoctorID = a.DoctorID
                })
                .ToListAsync();

            var appointmentDTOs = new List<AppointmentHistoryListDTO>();

            foreach (var a in appointments)
            {
                var totalRatings = await _dbContext.DoctorRatings.CountAsync(r => r.DoctorID == a.DoctorID && !r.IsDeleted && r.IsActive);
                var sumRatings = await _dbContext.DoctorRatings
                    .Where(r => r.DoctorID == a.DoctorID && !r.IsDeleted && r.IsActive)
                    .SumAsync(r => (double?)r.Rating) ?? 0;

                var categoryName = await _dbContext.DoctorCategories
                    .Where(dc => dc.CategoryID == a.CategoryID)
                    .Select(dc => dc.CategoryName)
                    .FirstOrDefaultAsync() ?? "Unknown";

                var remaining = a.StartTime - currentDateTime;
                var remainingTimeFormatted = remaining.TotalMinutes < 0
                    ? "Started"
                    : $"{(int)remaining.TotalHours}h {remaining.Minutes}m";

                appointmentDTOs.Add(new AppointmentHistoryListDTO
                {
                    AppointmentID = a.AppointmentID,
                    DoctorProfileUrl = a.DoctorImage,
                    DoctorName = a.DoctorName,
                    DoctorSpeciality = a.DoctorSpeciality,
                    DoctorCategory = categoryName,
                    DoctorExperience = a.DoctorExperience,
                    AppointmentDate = a.StartTime.ToString("dddd, MMMM dd"),
                    AppointmentDay = a.StartTime.DayOfWeek.ToString(),
                    AppointmentTime = a.StartTime,
                    RemainingTime = remainingTimeFormatted,
                    AppointmentStatus = MapAppointmentStatus(a.AppointmentStatus),
                    AppointmentType = MapAppointmentType(a.AppointmentType),
                    AppointmentFees = a.AppointmentFee,
                    DoctorRating = totalRatings > 0 ? sumRatings / totalRatings : 0
                });
            }

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
            var doctorQualifications = await _dbContext.DoctorQualifacations
    .Where(x => x.DoctorID == doctorDetails.DoctorID)
    .Select(x => new DoctorQualification
    {
        QualificationName = x.QualificationName,
        InstituteName = x.InstituteName,
        QualificationDescription = x.QualificationDescription
    })
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
                DoctorAvailabilities = availabilityList,
                DoctorQualifications = doctorQualifications
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
                PaymentID = reqDTO.PaymentID,
                RefundDeadline = reqDTO.AppointmentDate.AddHours(6)
            };
            await _dbContext.AddAsync(appointmentDetails);
            await _dbContext.SaveChangesAsync();
            return "Appointment created successfully";

        }

        public async Task<AppointmentDetailsDTO> GetAppointmentDetails(int AppointmentID)
        {
            var appointmentDetails = await _dbContext.AppointmentsDetails.Where(x => x.AppointmentID == AppointmentID).FirstOrDefaultAsync();
            var appointment = await _dbContext.Appointments.Where(x => x.AppointmentID == appointmentDetails.AppointmentID).FirstOrDefaultAsync();

            if (appointmentDetails == null)
            {
                throw new Exception("Appointment Details not found");
            }
            var doctorDetails = await _dbContext.DoctorDetails
                .FirstOrDefaultAsync(x => x.DoctorID == appointmentDetails.DoctorID);

            DateTime date = appointmentDetails.AppointmentDate;
            DateTime RefundDate = appointmentDetails.RefundDeadline;
            var start = appointmentDetails.StartTime;
            var end = appointmentDetails.EndTime;
            var timeSlots = new List<KeyValuePair<string, string>>();
            var slotTime = start;

            while (slotTime < end)
            {
                var nextSlot = slotTime.AddMinutes(30);
                if (nextSlot > end) break;

                string slotLabel = $"{slotTime:hh:mm tt} - {nextSlot:hh:mm tt}";
                timeSlots.Add(new KeyValuePair<string, string>(slotTime.ToString("HH:mm"), slotLabel));

                slotTime = nextSlot;
            }

            return new AppointmentDetailsDTO
            {
                AppointmentID = appointmentDetails.AppointmentID,
                DoctorName = doctorDetails.FullName,
                DoctorExperience = doctorDetails.TotalExperience,
                DoctorSpeciality = doctorDetails.Speciality,
                ShowCaseAppointmentID = "AXZ" + AppointmentID + "B12",
                DoctorImageUrl = doctorDetails.Image,
                AppointmentType = MapAppointmentType(appointment.AppointmentType),
                AppointmentStatus = MapAppointmentStatus(appointment.AppointmentID),
                TimeSlots = timeSlots,
                AppointmentDescription = appointmentDetails.AppointmentDescription,
                AppointmentBookedOn = $"{date:dddd}, {date:hh:mm tt}, {date:MMM} {date:dd}, {date:yyyy}",
                RefundDeadline = $"{RefundDate:dddd}, {RefundDate:hh:mm tt}, {RefundDate:MMM} {RefundDate:dd}, {RefundDate:yyyy}",

            };
        }


    }
}

using CommunityToolkit.HighPerformance.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.FileProviders;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using TapDoc_Mobile_App_Backend.Data;
using TapDoc_Mobile_App_Backend.Models;
using TapDoc_Mobile_App_Backend.Services;

namespace TapDoc_Mobile_App_Backend.Controllers
{
    [ApiController]
    [Route("/Appointment")]
    public class AppointmentController : ControllerBase
    {
        public readonly ApplicationDbContext _dbContext;
        public AppointmentService _service;
        public AppointmentController(ApplicationDbContext dbContext, AppointmentService service)
        {
            _dbContext = dbContext;
            _service = service;
        }

        [HttpGet("get-patient-appointment-history")]
        public async Task<IActionResult> GetPatientAppointmentHistory([FromQuery] int PatientID, [FromQuery] int? AppointmentStatus, [FromQuery] int? pageNo, [FromQuery] int? pageSize)
        {
            if (PatientID == 0)
            {
                return BadRequest("Patient ID is invalid");
            }
            var result = await _service.GetPatientAppointmentHistory(AppointmentStatus, pageNo, pageSize, PatientID);
            if (result == null)
            {
                return NotFound("Appointments not found");
            }
            return Ok(new { message = "Success", data = result });
        }
        [HttpGet("get-book-an-appointment-doctors")]
        public async Task<IActionResult> GetBookAnAppointmentDoctors([FromQuery] int? CategoryID, [FromQuery] string? City, int pageNo = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetBookAnAppointmentDoctors(CategoryID, City, pageNo, pageSize);
            if (result == null)
            {
                return NotFound("Doctors not found");
            }
            return Ok(new { message = "Success", data = result });
        }
        [HttpGet("get-doctor-details-for-appointment-booking")]
        public async Task<IActionResult> GetDoctorDetailsForAppointmentBooking([FromQuery] int DoctorID)
        {
            var result = await _service.GetDoctorDetailsForAppointmentBooking(DoctorID);
            if (result == null)
            {
                return NotFound("Details not found");
            }
            return Ok(new { message = "Success", data = result });
        }
        [HttpGet("get-doctor-details-for-inner-page")]
        public async Task<IActionResult> GetDoctorDetailsForInnerPage([FromQuery] int DoctorID)
        {
            var result = await _service.GetDoctorDetailsForInnerPage(DoctorID);
            if (result == null)
            {
                return NotFound("Details not found");
            }
            return Ok(new { message = "Success", data = result });
        }
        [HttpPost("create-appointment")]
        public async Task<IActionResult> CreateAppointment([FromBody] CreateAppointmentDTO reqDTO)
        {
            var result = await _service.CreateAppointment(reqDTO);
            if (result == null)
            {
                return BadRequest("Failed to create appointment");
            }
            return Ok(new { message = "Success", data = result });
        }
        [HttpGet("get-appointment-details")]
        public async Task<IActionResult> GetAppointmentDetails([FromQuery] int AppointmentID)
        {
            if (AppointmentID == 0)
            {
                return BadRequest("Appointment ID is not valid");
            }
            var result = await _service.GetAppointmentDetails(AppointmentID);
            if (result == null)
            {
                return BadRequest("Appointment Details not found");
            }
            return Ok(new { message = "Success", data = result });
        }
        [HttpPost("change-appointment-time-slots")]
        public async Task<IActionResult> ChangeAppointmentTimeSlots([FromBody] ChangeTimeSlotsDTO reqDTO)
        {
            if (reqDTO == null)
            {
                return BadRequest("Invalid Data in DTO");
            }
            var result = await _service.ChangeAppointmentTimeSlots(reqDTO);
            if (result == null)
            {
                return BadRequest("Failed to request change of time slots");
            }
            return Ok(new { message = "success", data = result });
        }
        [HttpGet("get-change-request-appointments")]
        public async Task<IActionResult> GetChangeRequestAppointments([FromQuery] int DoctorID)
        {
            var result = _service.GetChangeRequestAppointments(DoctorID);
            if (result != null)
            {
                return Ok(new { message = "success", data = result });
            }
            return BadRequest("No appointments found that requested change");
        }
        [HttpPost("accept-decline-change-request")]
        public async Task<IActionResult> AcceptDeclineChangeRequest([FromQuery] int AppointmentID, [FromQuery] int StatusID)
        {
            if (AppointmentID > 0)
            {
                var result = _service.AcceptDeclineChangeRequest(AppointmentID, StatusID);
                if (result != null)
                {
                    return Ok(new { message = "success", data = result });
                }
                return BadRequest("Failed to accept or reject appointment");
            }
            return NotFound("AppointmentID not found");
        }
        [HttpGet("get-change-request-appointment-details")]
        public async Task<IActionResult> GetChangeRequestAppointmentDetails([FromQuery] int AppointmentID)
        {
            if (AppointmentID == 0)
            {
                return NotFound("No appointmentID found");
            }
            var result = await _service.GetChangeRequestAppointmentDetails(AppointmentID);
            if (result != null)
            {
                return Ok(new { message = "success", data = result });
            }
            return BadRequest("No content found");
        }
        [HttpPost("accept-reject-appointment")]
        public async Task<IActionResult> AcceptRejectAppointment([FromQuery] int AppointmentID, [FromQuery] int StatusID)
        {
            if (AppointmentID == 0 || StatusID == 0)
            {
                return NotFound("AppointmentID or statusID not found");
            }
            var result = _service.AcceptRejectAppointment(AppointmentID, StatusID);
            if(result  != null)
            {
                return Ok(new {message = "success", data = result});
            }
            return BadRequest("An error occurred");
        }
        [HttpGet("get-doctor-appointment-details")]
        public async Task<IActionResult> GetDoctorAppointmentDetails([FromQuery] int AppointmentID)
        {
            if (AppointmentID == 0)
            {
                return NotFound("No appointmentID found");
            }
            var result = await _service.GetDoctorAppointmentDetails(AppointmentID);
            if (result != null)
            {
                return Ok(new { message = "success", data = result });
            }
            return BadRequest("No content found");
        }
        [HttpGet("get-doctor-appointment-history")]
        public async Task<IActionResult> GetDoctorAppointmentHistory([FromQuery] int DoctorID, [FromQuery] int? AppointmentStatus, [FromQuery] int? pageNo, [FromQuery] int? pageSize)
        {
            if (DoctorID == 0)
            {
                return BadRequest("DoctorID ID is invalid");
            }
            var result = await _service.GetDoctorAppointmentHistory(AppointmentStatus, pageNo, pageSize, DoctorID);
            if (result == null)
            {
                return NotFound("Appointments not found");
            }
            return Ok(new { message = "Success", data = result });
        }
    }


}

using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetPatientAppointmentHistory([FromQuery] int PatientID, [FromQuery] int? AppointmentStatus, [FromQuery] int pageNo = 1, [FromQuery] int pageSize = 10)
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
    }


}

using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetPatientAppointmentHistory([FromQuery] int PatientID,[FromQuery] int? AppointmentStatus, [FromQuery] int pageNo = 1, [FromQuery] int pageSize = 10)
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
        public async Task<IActionResult> GetBookAnAppointmentDoctors([FromQuery] int? CategoryID,[FromQuery]string?City, int pageNo = 1, [FromQuery]int pageSize =10)
        {
            var result = await _service.GetBookAnAppointmentDoctors(CategoryID,City, pageNo, pageSize);
            if(result == null)
            {
                return NotFound("Doctors not found");
            }
            return Ok(new { message = "Success", data = result });
        }
    }
}

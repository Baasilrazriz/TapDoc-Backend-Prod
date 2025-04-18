using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.OpenApi.Models;
using Microsoft.VisualBasic;
using System.Reflection.Metadata.Ecma335;
using TapDoc_Mobile_App_Backend.Data;
using TapDoc_Mobile_App_Backend.Models;
using TapDoc_Mobile_App_Backend.Services;

namespace TapDoc_Mobile_App_Backend.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("/Patient")]
    public class PatientController : ControllerBase
    {
        public readonly ApplicationDbContext _dbContext;
        public PatientService _service;

        public PatientController(ApplicationDbContext dbContext, PatientService service)
        {
            _dbContext = dbContext;
            _service = service;
        }

        [HttpGet("get-patient-home-screen-details")]
        public async Task<IActionResult> GetPatientHomeDetails([FromQuery] int PatientID)
        {
            if (PatientID <= 0)
            {
                return BadRequest("Invalid PatientID");
            }
            var result = await _service.GetPatientHomeDetails(PatientID);
            return Ok(new { message = "Success", data = result });
        }
        [HttpGet("get-home-screen-best-rated-doctors")]
        public async Task<IActionResult> GetHomeScreenBestDoctors([FromQuery] string City, [FromQuery] int pageNo = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetHomeScreenBestDoctors(pageNo, pageSize, City);
            if (result == null)
            {
                return BadRequest("An error occurred");
            }
            return Ok(result);
        }

        [HttpGet("get-patient-home-upcoming-appointments")]
        public async Task<IActionResult> GetHomeUpcomingAppointmentDetails([FromQuery] int PatientID, [FromQuery] int pageNo = 1, [FromQuery] int pageSize = 10)
        {
            var result = await _service.GetHomeUpcomingAppointmentDetails(PatientID,pageNo, pageSize);
            if (result == null)
            {
                return BadRequest("An error occurred");
            }
            return Ok(new { message = "success", data = result });
        }
        [HttpPost("create-patient")]
        public async Task<IActionResult> CreatePatientDetails([FromQuery] int UserID, [FromBody] PatientDTO patientDTO)
        {
            if (UserID == 0)
            {
                return NotFound(new { message = "Invalid UserID" });
            }

            if (patientDTO == null)
            {
                return BadRequest(new { message = "Details cannot be null" });
            }
            var result = await _service.CreatePatient(UserID, patientDTO);
            if (result == null)
            {
                return BadRequest("Patient creation failed");
            }
            return Ok(new { message = "Patient created successfully", data = result });
        }

        [HttpPost("update-patient-image")]
        public async Task<IActionResult> UpdatePatientImage([FromQuery] int PatientID, [FromQuery] string ImageUrl)
        {

            if (ImageUrl == null || PatientID <= 0)
            {
                return BadRequest(new { message = "Image or PatientID not found" });
            }
            var result = await _service.UpdatePatientImage(PatientID, ImageUrl);
            if (result == null)
            {
                return BadRequest("Update image failed");
            }
            return Ok(new { message = "Image Updated Successfully", data = result });

        }

        [HttpPost("update-patient-personal-details")]
        public async Task<IActionResult> UpdatePatientPersonalDetails([FromQuery] int PatientID, [FromBody] UpdatePatientPersonalDetailsDTO reqDTO)
        {
            if (PatientID <= 0 || reqDTO == null)
            {
                return BadRequest("PatientID is null or reqDTO is null");
            }
            var result = await _service.UpdatePatientPersonalDetails(PatientID, reqDTO);
            if (result == null)
            {
                return BadRequest("Updation failed");
            }

            return Ok(new { message = "Patient details updated successfully", data = result });

        }

        [HttpPost("update-patient-BMI-details")]
        public  async Task<IActionResult> UpdatePatientBMI([FromQuery] int PatientID, [FromQuery] double height, [FromQuery] double weight)
        {
            if (PatientID <= 0 || height <= 0 || weight <= 0)
            {
                return BadRequest("Invalid details");
            }
            var result = await _service.UpdatePatientBMIDetails(PatientID, height, weight);
            if (result == null)
            {
                return BadRequest("Updation failed");
            }
            return Ok(new { message = "Patient BMI details updated successfully", data = result });
        }


    }
}

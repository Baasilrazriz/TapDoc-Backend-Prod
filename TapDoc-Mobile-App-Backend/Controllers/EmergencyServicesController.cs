using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Reflection.Metadata.Ecma335;
using TapDoc_Mobile_App_Backend.Data;
using TapDoc_Mobile_App_Backend.Models;
using TapDoc_Mobile_App_Backend.Services;

namespace TapDoc_Mobile_App_Backend.Controllers
{
    [ApiController]
    [Route("/EmergencyService")]
    public class EmergencyServicesController : ControllerBase
    {
        public readonly ApplicationDbContext _dbContext;
        public EmergencyServicesService _service;
        public EmergencyServicesController(ApplicationDbContext dbContext, EmergencyServicesService service)
        {
            _dbContext = dbContext;
            _service = service;

        }
        [HttpGet("get-emergency-services")]
        public async Task<IActionResult> GetEmergencyServices()
        {
            try
            {
                var emergencyServices = await _dbContext.EmergencyServices.ToListAsync();
                if(emergencyServices.Any())
                {
                    return Ok(new { message = "Emergency services retrieved successfully", data = emergencyServices });
                }
                return BadRequest(new { message = "Emergency services not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("create-emergency-contacts")]
        public async Task<IActionResult> CreateEmergencyServices([FromBody] EmergencyServicesDTO emergencyServices)
        {
            try
            {
                if (emergencyServices == null)
                {
                    return BadRequest(new { message = "Emergency services data is null" });
                }
                var newEmergencyService = new EmergencyServices
                {
                    ServiceName = emergencyServices.ServiceName,
                    ServiceDescription = emergencyServices.ServiceDescription,
                    ServiceImage = emergencyServices.ServiceImage,
                    ServiceNumber = emergencyServices.ServiceNumber,
                };
                _dbContext.EmergencyServices.Add(newEmergencyService);
                await _dbContext.SaveChangesAsync();
                return Ok(new { message = "Emergency service created successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("delete-emergency-service")]
        public async Task<IActionResult> DeleteEmergencyService ([FromQuery] int id)
        {
            if (id == 0)
            {
                return BadRequest(new { message = "Id is null" });
            }

            var emergencyService = await _dbContext.EmergencyServices
                .Where(x=> x.EmergencyServiceID == id).FirstOrDefaultAsync();

            if(emergencyService == null)
            {
                return BadRequest(new { message = "No emergency service found" });
            }
            emergencyService.IsActive = false;
            emergencyService.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
            return Ok(new { message = "Emergency Service deleted successfully" });
        }
    }
}

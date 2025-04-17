using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using TapDoc_Mobile_App_Backend.Data;
using TapDoc_Mobile_App_Backend.Models;
using TapDoc_Mobile_App_Backend.Services;

namespace TapDoc_Mobile_App_Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/EmergencyContacts")]
    public class EmergencyContactsController : ControllerBase
    {
        public readonly ApplicationDbContext _dbContext;
        public EmergencyContactsService _service;
        public EmergencyContactsController(ApplicationDbContext dbContext, EmergencyContactsService service)
        {
            _dbContext = dbContext;
            _service = service;
        }
        [HttpGet("get-patient-contacts")]
        public async Task<IActionResult> GetPatientContacts([FromQuery] int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest(new { message = "Patient id is null" });
                }
                var emergencyContacts = await _dbContext.EmergencyContacts
                    .Where(x => x.PatientID == id && x.IsDeleted == false && x.IsActive == true)
                    .ToListAsync();

                if (emergencyContacts == null)
                {
                    return NotFound(new { message = "Emergency contacts not found" }); ;
                }
                return Ok(new
                {
                    message = "Emergency contacts retrieved successfully",
                    data = emergencyContacts
                });
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost("create-emergency-contacts")]
        public async Task<IActionResult> CreateEmergencyContacts([FromQuery] int id,[FromBody]EmergencyContactsDTO emergencyContacts )
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest(new { message = "Patient id is null" });
                }
                if (emergencyContacts == null)
                {
                    return BadRequest(new { message = "Emergency contacts data is null" });
                }

                var newEmergencyContact = new EmergencyContacts
                {
                    PatientID = id,
                    ContactName = emergencyContacts.ContactName,
                    ContactPhone = emergencyContacts.phhoneNumber
                };
                _dbContext.EmergencyContacts.Add(newEmergencyContact);
                await _dbContext.SaveChangesAsync();
                return Ok(new { message = "Emergency contact created successfully" });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
            
        }

        [HttpPut("edit-emergency-contacts")]
        public async Task<IActionResult> EditEmergencyContact([FromQuery]int id, [FromBody] EmergencyContactsDTO emergencyContacts)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest(new { message = "Emergency contact id is null" });
                }
                if (emergencyContacts == null)
                {
                    return BadRequest(new { message = "Emergency contact information is null" });
                }
                var editEmergencyContact = await _dbContext.EmergencyContacts
                    .Where(x => x.EmergencyContactID == id).FirstOrDefaultAsync();

                if (editEmergencyContact != null)
                {
                    editEmergencyContact.ContactName = emergencyContacts.ContactName;
                    editEmergencyContact.ContactPhone = emergencyContacts.phhoneNumber;
                    editEmergencyContact.UpdatedAt = DateTime.Now;

                    _dbContext.Entry(editEmergencyContact).State = EntityState.Modified;
                    await _dbContext.SaveChangesAsync();
                    return Ok(new { message = "Emergency contact updated successfully" });
                }
                return NotFound(new { message = "Emergency contact not found" });
            }
            catch (Exception ex)
            { 
                return BadRequest(ex.Message);
            }

        }
        [HttpDelete("delete-emergency-contact")]
        public async Task<IActionResult> DeleteEmergencyContact ([FromQuery] int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest(new { message = "Emergency contact id is null" });
                }
                var emergencyContact = await _dbContext.EmergencyContacts
                    .Where(x => x.EmergencyContactID == id).FirstOrDefaultAsync();
                if (emergencyContact != null)
                {
                    emergencyContact.IsDeleted = true;
                    emergencyContact.IsActive = false;
                    await _dbContext.SaveChangesAsync();
                    return Ok(new { message = "Emergency contact deleted successfully" });
                }
                return NotFound(new { message = "Emergency contact not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }
    }
}

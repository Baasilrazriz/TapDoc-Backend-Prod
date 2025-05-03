using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.Xml;
using TapDoc_Mobile_App_Backend.Data;
using TapDoc_Mobile_App_Backend.Models;
using TapDoc_Mobile_App_Backend.Services;

namespace TapDoc_Mobile_App_Backend.Controllers
{
    [ApiController]
    [Route("/LabRecords")]
    public class RecordsController : ControllerBase
    {
        public readonly ApplicationDbContext _dbContext;
        public RecordsService _service;
        public RecordsController(ApplicationDbContext dbContext, RecordsService service)
        {
            _dbContext = dbContext;
            _service = service;

        }
        [HttpGet("get-lab-reports-list")]
        public async Task<IActionResult> GetUserLabReports([FromQuery] int id)
        {
            if (id == 0)
            {
                return BadRequest(new { message = "User id not found" });
            }
            var labReports = await _dbContext.LabRecords.Where(x => x.PatientID == id && x.IsActive == true && x.IsDeleted == false).ToListAsync();
            if (!labReports.Any())
            {
                return BadRequest(new { message = "Lab reports not found" });
            }
            return Ok(labReports);
        }

        //get a single user lab record
        [HttpGet("get-patient-lab-record")]
        public async Task<IActionResult> GetPatientLabReport([FromQuery] int id)
        {
            if (id == 0)
            {
                return BadRequest(new { message = "Lab report id is null" });
            }
            var labRecord = await _dbContext.LabRecords
                .Where(x => x.LabRecordID == id).FirstOrDefaultAsync();
            if (labRecord == null)
            {
                return NotFound(new { message = "Lab record not found" });
            }
            return Ok(new { message = "Lab report retrieved successfully", data = labRecord });
        }

        [HttpPost("create-patient-lab-record")]
        public async Task<IActionResult> CreateUserLabReports([FromQuery] int PatientID, [FromBody] LabRecordsDTO labRecordsDTO)
        {
            if (labRecordsDTO == null || PatientID == 0)
            {
                return BadRequest("PatientID is null or dto is null");
            }
            var result = await _service.CreateUserLabReports(PatientID, labRecordsDTO);
            if (result == null)
            {
                return BadRequest("Creation failed");
            }

            return Ok(new { message = "Lab record created successfully", data = result });
        }
        [HttpPost("create-patient-payment-record")]
        public async Task<IActionResult> CreatePatientPaymentRecord([FromQuery] int PatientID, [FromBody] PaymentRecordDTO paymentDTO)
        {
            if (PatientID == 0 || paymentDTO == null)
            {
                return BadRequest("DTO is null or invalid patient request");
            }
            var result = await _service.CreatePatientPaymentRecord(PatientID, paymentDTO);
            if (result == null)
            {
                return BadRequest("Creation failed");
            }
            return Ok(new { message = "Payment record created successfully", data = result });

        }
        [HttpPost("create-patient-prescription")]
        public async Task<IActionResult> CreatePatientPrescription([FromQuery]int PatientID, [FromBody]PrescriptionDTO reqDTO)
        {
            if(PatientID == 0 || reqDTO == null)
            {
                return BadRequest("PatientID null or DTO is null");
            }
            var result = await _service.CreatePatientPrescription(PatientID, reqDTO);
            if (result == null)
            {
                return BadRequest("An error occurred");
            }
            return Ok(new { message = "Prescription created successfully", data = result });
        }
        
        //delete a single lab record

        [HttpDelete("delete-lab-reports")]
        public async Task<IActionResult> DeleteLabReports([FromQuery] int id)
        {
            if(id==0)
            {
                return BadRequest(new { message = "Lab report id is null" });
            }
            var labReport = await _dbContext.LabRecords.Where(x=>x.LabRecordID==id).FirstOrDefaultAsync();
            if(labReport == null)
            {
                return NotFound(new {message= "Lab record not found"});
            }
            labReport.IsDeleted = true;
            labReport.IsActive = false;
            return Ok(new {message= "Lab report deleted successfully"});
        }



    }

}

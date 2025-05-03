using Microsoft.AspNetCore.Mvc;
using TapDoc_Mobile_App_Backend.Models;
using TapDoc_Mobile_App_Backend.Services;

namespace TapDoc_Mobile_App_Backend.Controllers
{
    [ApiController]
    [Route("/Payment")]
    public class PaymentController : ControllerBase
    {
        public readonly ApplicationDbContext _context;
        public readonly PaymentService _service;
        public PaymentController(ApplicationDbContext context, PaymentService paymentService)
        {
            _context = context;
            _service = paymentService;
        }
        [HttpPost("create-payment-details")]
        public async Task<IActionResult> CreatePaymentDetails([FromBody]Payments reqDTO)
        {
            var result = await _service.CreatePaymentDetails(reqDTO);
            if(result == null)
            {
                return BadRequest("Failed to create payment details");
            }
            return Ok(new {message= "Success", data = result});
        }
    }
}

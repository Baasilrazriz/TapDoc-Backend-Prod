using Microsoft.AspNetCore.Mvc;

namespace TapDoc_Mobile_App_Backend.Controllers
{
    [ApiController]
    [Route("/Health")]
    public class HealthController : ControllerBase
    {
        [HttpGet("check-server-health")]
        public IActionResult CheckServerHealth()
        {
            return Ok(new { message = "Server is healthy", status = "Healthy" });
        }
    }
}

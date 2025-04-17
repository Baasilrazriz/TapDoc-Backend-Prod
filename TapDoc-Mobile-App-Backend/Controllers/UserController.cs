using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Formats.Asn1;
using TapDoc_Mobile_App_Backend.Data;
using TapDoc_Mobile_App_Backend.Models;
using TapDoc_Mobile_App_Backend.Services;
namespace TapDoc_Mobile_App_Backend.Controllers
{
    [ApiController]
    [Route("/User")]
    public class UserController : ControllerBase
    {
        public readonly ApplicationDbContext _context;
        public UsersService _userService;
        public UserController(ApplicationDbContext context, UsersService userService)
        {
            _context = context;
            _userService = userService;
        }
        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO userDTO)
        {
            if(userDTO == null)
            {
                return BadRequest("User information cannot be null");
            }
            var result = await _userService.CreateUser(userDTO);
            if(result == null)
            {
                return BadRequest("User creation failed");
            }
            return Ok(result);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using TapDoc_Mobile_App_Backend.Data;
using TapDoc_Mobile_App_Backend.Models;

namespace TapDoc_Mobile_App_Backend.Services
{
    public class UsersService
    {
        public readonly ApplicationDbContext _context;
        public UsersService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<CreateUserDTO> CreateUser(CreateUserDTO userDTO)
        {
            var newUser = new Users
            {
                Email = userDTO.Email,
                PhoneNo = userDTO.PhoneNo,
                RoleTypeID = userDTO.RoleTypeID,
            };
            await _context.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return new CreateUserDTO
            {
                UserID = newUser.UserID,
                Email = newUser.Email,
                PhoneNo = newUser.PhoneNo,
                RoleTypeID = newUser.RoleTypeID
            };
        }
    }
}

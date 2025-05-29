using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Net.Mail;
using TapDoc_Mobile_App_Backend.Data;
using TapDoc_Mobile_App_Backend.Models;

namespace TapDoc_Mobile_App_Backend.Services
{
    public class UsersService
    {
        public readonly ApplicationDbContext _context;
        public readonly S3Service _s3Service;
        public UsersService(ApplicationDbContext context, S3Service s3Service)
        {
            _context = context;
            _s3Service = s3Service;
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
        public async Task<Attachments> UploadAttachmentAsync(UploadAttachmentModel model)
        {
            var user = await _context.Users.FindAsync(model.UserID);
            if (user == null)
                throw new Exception("User not found.");

            var pictureUrl = await _s3Service.UploadFile(model.file);

            var attachment = new Attachments
            {
                AttachmentName = model.AttachmentName,
                AttachmentUrl = pictureUrl,
                CreatedBy = model.UserID,
                CreatedOn = DateTime.Now,
                UserID = model.UserID
            };

            _context.Attachments.Add(attachment);
            await _context.SaveChangesAsync();

            return attachment;
        }
        public async Task<DoctorLoginDTO> GetDoctorUserID(string email)
        {
            var user = await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
            var doctorDetails = await _context.DoctorDetails.Where(x => x.UserID == user.UserID).FirstOrDefaultAsync();
            return new DoctorLoginDTO
            {
                UserID = user.UserID,
                DoctorID = doctorDetails.DoctorID,
                FullName = doctorDetails.FullName,
                UserName = doctorDetails.UserName,
                ImageUrl = doctorDetails.Image,
                Email = doctorDetails.Email,
            };
        }





    }

}

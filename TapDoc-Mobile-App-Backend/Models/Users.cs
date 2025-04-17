using System;
using System.ComponentModel.DataAnnotations;

namespace TapDoc_Mobile_App_Backend.Models
{
    public class Users
    {
        [Key]
        public int UserID{ get; set; }  
        public string Email { get; set; }   
        public string PhoneNo { get; set; }
        public int RoleTypeID { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime UpdatedAt { get; set; } 

        public virtual PatientDetails PatientDetails { get; set; }
        public virtual DoctorDetails DoctorDetails { get; set; }    
    }
}

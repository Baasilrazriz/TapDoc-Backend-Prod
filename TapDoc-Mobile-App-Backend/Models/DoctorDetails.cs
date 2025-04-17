using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TapDoc_Mobile_App_Backend.Models
{
    public class DoctorDetails
    {
        [Key]
        public int DoctorID { get; set; }

        [ForeignKey("Users")]
        public int UserID { get; set; }
        public virtual Users Users { get; set; }

        public string UserName { get; set; }
        public string FullName { get; set; }
        public int CategoryID { get; set; }

        public string Cnic { get; set; }
        public string Gender { get; set; }
        public DateTime Dob { get; set; }
        public string Image { get; set; }
        public string Speciality { get; set; }
        public double TotalExperience { get; set; }
        public double DoctorFee { get; set; }
        public string Address { get; set; }
        public string DoctorDescription { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public bool IsOnline { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

        [ForeignKey("CategoryID")]
        public virtual DoctorCategories DoctorCategories { get; set; }

        public virtual ICollection<DoctorRatings> DoctorRatings { get; set; } = new List<DoctorRatings>();  // FIXED: Now a collection
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TapDoc_Mobile_App_Backend.Models  
{
    public class PatientDetails
    {
        [Key]
        public int PatientID { get; set; }

        [ForeignKey("Users")]
        public int UserID { get; set; }
        public virtual Users User { get; set; }

        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Cnic { get; set; }
        public int GenderTypeID { get; set; }
        public DateTime Dob { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public int HeightUnitID { get; set; }
        public int WeightUnitID { get; set; }
        public string BloodGroup { get; set; }
        public string PatientImageUrl { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public virtual ICollection<DoctorRatings> DoctorRatings { get; set; }
    }
}

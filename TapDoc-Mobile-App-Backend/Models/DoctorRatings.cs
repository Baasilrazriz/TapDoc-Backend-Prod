using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TapDoc_Mobile_App_Backend.Models
{
    public class DoctorRatings
    {
        [Key]
        public int DoctorRatingsID { get; set; }

        [ForeignKey("PatientDetails")]
        public int PatientID { get; set; }
        public virtual PatientDetails PatientDetails { get; set; }

        [ForeignKey("DoctorDetails")]
        public int DoctorID { get; set; }
        public virtual DoctorDetails DoctorDetails { get; set; }

        public string Description { get; set; }
        public double Rating { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}

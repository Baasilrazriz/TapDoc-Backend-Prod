using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TapDoc_Mobile_App_Backend.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }  
        [ForeignKey("Patient")]
        public int PatientID { get; set; }

        [ForeignKey("Doctor")]
        public int DoctorID { get; set; }

        public int AppointmentStatus { get; set; }
        public int AppointmentType { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedOn { get; set; } 

        public virtual PatientDetails Patient { get; set; }
        public virtual DoctorDetails Doctor { get; set; }

        public virtual AppointmentDetails AppointmentDetails{ get; set; }

    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TapDoc_Mobile_App_Backend.Models
{
    public class AppointmentDetails
    {
        [Key]
        public int AppointmentDetailsID { get; set; }

        [ForeignKey("Appointment")]
        public int AppointmentID { get; set; }

        [ForeignKey("PatientDetails")]
        public int PatientID { get; set; }
        [ForeignKey("DoctorDetails")]
        public int DoctorID { get; set; }
        public DateTime AppointmentDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double AppointmentFee { get; set; }
        public int NoOfSlots {  get; set; }
        public int PaymentID { get; set; }
        public string? AppointmentDescription { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } 

        public virtual ICollection<DoctorDetails> Doctors { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TapDoc_Mobile_App_Backend.Models
{
    public class PrescriptionRecords
    {
        [Key]
        public int PrescriptionRecordID { get; set; }

        [ForeignKey("PatientDetails")]
        public int PatientID { get; set; }
        public virtual PatientDetails PatientDetails { get; set; }

        [ForeignKey("DoctorDetails")]
        public int DoctorID { get; set; }
        public virtual DoctorDetails DoctorDetails { get; set; }  // FIXED: Single doctor, not List

        [ForeignKey("Records")]
        public int RecordID { get; set; }
        public virtual Records Records { get; set; }  // Ensure this is included

        [ForeignKey("Appointment")]
        public int AppointmentID { get; set; }
        public virtual Appointment Appointment { get; set; }  // FIXED: Ensure virtual navigation

        public string PrescriptionRecordUrl { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
    }
}

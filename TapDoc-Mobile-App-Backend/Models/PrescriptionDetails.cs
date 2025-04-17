using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TapDoc_Mobile_App_Backend.Models
{
    public class PrescriptionDetails
    {
        [Key]
        public int PrescriptionDetailID { get; set; }

        [ForeignKey("PrescriptionRecords")]
        public int PrescriptionRecordID { get; set; }
        public string MedicationName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool isReminderEnabled { get; set; } = true;
        public virtual PrescriptionRecords PrescriptionRecords { get; set; }

        public virtual List<PrescriptionDetailTimings> PrescriptionDetailTimings { get; set; }
    }
}

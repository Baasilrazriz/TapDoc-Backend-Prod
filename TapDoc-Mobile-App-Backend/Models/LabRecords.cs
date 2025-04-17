using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TapDoc_Mobile_App_Backend.Models
{
    public class LabRecords
    {
        [Key]
        public int LabRecordID { get; set; }

        [ForeignKey("Records")]
        public int RecordID { get; set; } 

        [ForeignKey("PatientDetails")]
        public int PatientID { get; set; }

        public string Title { get; set; }
        public string LabRecordUrl { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }

        public virtual PatientDetails PatientDetails { get; set; }
        public virtual Records Records { get; set; } 
    }
}

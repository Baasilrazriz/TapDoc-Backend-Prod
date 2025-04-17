using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TapDoc_Mobile_App_Backend.Models
{
    public class Records
    {
        [Key]
        public int RecordID {  get; set; }
        public int RecordType { get; set; }
        [ForeignKey("PatientDetails")]
        public int PatientID {  get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

    }
}

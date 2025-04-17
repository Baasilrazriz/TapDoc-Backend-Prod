using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TapDoc_Mobile_App_Backend.Models
{
    public class EmergencyContacts
    {
        [Key]
        public int EmergencyContactID { get; set; }
        [ForeignKey("PatientDetails")]
        public int PatientID { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } 
    }
}

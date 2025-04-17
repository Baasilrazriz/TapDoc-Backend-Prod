using System.ComponentModel.DataAnnotations;

namespace TapDoc_Mobile_App_Backend.Models
{
    public class EmergencyServices
    {
        [Key]
        public int EmergencyServiceID { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public string ServiceImage {  get; set; }
        public string ServiceNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } 
    }
}

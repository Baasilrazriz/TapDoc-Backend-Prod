using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TapDoc_Mobile_App_Backend.Models
{
    public class PrescriptionDetailTimings
    {
        [Key]
        public int PrescriptionDetailTimingsID { get; set; }

        [ForeignKey("PrescriptionDetails")]
        public int PrescriptionDetailID { get; set; }
        public int FrequencyID { get; set; }  
        public int ReminderOffsetMinutes { get; set; }   
        public virtual PrescriptionDetails PrescriptionDetails { get; set; }
    }
} 

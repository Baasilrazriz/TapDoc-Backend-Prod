using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TapDoc_Mobile_App_Backend.Models
{
    public class AvailabilityDetails
    {
        [Key]
        public int AvailabilityDetailsID { get; set; }

        [ForeignKey("DoctorDetails")]
        public int DoctorID { get; set; }
        public int DayOfWeek {  get; set; }
        public DateTime StartTime {  get; set; }
        public DateTime EndDate {  get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TapDoc_Mobile_App_Backend.Models
{
    public class AppointmentRescheduleRequests
    {
        [Key]
        public int AppointmentRescheduleRequestsID {  get; set; }
        [ForeignKey("Appointments")]
        public int AppointmentID {  get; set; }
        public DateTime NewStartTime { get; set; }
        public DateTime NewEndTime {  get; set; }
        public int ChangeRequestStatusID { get; set; }


    }
}

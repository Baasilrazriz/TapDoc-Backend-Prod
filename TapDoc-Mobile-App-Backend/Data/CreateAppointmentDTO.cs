using TapDoc_Mobile_App_Backend.Models;

namespace TapDoc_Mobile_App_Backend.Data
{
    public class CreateAppointmentDTO
    {
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public int AppointmentStatus { get; set; } = (int)AppointmentEnums.Pending;
        public int AppointmentType { get; set; }
        public DateTime AppointmentDate {  get; set; }
        public DateTime StartTime {  get; set; }
        public DateTime EndTime {  get; set; }
        public double AppointmentFee { get; set; }
        public int NoOfSlots {  get; set; }
        public int PaymentID { get; set; }
        public string? AppointmentDescription {  get; set; }

        
    }
}

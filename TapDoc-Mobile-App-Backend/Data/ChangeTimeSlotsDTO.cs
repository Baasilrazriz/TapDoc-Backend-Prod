namespace TapDoc_Mobile_App_Backend.Data
{
    public class ChangeTimeSlotsDTO
    {
        public int AppointmentID {  get; set; }
        public DateTime NewStartTime { get; set; }
        public DateTime NewEndTime { get; set; }
    }
}

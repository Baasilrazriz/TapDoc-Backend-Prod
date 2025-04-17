namespace TapDoc_Mobile_App_Backend.Data
{
    public class PatientHomeScreenDetailsDTO
    {
        public int PatientID {  get; set; }
        public int UserID { get; set; }
        public string PatientName {  get; set; }
        public string PatientEmail { get; set; }
        public string PatientProfileUrl {  get; set; }
        public int PatientUpcomingAppointments {  get; set; }
        public int PatientCompletedAppointments { get; set; }


    }
}

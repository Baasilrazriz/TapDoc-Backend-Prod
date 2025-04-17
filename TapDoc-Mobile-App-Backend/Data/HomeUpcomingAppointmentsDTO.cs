namespace TapDoc_Mobile_App_Backend.Data
{
    public class HomeUpcomingAppointmentsDTO
    {
        public int AppointmentID {  get; set; }
        public string DoctorProfileUrl {  get; set; }
        public string DoctorName { get; set; }
        public string DoctorSpeciality { get; set; }
        public string DoctorCategory {  get; set; }
        public double DoctorExperience { get; set; }
        public int RemainingTime { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentDay { get; set; }
        public DateTime AppointmentTime {  get; set; }


    }
}

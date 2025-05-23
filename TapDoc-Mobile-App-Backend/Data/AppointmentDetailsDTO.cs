namespace TapDoc_Mobile_App_Backend.Data
{
    public class AppointmentDetailsDTO
    {
        public int AppointmentID { get; set; }
        public string DoctorName {  get; set; }
        public double DoctorExperience {  get; set; }
        public string DoctorSpeciality { get; set; }
        public string ShowCaseAppointmentID {  get; set; }
        public string DoctorImageUrl { get; set; }
        public List<KeyValuePair<string,string>> TimeSlots {  get; set; }
        public string AppointmentType {  get; set; }
        public string AppointmentStatus {  get; set; }
        public string AppointmentBookedOn {  get; set; }
        public string AppointmentDescription {  get; set; }
        public string RefundDeadline {  get; set; }
    }
}

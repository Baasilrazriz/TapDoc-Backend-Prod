namespace TapDoc_Mobile_App_Backend.Data
{
    public class GetChangeAppointmentsForDocDTO
    {
        public int AppointmentID {  get; set; }
        public string PatientImageUrl { get; set; }
        public string PatientName { get; set;}
        public string AppointmentStatus { get; set; }
        public string AppointmentType { get; set;}
        public string RemainingTime { get; set; }
        public string AppointmentDate { get; set; }
        public string AppointmentDay { get; set; }
        public DateTime AppointmentTime { get; set; }
        public double AppointmentFees { get; set; }

    }
}

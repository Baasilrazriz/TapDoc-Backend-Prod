namespace TapDoc_Mobile_App_Backend.Data
{
    public class InnerDocDetailsForAppointmentDTO
    {
        public int UserID { get; set; }
        public int DoctorID { get; set; }
        public string DoctorProfileURL { get; set; }
        public string DoctorName { get; set; }
        public string DoctorCategory { get; set; }
        public double DoctorExperience { get; set; }
        public double DoctorFee { get; set; }
        public string DoctorDescription { get; set; }
        public string DoctorSpeciality { get; set; }

        public List<DoctorAvailabilityInnerPage> DoctorAvailabilities { get; set; }

    }
    public class DoctorAvailabilityInnerPage
    {
        public Dictionary<string, string> Availability { get; set; }
    }
}

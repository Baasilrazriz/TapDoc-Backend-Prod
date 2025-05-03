namespace TapDoc_Mobile_App_Backend.Data
{
    public class DoctorDetailsForCreateAppointmentDTO
    {
        public int UserID { get; set; }
        public int DoctorID { get; set; }
        public string DoctorProfileURL { get; set; }
        public string DoctorName { get; set; }
        public string DoctorCategory { get; set; }
        public double DoctorRating { get; set; }
        public double DoctorExperience { get; set; }
        public double DoctorFee { get; set; }
        public string DoctorDescription { get; set; }
        public string DoctorSpeciality { get; set; }
        public List<DoctorReviews> DoctorReviews { get; set; }
        public List<DoctorAvailability> DoctorAvailabilities { get; set; }
    }
    public class DoctorReviews
    {
        public int PatientID { get; set; }
        public string PatientName { get; set; }
        public string PatientImageURL { get; set; }
        public double Rating { get; set; }
        public string ReviewDescription { get; set; }
    }
    public class DoctorAvailability
    {
        public Dictionary<string, string> Availability { get; set; }
    }
}

using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TapDoc_Mobile_App_Backend.Data
{
    public class PrescriptionDTO
    {
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public int AppointmentID { get; set; }
        public string PrescriptionRecordUrl { get; set; }
        public string MedicationName { get; set; }
        public DateTime PrescriptionStartDate { get; set; }
        public DateTime PrescriptionEndDate { get; set; }
        public int ReminderOffsetMinutes { get; set; }
        public PrescriptionTimingDTO PrescriptionTimingDTO { get; set; }
    }
    public class PrescriptionTimingDTO
    {
        public bool IsMorning { get; set; }
        public bool IsEvening { get; set; }
        public bool IsNight { get; set; }
    }
}

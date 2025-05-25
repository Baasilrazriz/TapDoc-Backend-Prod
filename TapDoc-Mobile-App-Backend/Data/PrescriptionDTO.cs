using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TapDoc_Mobile_App_Backend.Data
{
    public class PrescriptionDTO
    {
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public int AppointmentID { get; set; }
        public string PrescriptionRecordUrl { get; set; }
       
       
    }
  
}

using System.ComponentModel.DataAnnotations;
using TapDoc_Mobile_App_Backend.Models;

namespace TapDoc_Mobile_App_Backend.Data
{

    public class LabRecordsDTO
    {
        public int? RecordID {  get; set; }
        public int? PatientID {  get; set; }
        public int RecordType { get; set; }
        public string Title { get; set; }
        public string LabRecordUrl { get; set; }
    }

    
}

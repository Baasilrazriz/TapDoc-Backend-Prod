using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TapDoc_Mobile_App_Backend.Models
{
    public class LabRecordAnalysis
    {
        [Key]
        public int LabRecordAnalysisId { get; set; }
        [ForeignKey("LabRecords")]
        public int LabRecordId { get; set; }
        public string Result { get; set; }
        public virtual LabRecords LabRecords { get; set; }


    }
}

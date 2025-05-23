using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TapDoc_Mobile_App_Backend.Models
{
    public class DoctorQualifications
    {
        [Key]
        public int DoctorQualificationID { get; set; }
        [ForeignKey("DoctorDetails")]
        public int DoctorID { get; set; }
        public string QualificationName {  get; set; }
        public string InstituteName { get; set; }
        public string QualificationDescription { get; set; }
        public virtual DoctorDetails DoctorDetails { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace TapDoc_Mobile_App_Backend.Models
{
    public class DoctorCategories
    {
        [Key]
        public int CategoryID { get; set; } 
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        public string CategoryImage {  get; set; }

        public virtual ICollection<DoctorDetails> DoctorDetails { get; set; }

    }
}

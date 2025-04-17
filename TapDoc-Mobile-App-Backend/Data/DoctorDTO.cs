using System.ComponentModel.DataAnnotations;

namespace TapDoc_Mobile_App_Backend.Data
{
    public class DoctorDTO
    {
        public string UserName { get; set; }
        public string FullName { get; set; }

        public string Cnic { get; set; }    

        public string Gender { get; set; }

        public DateTime Dob { get; set; }

        public string Image { get; set; }
        public string Speciality { get; set; }
        public string Category { get; set; }

        public string TotalExperience { get; set; }
        public int TotalRating { get; set; }

        public string ExperienceDetails { get; set; }

        public string EducationDetails { get; set; }

        public string AvailaibilityDetails { get; set; }
        public double DoctorFee { get; set; }
        public string DoctorLocation { get; set; }

        public bool isOnline { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } 
    }
}

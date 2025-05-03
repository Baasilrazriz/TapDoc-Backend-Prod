using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Identity.Client;
using TapDoc_Mobile_App_Backend.Models;

namespace TapDoc_Mobile_App_Backend.Data
{
    public class PatientDTO
    {
        public int? PatientID { get; set; }
        public int? UserID { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Cnic { get; set; }
        public int GenderTypeID { get; set; }

        public DateTime Dob { get; set; }
        public double Weight { get; set; }
        public int WeightUnitID { get; set; }
        public double Height { get; set; }
        public int HeightUnitID { get; set; }
        public string Country {  get; set; }
        public string City {  get; set; }

        public string BloodGroup { get; set; }

        public string Image { get; set; }
        public string Address { get; set; }

      

    }
}

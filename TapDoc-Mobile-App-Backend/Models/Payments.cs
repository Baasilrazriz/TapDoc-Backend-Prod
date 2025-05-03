using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TapDoc_Mobile_App_Backend.Models
{
    public class Payments
    {
        [Key]
        public int PaymentID { get; set; }
        [ForeignKey("PatientDetails")]
        public int PatientID { get; set; }
        [ForeignKey("DoctorDetails")]
        public int DoctorID { get; set; }
        public string PaymentStripeID { get; set; }
        public string CustomerID { get; set; }
        public string PaymentUrl { get; set; }
        public int PaymentType { get; set; }
        public int PaymentStatus { get; set; }
        public double PaymentAmount { get; set; }
    }
}
        
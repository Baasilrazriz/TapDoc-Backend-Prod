namespace TapDoc_Mobile_App_Backend.Data
{
    public class PaymentRecordDTO
    {
        public int PatientID { get; set; }
        public int DoctorID { get; set; }
        public string PaymentStripeID {  get; set; }
        public string CustomerID { get; set; }
        public string PaymentUrl { get; set; }
        public int PaymentStatus { get; set; }
        public double PaymentAmount { get; set; }



    }
}

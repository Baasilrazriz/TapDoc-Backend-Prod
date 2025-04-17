namespace TapDoc_Mobile_App_Backend.Data
{
    public class AvailabilityDetailsDTO
    {
        public int UserId { get; set; } 

        public string Days { get; set; }

        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
    }
}

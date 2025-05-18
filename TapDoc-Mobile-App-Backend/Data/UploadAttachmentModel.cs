namespace TapDoc_Mobile_App_Backend.Data
{
    public class UploadAttachmentModel
    {
        public int UserID { get; set; }
        public string? AttachmentUrl { get; set; }
        public string AttachmentName { get; set; }
        public IFormFile file { get; set; }
    }
}

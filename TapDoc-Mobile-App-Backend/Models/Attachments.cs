using Amazon.S3.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TapDoc_Mobile_App_Backend.Models
{
    public class Attachments
    {
        [Key]
        public int AttachmentID {  get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentUrl { get; set; }
        public int AttachmentTypeID { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn{ get; set; }
        [ForeignKey("Users")]
        public int UserID { get; set; }

    }
}

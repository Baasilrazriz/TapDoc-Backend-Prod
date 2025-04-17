using System.ComponentModel.DataAnnotations;

namespace TapDoc_Mobile_App_Backend.Models
{
    public class Localizations
    {
        [Key]
        public int LocalizationID {  get; set; }
        public string TableName { get; set; }
        public int PrimaryKey {  get; set; }
        public string Value { get; set; }
        public DateTime CreatedOn {  get; set; }
        public DateTime UpdatedOn {  get; set; }
        public bool IsActive {  get; set; }


    }
}

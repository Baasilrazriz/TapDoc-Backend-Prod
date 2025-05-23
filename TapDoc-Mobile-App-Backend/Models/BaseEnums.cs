namespace TapDoc_Mobile_App_Backend.Models
{
    public static class BaseEnums
    {
        public static short Approved = 1;
        public static short Pending = 2;
        public static short Rejected = 3;

    }
    public static class RoleTypeEnums
    {
        public static short Patient = 1;
        public static short Doctor = 2;
        public static short Pharmacy = 3;

    }
    public static class AppointmentTypeEnums
    {
        public static short OnSite = 7;
        public static short VideoConsultation = 8;
        public static short ChatConsultation = 9;
    }
    public static class GenderTypeEnums
    {
        public static short Male = 1;
        public static short Female = 2;
    }
    public static class PaymentTypeEnums
    {
        public static short Card = 1;
    }
    public static class PaymentStatusEnums
    {
        public static short Pending = 2;
        public static short Paid = 1;
        public static short Failed = 3;
        public static short Cancelled = 4;
    }
    public static class MedicationFrequency
    {
        public static short Once = 1;
        public static short Twice = 2;
        public static short Thrice = 3;
    }
    public static class MeasuringUnitEnums
    {
        public static short KG = 1;
        public static short CM = 2;
        public static short M = 3;
        public static short FT = 4;
    }
    public static class AppointmentEnums
    {
        public static short Upcoming = 1;
        public static short Cancelled = 2;
        public static short Completed = 3;
        public static short Pending = 4;
    }
    public static class RecordTypesEnum
    {
        public static short LabRecord = 1;
        public static short PaymentRecord = 2;
        public static short PrescriptionRecord = 3;
    }
    public static class MedicationFrequencyEnums
    {
        public static int Morning = 1;
        public static int Day = 2;
        public static int Night = 3;
    }
    public static class AttachmentTypeEnums
    {
        public static int Image = 1;
        public static int Document = 2;
    }
    
}

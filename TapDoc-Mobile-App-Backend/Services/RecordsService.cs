using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using TapDoc_Mobile_App_Backend.Data;
using TapDoc_Mobile_App_Backend.Models;

namespace TapDoc_Mobile_App_Backend.Services
{
    public class RecordsService
    {
        public readonly ApplicationDbContext _dbContext;
        public RecordsService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<LabRecordsDTO> CreateUserLabReports(int PatientID, LabRecordsDTO reqDTO)
        {
            var newRecord = new Records
            {
                RecordType = 1,
                PatientID = PatientID,
            };
            await _dbContext.Records.AddAsync(newRecord);
            await _dbContext.SaveChangesAsync();

            var newLabRecord = new LabRecords
            {
                RecordID = newRecord.RecordID,
                PatientID = PatientID,
                Title = reqDTO.Title,
                LabRecordUrl = reqDTO.LabRecordUrl,
            };
            await _dbContext.LabRecords.AddAsync(newLabRecord);
            await _dbContext.SaveChangesAsync();

            return new LabRecordsDTO
            {
                RecordID = newRecord.RecordID,
                PatientID = PatientID,
                RecordType = 1,
                Title = reqDTO.Title,
                LabRecordUrl = reqDTO.LabRecordUrl
            };
        }
        public async Task<PaymentRecordDTO> CreatePatientPaymentRecord(int PatientID, PaymentRecordDTO reqDTO)
        {
            var newRecord = new Records
            {
                RecordType = 2,
                PatientID = PatientID
            };
            await _dbContext.Records.AddAsync(newRecord);
            await _dbContext.SaveChangesAsync();

            var newPaymentRecord = new Payments
            {
                PatientID = PatientID,
                DoctorID = reqDTO.DoctorID,
                PaymentStripeID = reqDTO.PaymentStripeID,
                CustomerID = reqDTO.CustomerID,
                PaymentUrl = reqDTO.PaymentUrl,
                PaymentType = 1,
                PaymentStatus = reqDTO.PaymentStatus,
                PaymentAmount = reqDTO.PaymentAmount,
            };
            await _dbContext.Payments.AddAsync(newPaymentRecord);
            return new PaymentRecordDTO
            {
                PatientID = PatientID,
                DoctorID = newPaymentRecord.DoctorID,
                PaymentStripeID = newPaymentRecord.PaymentStripeID,
                CustomerID = newPaymentRecord.CustomerID,
                PaymentAmount = newPaymentRecord.PaymentAmount,
                PaymentStatus = newPaymentRecord.PaymentStatus,
                PaymentUrl = newPaymentRecord.PaymentUrl,
            };
        }

        public async Task<PrescriptionDTO> CreatePatientPrescription(int PatientID, PrescriptionDTO reqDTO)
        {
            var newRecord = new Records
            {
                RecordType = 3,
                PatientID = PatientID
            };
            await _dbContext.AddAsync(newRecord);
            await _dbContext.SaveChangesAsync();

            var newPrescriptionRecord = new PrescriptionRecords
            {
                PatientID = PatientID,
                DoctorID = reqDTO.DoctorID,
                RecordID = newRecord.RecordID,
                AppointmentID = reqDTO.AppointmentID,
                PrescriptionRecordUrl = reqDTO.PrescriptionRecordUrl,   

            };
            await _dbContext.AddAsync(newPrescriptionRecord);
            await _dbContext.SaveChangesAsync();

            var newPrescriptionRecordDetails = new PrescriptionDetails
            {
                PrescriptionRecordID = newPrescriptionRecord.PrescriptionRecordID,
                MedicationName = reqDTO.MedicationName,
                StartDate = reqDTO.PrescriptionStartDate,
                EndDate = reqDTO.PrescriptionEndDate,
            };
            await _dbContext.AddAsync(newPrescriptionRecordDetails);
            await _dbContext.SaveChangesAsync();

            var newPrescriptionRecordDetailTimings = new PrescriptionDetailTimings
            {
                PrescriptionDetailID = newPrescriptionRecordDetails.PrescriptionDetailID,
                ReminderOffsetMinutes = reqDTO.ReminderOffsetMinutes,
            };
            await _dbContext.AddAsync(newPrescriptionRecordDetailTimings);
            await _dbContext.SaveChangesAsync();

            if(reqDTO.PrescriptionTimingDTO.IsMorning && reqDTO.PrescriptionTimingDTO.IsEvening && reqDTO.PrescriptionTimingDTO.IsNight)
            {
                var newDetails = new Localizations
                {
                    TableName = "PrescriptionDetailTimings",
                    PrimaryKey = newPrescriptionRecordDetailTimings.PrescriptionDetailTimingsID,
                    Value = ""
                };
            }

            return new PrescriptionDTO();
        }
    }
}

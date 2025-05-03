using TapDoc_Mobile_App_Backend.Models;

namespace TapDoc_Mobile_App_Backend.Services
{
    public class PaymentService
    {
        public ApplicationDbContext _dbContext;

        public PaymentService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Payments> CreatePaymentDetails(Payments reqDTO)
        {
            var payment = new Payments
            {
                DoctorID = reqDTO.DoctorID,
                PatientID = reqDTO.PatientID,
                PaymentAmount = reqDTO.PaymentAmount,
                PaymentStatus = reqDTO.PaymentStatus,
                CustomerID = reqDTO.CustomerID,
                PaymentStripeID = reqDTO.PaymentStripeID,
                PaymentType = reqDTO.PaymentType,
                PaymentUrl = reqDTO.PaymentUrl,
            };
            await _dbContext.AddAsync(payment);
            await _dbContext.SaveChangesAsync();
            return payment;
        }

    }
}

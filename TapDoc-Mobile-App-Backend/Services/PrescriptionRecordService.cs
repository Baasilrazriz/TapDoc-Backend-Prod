using TapDoc_Mobile_App_Backend.Models;

namespace TapDoc_Mobile_App_Backend.Services
{
    public class PrescriptionRecordService
    {
        public readonly ApplicationDbContext _dbContext;
        public PrescriptionRecordService (ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}

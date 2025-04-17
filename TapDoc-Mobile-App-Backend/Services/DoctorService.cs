using TapDoc_Mobile_App_Backend.Models;

namespace TapDoc_Mobile_App_Backend.Services
{
    public class DoctorService
    {
        public readonly ApplicationDbContext _dbContext;
        public DoctorService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}

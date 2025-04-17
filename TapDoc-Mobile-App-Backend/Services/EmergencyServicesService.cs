using TapDoc_Mobile_App_Backend.Models;

namespace TapDoc_Mobile_App_Backend.Services
{
    public class EmergencyServicesService
    {
        public readonly ApplicationDbContext _dbContext;
        public EmergencyServicesService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}

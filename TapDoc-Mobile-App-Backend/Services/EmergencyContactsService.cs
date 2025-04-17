using TapDoc_Mobile_App_Backend.Models;

namespace TapDoc_Mobile_App_Backend.Services
{
    public class EmergencyContactsService
    {
        public readonly ApplicationDbContext _dbContext;
        public EmergencyContactsService (ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}

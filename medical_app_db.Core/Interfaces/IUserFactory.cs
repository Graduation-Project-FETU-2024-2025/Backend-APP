using medical_app_db.Core.DTOs;
using medical_app_db.Core.Models;

namespace medical_app_db.Core.Interfaces
{
    public interface IUserFactory
    {
        ApplicationUser CreateUser(RegisterationDTO model);
        
    }
}

using medical_app_db.Core.DTOs;
using medical_app_db.Core.Models;
namespace medical_app_db.Core.Interfaces
{
    public interface IAuthService
    {
        Task<AuthModel> Login(LoginDTO model);
        Task<AuthModel> Register(RegisterationDTO model);
        Task<AuthModel> VerifytOtp(TestOtpDTO model);
    }
}

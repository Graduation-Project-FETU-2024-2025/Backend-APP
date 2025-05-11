using medical_app_db.Core.DTOs;
using medical_app_db.Core.Models;
namespace medical_app_db.Core.Interfaces
{
    public interface IAuthService
    {
        Task<AuthModel> GetOtpAsync(LoginDTO model);
        Task<AuthModel> UserLogin(UserLoginDTO model);
        Task<AuthModel> Register(RegisterationDTO model);
        Task<AuthModel> LoginAsync(TestOtpDTO model);
        Task<string?> GetPasswordResetToken(TestOtpDTO model);
        Task<bool> ResetPasswordAsync(ResetPasswordDTO model);
    }
}

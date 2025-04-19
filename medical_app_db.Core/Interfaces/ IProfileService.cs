using Microsoft.AspNetCore.Http;

namespace medical_app_db.Core.Services.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileDto> GetProfileAsync();
        Task<bool> EditProfileAsync(ProfileDto dto,IFormFile?image);
    }
}

using medical_app_db.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using medical_app_db.Core.Services.Interfaces;
using medical_app_db.Core.Models.Doctor_Module;
using medical_app_db.Core.Interfaces;
using static System.Net.Mime.MediaTypeNames;
using medical_app_db.Core.DTOs;

namespace medical_app_db.Core.Services
{
    public class ProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IImageService _imageService;

        public ProfileService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor, IImageService imageService)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _imageService = imageService;
        }

        public async Task<ProfileDto> GetProfileAsync()
        {
            var userId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Role);

            ApplicationUser? user = null;

            if (role == UserRoles.Account)
            {
                user = await _userManager.Users
                    .OfType<Account>()
                    .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            }
            else if (role == UserRoles.Doctor)
            {
                user = await _userManager.Users
                    .OfType<Doctor>()
                    .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            }
            else if (role == UserRoles.User)
            {
                user = await _userManager.Users
                    .OfType<User>()
                    .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            }
            else
            {
                throw new Exception("Unauthorized or invalid role.");
            }

            if (user == null)
                throw new Exception("User not found");

            return new ProfileDto
            {
                Name = user.Name,
                Picture = user.Picture
               
            };
        }

        public async Task<bool> EditProfileAsync(ProfileDto dto, IFormFile? image)
        {
            var userId = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Role);

            ApplicationUser? user = null;

            if (role == UserRoles.Account)
            {
                user = await _userManager.Users
                    .OfType<Account>()
                    .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            }
            else if (role == UserRoles.Doctor)
            {
                user = await _userManager.Users
                    .OfType<Doctor>()
                    .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            }
            else if (role == UserRoles.User)
            {
                user = await _userManager.Users
                    .OfType<User>()
                    .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            }
            else
            {
                throw new Exception("Unauthorized or invalid role.");
            }

            if (user == null)
                throw new Exception("User not found");

            user.Name = dto.Name;
           if (image is not null)
                user.Picture = await _imageService.UpdateImageAsync(image, user.Id);

            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded;
        }

        public Task<bool> EditProfileAsync(ProfileDto dto)
        {
            throw new NotImplementedException();
        }
    }
}

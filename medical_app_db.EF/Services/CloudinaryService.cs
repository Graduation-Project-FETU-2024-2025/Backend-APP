using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using medical_app_db.Core.Helpers;
using medical_app_db.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace medical_app_db.EF.Services
{
    public class CloudinaryService : IImageService
    {
        private readonly Cloudinary _cloudinary;
        
        public CloudinaryService(IOptions<CloudinatuSettings> cloudinaryOptions)
        {
            var account = new Account(
                cloudinaryOptions.Value.CloudName,
                cloudinaryOptions.Value.ApiKey,
                cloudinaryOptions.Value.ApiSecret
            );
            _cloudinary = new Cloudinary(account);
        }
        public async Task<string> UploadImageAsync(IFormFile image)
        {
            using var stream = image.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(image.FileName, stream),
                PublicId = $"user_profiles/{Guid.NewGuid()}"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl.ToString();
        }
    }
}

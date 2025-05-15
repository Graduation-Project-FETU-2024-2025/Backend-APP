using medical_app_db.Core.DTOs;
using medical_app_db.Core.Interfaces;
using medical_app_db.Core.Models;
using medical_app_db.Core.Models.Doctor_Module;
using medical_app_db.EF.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Internal;

namespace medical_app_api.Controllers
{
	public class SpecializationService : ISpecializationService
	{
		private readonly MedicalDbContext _context;
		private readonly IImageService _imageService;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public SpecializationService(MedicalDbContext context, IHttpContextAccessor httpContextAccessor, IImageService imageService)
		{
			_context = context;
			_httpContextAccessor = httpContextAccessor;
			_imageService = imageService;
		}
		public async Task<SpecializationDto> AddSpecializationAsync(SpecializationDto specializationDto, IFormFile icon)
		{
			var existingSpecialization = await _context
				.Specializations
				.FirstOrDefaultAsync(s => s.ArName == specializationDto.ArName && s.EnName == specializationDto.EnName);

			if (existingSpecialization is not null)
				throw new Exception("Specialization already exisits");

			var specialization = new Specialization
			{
				ArName = specializationDto.ArName,
				EnName = specializationDto.EnName,
			};

			Specialization sp = _context.Specializations.Add(specialization).Entity;

			sp.Icon = await _imageService.UploadImageAsync(icon, sp.Id);

			_context.SaveChanges();

			return new SpecializationDto
			{
				Id = specializationDto.Id,
				ArName = specializationDto.ArName,
				EnName = specializationDto.EnName,
				Icon = specialization.Icon
			};
		}

		public async Task<bool> DeleteSpecializationAsync(Guid id)
		{
			var specialization = await _context.Specializations
			.FirstOrDefaultAsync(p => p.Id == id);

			if (specialization == null)
				return false;

			await _imageService.DeleteImageAsync(id);

			_context.Specializations.Remove(specialization);
			await _context.SaveChangesAsync();

			return true;
		}

		public async Task<IEnumerable<SpecializationDto>> GetAllSpecializationsAsync(int page, int pageSize, string search)
		{
			var httpContext = _httpContextAccessor.HttpContext ?? throw new UnauthorizedAccessException("HttpContext is not available.");
			var specializations = await _context.Specializations
				.Where(b => b.ArName.Contains(search) || b.EnName.Contains(search))
				.Select(b => new SpecializationDto
				{
					Id = b.Id,
					ArName = b.ArName,
					EnName = b.EnName,
					Icon = b.Icon,
				})
				.Skip((page - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();

			return specializations;
		}

		public async Task<SpecializationDto> GetSpecializationAsync(Guid id)
		{
			var specialization = await _context.Specializations.FirstOrDefaultAsync(b => b.Id == id);
			if (specialization == null)
				return null;

			return new SpecializationDto
			{
				Id = specialization.Id,
				ArName = specialization.ArName,
				EnName = specialization.EnName,
				Icon = specialization.Icon,
			};
		}

		public async Task<SpecializationDto> UpdateSpecializationAsync(Guid id, SpecializationDto specializationDto, IFormFile? icon)
		{
			var specialization = await _context.Specializations
			.FirstOrDefaultAsync(p => p.Id == id);

			if (specialization == null)
				return null;

			if (icon is not null)
				specialization.Icon = await _imageService.UpdateImageAsync(icon, id);

			specialization.EnName = specializationDto.EnName;
			specialization.ArName = specializationDto.ArName;

			_context.SaveChanges();

			return new SpecializationDto
			{
				Id = id,
				ArName= specialization.ArName,
				EnName = specialization.EnName,
				Icon = specialization.Icon,
			};
		}
	}
}
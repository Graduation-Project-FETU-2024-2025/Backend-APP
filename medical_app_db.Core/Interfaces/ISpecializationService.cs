using medical_app_db.Core.DTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace medical_app_db.Core.Interfaces
{
	public interface ISpecializationService
	{
		Task<IEnumerable<SpecializationDto>> GetAllSpecializationsAsync(int page, int pageSize, String search);
		Task<SpecializationDto?> GetSpecializationAsync(Guid id);
		Task<SpecializationDto?> AddSpecializationAsync(SpecializationDto specializationDto, IFormFile? icon);
		Task<SpecializationDto?> UpdateSpecializationAsync(Guid id, SpecializationDto specializationDto, IFormFile? image);
		Task<bool> DeleteSpecializationAsync(Guid id);
	}
}

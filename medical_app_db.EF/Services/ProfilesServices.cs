using System;
using System.Linq;
using System.Threading.Tasks;
using medical_app_db.Core.DTOs;
using medical_app_db.Core.Interfaces;
using medical_app_db.EF.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace medical_app_db.Services
{
    public class ProfilesServices : IDoctorProfileServices
    {
        private readonly MedicalDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfilesServices(MedicalDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<DoctorDTO> GetDoctorProfileAsync()
        {
            var accountId = GetCurrentAccountId();
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.ApplicationUserId == accountId);

            if (doctor == null)
                throw new KeyNotFoundException("Doctor profile not found");

            return new DoctorDTO
            {
                Id = doctor.Id,
                ApplicationUserId = doctor.ApplicationUserId,
                EN_Name = doctor.EN_Name,
                AR_Name = doctor.AR_Name,
                DateOfBirth = doctor.DateOfBirth,
                Picture = doctor.Picture,
                Specialization = doctor.Specialization,
                Gender = doctor.Gnder,
                SSN = doctor.SSN
            };
        }

        public async Task<DoctorDTO> UpdateDoctorProfileAsync(DoctorDTO doctorDto)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == doctorDto.Id);

            if (doctor == null)
                throw new KeyNotFoundException("Doctor profile not found");

            if (!string.IsNullOrEmpty(doctorDto.EN_Name))
                doctor.EN_Name = doctorDto.EN_Name;

            if (!string.IsNullOrEmpty(doctorDto.AR_Name))
                doctor.AR_Name = doctorDto.AR_Name;

            if (!string.IsNullOrEmpty(doctorDto.Picture))
                doctor.Picture = doctorDto.Picture;

            await _context.SaveChangesAsync();

            return new DoctorDTO
            {
                Id = doctor.Id,
                ApplicationUserId = doctor.ApplicationUserId,
                EN_Name = doctor.EN_Name,
                AR_Name = doctor.AR_Name,
                DateOfBirth = doctor.DateOfBirth,
                Picture = doctor.Picture,
                Specialization = doctor.Specialization,
                Gender = doctor.Gnder,
                SSN = doctor.SSN
            };
        }

        private Guid GetCurrentAccountId()
        {
            var accountId = _httpContextAccessor.HttpContext?.User?.FindFirst("AccountId")?.Value;
            if (accountId == null || !Guid.TryParse(accountId, out var guidAccountId))
                throw new UnauthorizedAccessException("Invalid Account ID");
            return guidAccountId;
        }
    }
}


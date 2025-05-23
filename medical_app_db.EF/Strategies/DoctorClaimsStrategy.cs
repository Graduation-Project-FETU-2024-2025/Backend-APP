﻿using medical_app_db.Core.Interfaces;
using medical_app_db.Core.Models;
using medical_app_db.Core.Models.Doctor_Module;
using medical_app_db.EF.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace medical_app_db.EF.Strategies
{
    public class DoctorClaimsStrategy : IUserClaimsStrategy
    {
        private readonly MedicalDbContext _context;

        public DoctorClaimsStrategy(MedicalDbContext context)
        {
            _context = context;
        }
        public List<Claim> GetClaims(ApplicationUser user)
        {
            var clinicId = _context.Set<DoctorClinic>()
                                   .AsNoTracking()
                                   .Where(dc => dc.DoctorId == user.Id)
                                   .Select(dc => dc.ClinicId)
                                   .FirstOrDefault();

            var claims = new List<Claim>();
            if (user is Doctor doctor)
            {
                claims.Add(new Claim("ClinicId", clinicId.ToString() ?? ""));

                if (!string.IsNullOrEmpty(doctor.Specialization))
                {
                    claims.Add(new Claim("Specialization", doctor.Specialization));
                }
            }
            return claims;
        }
    }
}

using medical_app_db.Core.DTOs;
using medical_app_db.Core.Models.Doctor_Module;
using medical_app_db.Core.Models;
using medical_app_db.Core.Interfaces;

namespace medical_app_db.EF.Factory
{
    public class UserFactory : IUserFactory
    {
        public ApplicationUser CreateUser(RegisterationDTO model)
        {
            return model.Role switch
            {
                UserRoles.Account => new Account
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Name = model.FullName,
                    PharmacyId = model.PharmacyId,
                    Gnder = model.Gnder,
                    SSN = model.SSN,
                    PhoneNumber = model.Phone,
                    DateOfBirth = model.DateOfBirth,
                    Id = Guid.NewGuid()
                },
                UserRoles.Doctor => new Doctor
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Name = model.FullName,
                    SpecializationId = model.SpecializationId,
                    DoctorClinic = new DoctorClinic
                    {
                        ClinicId = model.ClinicId
                    },
                    Gnder = model.Gnder,
                    SSN = model.SSN,
                    PhoneNumber = model.Phone,
                    DateOfBirth = model.DateOfBirth,
                    Id = Guid.NewGuid()
                },
                _ => new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Name = model.FullName,
                    Gnder = model.Gnder,
                    SSN = model.SSN,
                    PhoneNumber = model.Phone,
                    DateOfBirth = model.DateOfBirth,
                    Id = Guid.NewGuid()
                }
            };
        }
    }
}


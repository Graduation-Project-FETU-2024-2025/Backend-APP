using Microsoft.EntityFrameworkCore;
using medical_app_db.Core.DTOs;
using medical_app_db.Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using medical_app_db.Core.Models.Doctor_Module;
using medical_app_db.EF.Data;

namespace medical_app_db.EF.Services
{
    public class ClinicStatisticsService : IClinicStatisticsService
    {
        private readonly MedicalDbContext _context;

        public ClinicStatisticsService(MedicalDbContext context)
        {
            _context = context;
        }

        public async Task<ClinicStatisticsDTO> GetClinicStatisticsAsync(Guid clinicId)
        {
            var appointments = await _context.Appointments
                .Where(a => a.ClinicId == clinicId)
                .ToListAsync();
            return new ClinicStatisticsDTO
            {
                ReVisitCount = appointments.Count(a => a.Type == AppointmentType.ReVisit),
                NewVisitCount = appointments.Count(a => a.Type == AppointmentType.NewVisit),
                CheckupCount = appointments.Count(a => a.Type == AppointmentType.Checkup),
                PendingAppointments = appointments.Count(a => a.Status == AppointmentStatus.Pending),
                ConfirmedAppointments = appointments.Count(a => a.Status == AppointmentStatus.Accepted),
                CanceledAppointments = appointments.Count(a => a.Status == AppointmentStatus.Decliened),
                TotalIncome = appointments
                    .Where(a => a.Status == AppointmentStatus.Completed) 
                    .Sum(a => a.Price) 
            };

        }
    }
}

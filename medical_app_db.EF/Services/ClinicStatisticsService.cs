using Microsoft.EntityFrameworkCore;
using medical_app_db.Core.DTOs;
using medical_app_db.Core.Interfaces;
using medical_app_db.Infrastructure.Data;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace medical_app_db.EF.Services
{
    public class ClinicStatisticsService : IClinicStatisticsService
    {
        private readonly DoctorDbContext _context;

        public ClinicStatisticsService(DoctorDbContext context)
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
                ReVisitCount = appointments.Count(a => a.Status == "ReVisit"),
                NewVisitCount = appointments.Count(a => a.Status == "NewVisit"),
                CheckupCount = appointments.Count(a => a.Status == "Checkup"),
                PendingAppointments = appointments.Count(a => a.Status == "pending"),
                ConfirmedAppointments = appointments.Count(a => a.Status == "confirmed"),
                CanceledAppointments = appointments.Count(a => a.Status == "canceled"),
                TotalIncome = appointments.Any() ? appointments.Sum(a => a.Price) : 0
            };
        }
    }
}

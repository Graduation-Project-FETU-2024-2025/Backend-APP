using medical_app_db.Core.DTOs;
using medical_app_db.Core.Models;
using medical_app_db.Core.Models.Doctor_Module;

namespace medical_app_db.Core.Interfaces
{
    public interface IAppointmentService
    {
        Task<IReadOnlyList<Appointment>> GetAppointmentsAsync(DateTime? appointmentDate);
        Task<Appointment?> GetAppointmentAsync(Guid id);
        Task<bool> AcceptApointment(Guid id);
        Task<bool> DeclineApointment(Guid id);
        Task<Prescription?> AddPrescriptionAsync(PrescriptionDTO model);

        Task<AppointmentDates> UpdateAppointmentDateAsync(Guid id, AppointmentDateDTO appointmentDate);
	}
}

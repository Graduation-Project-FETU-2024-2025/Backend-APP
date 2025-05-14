using medical_app_db.Core.DTOs;
using medical_app_db.Core.Models;
using medical_app_db.Core.Models.Doctor_Module;

namespace medical_app_db.Core.Interfaces
{
    public interface IAppointmentService
    {
        Task<IReadOnlyList<AppointmentDTO>> GetAppointmentsAsync(DateTime? appointmentDate, AppointmentStatus? status, AppointmentType? type);
        Task<AppointmentDTO?> GetAppointmentAsync(Guid id);
        Task<bool> AcceptApointment(Guid id);
        Task<bool> DeclineApointment(Guid id);
        Task<Prescription?> AddPrescriptionAsync(PrescriptionDTO model);

        Task<AppointmentDateDTO> UpdateAppointmentDateAsync(Guid id, AppointmentDateDTO appointmentDate);
        Task<List<AppointmentDateDTO>> GetAppointmentDates(); 
	}
}

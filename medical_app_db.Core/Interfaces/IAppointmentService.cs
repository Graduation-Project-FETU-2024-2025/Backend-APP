using medical_app_db.Core.DTOs;
using medical_app_db.Core.Models;
using medical_app_db.Core.Models.Doctor_Module;
using Microsoft.AspNetCore.Http;

namespace medical_app_db.Core.Interfaces
{
    public interface IAppointmentService
    {
        Task<IReadOnlyList<AppointmentDTO>> GetAppointmentsAsync(DateTime? appointmentDate, AppointmentStatus? status, AppointmentType? type);
        Task<IReadOnlyList<AppointmentDTO>> GetUserAppointmentsAsync(DateTime? appointmentDate, AppointmentStatus? status, AppointmentType? type, Guid user_id);
        Task<IReadOnlyList<AppointmentDTO>> GetUserInCompleteAppointmentsAsync(DateTime? appointmentDate, AppointmentType? type, Guid user_id);
        Task<AppointmentDTO?> GetAppointmentAsync(Guid id);
        Task<bool> AcceptApointment(Guid id);
        Task<bool> DeclineApointment(Guid id);
        Task<Prescription?> AddPrescriptionAsync(PrescriptionDTO model);

        Task<AppointmentDateDTO> AddAppointmentDateAsync(AppointmentDateDTO appointmentDate);
        Task<List<AppointmentDateDTO>> GetAppointmentDates(Guid clinicId); 

        Task<AppointmentDTO> createAppointmentAsync(AppointmentDTO appointmentDTO,IFormFile? file);
		Task<AppointmentDTO> updateAppointmentAsync(AppointmentDTO appointmentDTO);

		Task<bool> deleteAppointmentAsync(Guid id);

	}
}

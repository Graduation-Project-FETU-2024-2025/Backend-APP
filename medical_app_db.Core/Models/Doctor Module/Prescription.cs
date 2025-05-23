﻿namespace medical_app_db.Core.Models.Doctor_Module
{
    public class Prescription
    {
        public Guid Id { get; set; }
        public string? Tests { get; set; }
        public string? Notes { get; set; }
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;
        public Guid AppointmentId { get; set; }
        public Appointment Appointment { get; set; } = null!;
        public ICollection<PrescriptionProduct>? PrescriptionProducts { get; set; }
    }
}

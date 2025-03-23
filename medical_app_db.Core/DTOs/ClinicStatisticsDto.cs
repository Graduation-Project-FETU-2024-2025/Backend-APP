namespace medical_app_db.Core.DTOs
{
    
        public class ClinicStatisticsDTO
        {
            public int ReVisitCount { get; set; }
            public int NewVisitCount { get; set; }
            public int CheckupCount { get; set; }
            public int PendingAppointments { get; set; }
            public int ConfirmedAppointments { get; set; }
            public int CanceledAppointments { get; set; }
            public decimal TotalIncome { get; set; }
        }
    }





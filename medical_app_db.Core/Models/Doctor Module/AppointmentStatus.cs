using System.Runtime.Serialization;

namespace medical_app_db.Core.Models.Doctor_Module
{
    public enum AppointmentStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Accepted")]
        Accepted,
        [EnumMember(Value = "Decliened")]
        Decliened,
        [EnumMember(Value = "Completed")]
        Completed
    }
}

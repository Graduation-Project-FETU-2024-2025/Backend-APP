using System.Runtime.Serialization;
namespace medical_app_db.Core.Models.Doctor_Module
{
    public enum AppointmentType
    {
        [EnumMember(Value = "ReVisit")]
        ReVisit,
        [EnumMember(Value = "NewVisit")]
        NewVisit,
        [EnumMember(Value = "Checkup")]
        Checkup
    }
}
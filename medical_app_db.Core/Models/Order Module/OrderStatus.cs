using System.Runtime.Serialization;

namespace medical_app_db.Core.Models.Order_Module
{
    public enum OrderStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,
        [EnumMember(Value = "Confirmed")]
        Confirmed,
        [EnumMember(Value = "Delivered")]
        Delivered,
        [EnumMember(Value = "Not Delivered")]
        NotDelivered,
    }
}

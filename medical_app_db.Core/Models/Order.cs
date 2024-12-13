using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace medical_app_db.Core.Models
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        public DateTime Date { get; set; }
        [Required]
        [MaxLength(50)]
        public string State { get; set; }
        public decimal DeliveryPrice { get; set; }
        public decimal TotalCart { get; set; }
        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public int BranchId { get; set; }

        [ForeignKey(nameof(BranchId))]
        public Branch Branch { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}






using System.ComponentModel.DataAnnotations;

namespace E_commerceFinalAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public string status { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        public int UserId { get; set; }
        public User ?User { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }

    }
}

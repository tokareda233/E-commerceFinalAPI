using System.ComponentModel.DataAnnotations;

namespace E_commerceFinalAPI.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100,MinimumLength =2)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string ?Description { get; set; }
        [Required]
        [Range(0.01,1000000)]
        public decimal Price { get; set; }
        [Required]
        [Range(0,1000000)]
        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }
        public Category ?Category { get; set; }

        public ICollection<OrderItem> orderItems { get; set; }
    }
}

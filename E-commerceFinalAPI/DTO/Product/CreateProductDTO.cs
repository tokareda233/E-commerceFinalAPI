using System.ComponentModel.DataAnnotations;

namespace E_commerceFinalAPI.DTO.Product
{
    public class CreateProductDTO
    {
       
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        [Required]
        [Range(0.01, 1000000)]
        public decimal Price { get; set; }
        [Required]
        [Range(0, 1000000)]
        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }
    }
}

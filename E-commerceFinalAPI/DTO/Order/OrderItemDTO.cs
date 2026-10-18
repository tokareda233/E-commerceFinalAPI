using System.ComponentModel.DataAnnotations;

namespace E_commerceFinalAPI.DTO.Order
{
    public class OrderItemDTO
    {
        [Required]
        public int ProductId { get; set; }
        [Range(1,1000)]
        public int Quantity { get; set; }
    }
}

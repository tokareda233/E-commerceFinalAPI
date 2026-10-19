using System.ComponentModel.DataAnnotations;

namespace E_commerceFinalAPI.DTO.Order
{
    public class CreateOrderDTO
    {
        [Required]
        public List<OrderItemDTO> OrderItems { get; set; }
        // toka test
    }
}

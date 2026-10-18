using System.ComponentModel.DataAnnotations;

namespace E_commerceFinalAPI.DTO.Product
{
    public class GetProductDTO
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
       
        public string? Description { get; set; }
        
        public decimal Price { get; set; }
       
        public int StockQuantity { get; set; }

        public int CategoryId { get; set; }
    }
}

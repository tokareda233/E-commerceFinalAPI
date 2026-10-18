using System.ComponentModel.DataAnnotations;

namespace E_commerceFinalAPI.DTO.Category
{
    public class GetCategoryDTO
    {
        public int Id { get; set; }
       
        public string Name { get; set; }
      
        public string? Description { get; set; }
    }
}

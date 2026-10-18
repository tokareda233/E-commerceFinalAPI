using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace E_commerceFinalAPI.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Category
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100,MinimumLength =2)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string ?Description { get; set; }
        public ICollection<Product> ?Products { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace E_commerceFinalAPI.Models
{
    [Index(nameof(Email) ,IsUnique =true)]
    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100,MinimumLength =3)]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]

        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        public ICollection<Order> ?Orders { get; set; }
    }
}

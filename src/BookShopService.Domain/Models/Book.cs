using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookShopService.Domain.Models
{
    public class Book
    {
        [Key]
        [Required]
        public Int64 Id { get; set; }
        [Required]
        public string Author { get; set; } = string.Empty;
        [Required]
        [MinLength(1)]
        public string Title { get; set; } = string.Empty;
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace BookShopService.API.DTOs
{
    /// <summary>
    /// DTO book object without a unique identifier
    /// </summary>
    public class CommonBook
    {
        /// <summary>
        /// The book's title.
        /// </summary>
        /// <example>Journey to the Center of the Earth</example>
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// The book's author.
        /// </summary>
        /// <example>Jules Verne</example>
        [Required(ErrorMessage = "Author is required")]
        public string Author { get; set; } = string.Empty;

        /// <summary>
        /// The book's price.
        /// </summary>
        /// <example>10.99</example>
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
    }
}
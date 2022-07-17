namespace BookShopService.API.DTOs
{
    /// <summary>
    /// DTO book object with a unique identifier
    /// </summary>
    public class UniqueBook
    {
        public Int64 Id { get; set; }

        /// <summary>
        /// The book's title.
        /// </summary>
        /// <example>Journey to the Center of the Earth</example>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// The book's price.
        /// </summary>
        /// <example>10.99</example>
        public string Author { get; set; } = string.Empty;

        /// <summary>
        /// The book's price.
        /// </summary>
        /// <example>10.99</example>
        public decimal Price { get; set; }
    }
}
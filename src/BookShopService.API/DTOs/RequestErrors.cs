namespace BookShopService.API.DTOs
{
    /// <summary>
    /// DTO used in id specific get and put requests to pass back bad request errors i.e. missing title
    /// </summary>
    public class RequestErrors
    {
        /// <example>["Title is required"]</example>
        public string[] errors { get; set;}
    }
}
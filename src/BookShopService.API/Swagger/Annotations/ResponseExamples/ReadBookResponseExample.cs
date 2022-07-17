using Swashbuckle.AspNetCore.Filters;
using BookShopService.API.DTOs;

namespace BookShopService.API.Swagger.Annotations.ResponseExamples
{
    public class ReadBookResponseExample : IExamplesProvider<UniqueBook>
    {
        public UniqueBook GetExamples()
        {
            return new UniqueBook
            {
                Id = 4,
                Title = "Journey to the Center of the Earth",
                Author = "Jules Verne",
                Price = 10.99M
            };
        }
    }
}

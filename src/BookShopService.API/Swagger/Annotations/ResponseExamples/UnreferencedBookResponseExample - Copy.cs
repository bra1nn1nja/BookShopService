using Swashbuckle.AspNetCore.Filters;
using BookShopService.API.DTOs;

namespace BookShopService.API.Swagger.Annotations.ResponseExamples
{
    public class UnreferencedBookResponseExample : IExamplesProvider<CommonBook>
    {
        public CommonBook GetExamples()
        {
            return new CommonBook
            {
                Title = "Journey to the Center of the Earth",
                Author = "Jules Verne",
                Price = 10.99M
            };
        }
    }
}

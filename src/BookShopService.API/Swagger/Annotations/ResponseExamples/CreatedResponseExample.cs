using Swashbuckle.AspNetCore.Filters;
using BookShopService.API.DTOs;

namespace BookShopService.API.Swagger.Annotations.ResponseExamples
{
    public class CreatedResponseExample : IExamplesProvider<CreatedBook>
    {
        public  CreatedBook GetExamples()
        {
            return new CreatedBook { Id = 4 };
        }
    }
}

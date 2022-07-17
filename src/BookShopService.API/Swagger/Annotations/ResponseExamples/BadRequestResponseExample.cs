using Swashbuckle.AspNetCore.Filters;
using BookShopService.API.DTOs;

namespace BookShopService.API.Swagger.Annotations.ResponseExamples
{
    public class BadRequestResponseExample: IExamplesProvider<RequestErrors>
    {
        public RequestErrors GetExamples()
        {
            return new RequestErrors { errors = new string[] {"Title is required"} };
        }
    }
}

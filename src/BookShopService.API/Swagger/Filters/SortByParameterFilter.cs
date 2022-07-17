using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BookShopService.API.Swagger.Filters
{
    public class SortByParameterFilter
    : IParameterFilter
    {
        public void Apply(OpenApiParameter parameter, ParameterFilterContext context)
        {
            if (parameter.Description == parameter.Schema.Description)
                parameter.Schema.Description = null;

            if (parameter.Name.Equals("sortby", StringComparison.InvariantCultureIgnoreCase))
            {
                parameter.Schema.Enum = new List<IOpenApiAny>
                {
                    new OpenApiString("title"),
                    new OpenApiString("author"),
                    new OpenApiString("price")
                };
            }
        }
    }
}


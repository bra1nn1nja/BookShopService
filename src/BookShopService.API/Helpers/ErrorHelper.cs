using BookShopService.API.DTOs;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BookShopService.API.Helpers
{
    public static class ErrorHelper
    {
        public static RequestErrors ExtractFromState(ModelStateDictionary state)
        {
            return new RequestErrors
                { 
                    errors = state.Keys
                    .Where(k => state[k].Errors.Count > 0)
                    .Select(k => state[k].Errors[0].ErrorMessage).ToArray()
                };
        }
    }
}
using API.Common.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace API.Common.Extensions
{
    public static class CustomExceptionHandler
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}

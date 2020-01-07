using Microsoft.AspNetCore.Builder;

namespace API.Extensions
{
    /// <summary>
    /// Class for implementation custom exception handling Middleware
    /// </summary>
    public static class ExceptionMiddlewareExtensions
    {
        public static void UseCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}

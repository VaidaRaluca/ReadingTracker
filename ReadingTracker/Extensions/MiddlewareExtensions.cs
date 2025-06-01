using Microsoft.AspNetCore.Builder;

namespace ReadingTracker.API.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}

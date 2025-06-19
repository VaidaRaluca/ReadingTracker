using global::ReadingTracker.API.Middlewares;
using Microsoft.AspNetCore.Builder;
namespace ReadingTracker.Api.Middlewares
{

        public static class MiddlewareExtensions
        {
            public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
            {
                return builder.UseMiddleware<ErrorHandlingMiddleware>();
            }
        }
}

using RequestCorrelation;

namespace Microsoft.AspNetCore.Builder
{
    public static class RequestCorrelationMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestCorrelation(this IApplicationBuilder app) => app.UseMiddleware<RequestCorrelationMiddleware>();
    }
}

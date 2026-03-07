
using System.Diagnostics;

namespace Restaurants.API.Middlewares
{
    public class TimeLoggingMiddleware : IMiddleware
    {
        private readonly ILogger<TimeLoggingMiddleware> _logger;
        public TimeLoggingMiddleware(ILogger<TimeLoggingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            await next.Invoke(context);
            stopwatch.Stop();
            var elapsedMs = stopwatch.ElapsedMilliseconds;
            if (elapsedMs / 1000 > 4)
            {
                _logger.LogInformation(
                    "Request {Method} {Path} executed in {ElapsedMilliseconds}ms with status code {StatusCode}",
                    context.Request.Method,
                    context.Request.Path,
                    elapsedMs / 1000,
                    context.Response.StatusCode
                );
            }
        }
    }
}

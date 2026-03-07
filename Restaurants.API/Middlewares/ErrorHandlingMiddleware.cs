
using Restraurants.Domain.Exceptions;

namespace Restaurants.API.Middlewares
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(ForbidException)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("access forbidden");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex , ex.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("something went wrong :(");
            }
        }
    }
}

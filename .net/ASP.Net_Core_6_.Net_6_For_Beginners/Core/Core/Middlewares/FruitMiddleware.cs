using Core.Models;
using Microsoft.Extensions.Options;

namespace Core.Middlewares
{
    public class FruitMiddleware
    {
        private RequestDelegate next;
        private FruitOptions options;

        public FruitMiddleware()
        {

        }

        public FruitMiddleware(RequestDelegate next, IOptions<FruitOptions> options)
        {
            this.next = next;
            this.options = options.Value;
        }

        public async Task Invoke(HttpContext context, ILogger<FruitMiddleware> logger)
        {
            if (context.Request.Path == "/fruit")
            {

                // This will write log to the debug CLI
                logger.LogDebug($"Started processing for {context.Request.Path}");
                await context.Response.WriteAsync($"{options.Name}, {options.Color}");
                // This will write log to the debug CLI
                logger.LogDebug($"End processing for {context.Request.Path}");
            }
            else
            {
                await next(context);
                // This will write log to the debug CLI
                logger.LogDebug($"/fruit was not requested {context.Request.Path}");
            }

            // This will write log to the debug CLI
            logger.LogDebug($"/fruit was/was not requested {context.Request.Path}");

        }
    }
}

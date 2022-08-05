using Core.Services;

namespace Core.Middlewares
{
    public class CustomMiddlewareTransient
    {
        private readonly RequestDelegate next;
        private readonly IResponseFormatter formatter;

        public CustomMiddlewareTransient(RequestDelegate next, IResponseFormatter formatter)
        {
            this.next = next;
            this.formatter = formatter;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/transientmiddleware")
            {
                await formatter.Format(context, "\n Custom Middleware Formatter");
            }
            else
            {
                await next(context);
            }
        }
    }
}

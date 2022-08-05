using Core.Services;

namespace Core.Middlewares
{
    public class CustomMiddleware
    {

        private readonly RequestDelegate next;
        //private readonly IResponseFormatter formatter;

        public CustomMiddleware(RequestDelegate next/*, IResponseFormatter formatter*/)
        {
            this.next = next;
            //this.formatter = formatter;
        }

        public async Task Invoke(HttpContext context, IResponseFormatter formatter1, IResponseFormatter formatter2,
            IResponseFormatter formatter3)
        {
            if (context.Request.Path == "/middleware")
            {
                // EACH OBJECT SHOULD SHOW DIFFERENT GUID SINCE AddTansient() IS CALLED
                // LINK : AddTransient<IResponseFormatter, GuidService>() => IResponseFormatter IMPLEMENTATION
                await formatter1.Format(context, string.Empty);
                await formatter2.Format(context, string.Empty);
                await formatter3.Format(context, string.Empty);
            }
            else
            {
                await next(context);
            }
        }

    }
}

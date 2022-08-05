using Core.Services;

namespace Core.Middlewares
{
    public class CustomEndpoint
    {
        public static async Task Endpoint(HttpContext context, IResponseFormatter formatter)
        {
            // DEPENDENCY INJECTION WITHOUT CONSTRUCTOR
            //IResponseFormatter formatter = context.RequestServices.GetRequiredService<IResponseFormatter>();
            // ALTERNATIVELY, SET THE INTERFACE AS AN ARGUMENT


            await formatter.Format(context, "\nCustom Endpoint");
        }
    }
}

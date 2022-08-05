namespace Core.Middlewares
{
    public class Middleware
    {
        private RequestDelegate _next;

        public Middleware()
        {

        }

        public Middleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if(context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
                {
                if (!context.Response.HasStarted)
                {
                    context.Response.ContentType = "text/plain";
                }
                
                await context.Response.WriteAsync("From Middlesware class... \n");
            }

            if(_next != null)
            {
                await _next(context);
            }
        }

    }
}

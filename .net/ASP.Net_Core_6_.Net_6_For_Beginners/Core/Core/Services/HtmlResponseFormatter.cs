namespace Core.Services
{
    public class HtmlResponseFormatter : IResponseFormatter
    {
        private int responseCounter = 0;
        public async Task Format(HttpContext context, string content)
        {
            context.Response.ContentType = "text/html";
            await context.Response.WriteAsync($"\nFormatted response for HTML: {++responseCounter}\n{content}");
        }
    }
}

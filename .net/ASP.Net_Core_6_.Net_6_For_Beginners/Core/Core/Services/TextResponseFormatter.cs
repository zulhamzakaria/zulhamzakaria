namespace Core.Services
{
    public class TextResponseFormatter : IResponseFormatter
    {
        private int responseCounter = 0;
        public async Task Format(HttpContext context, string content)
        {
           await context.Response.WriteAsync($"\nResponse for TEXT: {++ responseCounter}\n{content}");
        }
    }
}

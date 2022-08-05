using Microsoft.AspNetCore.Mvc;

namespace Core.Components
{
    // Decorate with [ViewComponent] to not use the -ViewComponent suffix
    // For view, always use this structure: Views/Shared/Components/[Name_of_VC]/Default.cshtml
    [ViewComponent]
    public class PageSize:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            HttpClient client = new();
            HttpResponseMessage response = await client.GetAsync("http://google.com");
            return View(response.Content.Headers.ContentLength);
        }
    }
}

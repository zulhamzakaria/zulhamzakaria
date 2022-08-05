using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Core.TagHelpers
{
    [HtmlTargetElement("tablehead")]
    public class TableHeadTagHelper:TagHelper
    {
        public string BgColor { get; set; } = "dark";

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            // Create element
            output.TagName = "thead";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.SetAttribute("class", $"bg-{BgColor} text-center text-white");

            string content = (await output.GetChildContentAsync()).GetContent();

            // C# string formatting for content
            //output.Content.SetHtmlContent($"<tr><th colspan=3>{content}</th></tr>");

            // TagBuilder is more robust than C# string formatting shown above
            TagBuilder header = new TagBuilder("th");
            header.Attributes["colspan"] = "3";
            header.InnerHtml.Append(content);

            TagBuilder row = new TagBuilder("tr");
            row.InnerHtml.AppendHtml(header);

            output.Content.SetHtmlContent(row);
        }
    }
}

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Core.TagHelpers
{
    /*
        TagHelper extends TagHelper of Razor.TagHelpers
        Override Process() to implement subclass behaviour
        TagHelper must also be registered with the add tag helper directive (_ViewImports.cshtml)
        TagHelper can be used to replace custom elements
        TagBuilder class is more robust than c# string formatting to create content
    */

    // TagHelepr targeting multiple elements
    [HtmlTargetElement("tr", Attributes="bg-color, text-color", ParentTag ="thead")]
    [HtmlTargetElement("td", Attributes="bg-color, text-color", ParentTag ="thead")]
    public class TrTagHelper:TagHelper
    {
        public string BgColor { get; set; } = "dark";
        public string TextColor { get; set; } = "white";

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //base.Process(context, output);
            output.Attributes.SetAttribute("class", $"bg-{BgColor} text-center text-{TextColor}");
        }
    }
}

using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC_Helpers.TagHelpers
{
    public class RandomNumberTagHelper: TagHelper
    {
        public int From { get; set; }
        public int To { get; set; }
        public string Color { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            Random rand = new Random();
            var number = rand.Next(From, To);

            output.PreElement.SetHtmlContent("<br>");

            output.Attributes.SetAttribute("style", $"color:{Color};");

            output.Content.SetContent(number.ToString());
        }
    }
}

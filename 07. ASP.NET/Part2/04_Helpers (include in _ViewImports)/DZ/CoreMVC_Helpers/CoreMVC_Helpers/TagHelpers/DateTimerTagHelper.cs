using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC_Helpers.TagHelpers
{
    public class DateTimerTagHelper: TagHelper
    {
        public bool ShowTime { get; set; }
        public string Color { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // название результирующего тега
            output.TagName = "div";

            // тип тега (одиночный или двойной)
            output.TagMode = TagMode.StartTagAndEndTag;

            var now = DateTime.Now;
            var dt = "";
            if (ShowTime)
                dt = now.ToString("dd.MM.yyyy HH:mm:ss");
            else
                dt = now.ToString("dd.MM.yyyy");

            // элемент, который генерируется перед результирующим
            output.PreElement.SetHtmlContent("<br>");

            output.Attributes.SetAttribute("style", $"color:{Color};");

            output.Content.SetHtmlContent($"<div>Текущая дата: {dt}</div>");
        }
    }
}

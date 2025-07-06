using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main.TagHelpers
{
	public class PagingTagHelper : TagHelper
	{
		public int AllCount { get; set; }
		public int OnePageCount { get; set; }
		public int CurrentPage { get; set; }
		public Func<int, string> UrlFunc { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
            string HTMLcontent = "";

            int pageCount = AllCount / OnePageCount + (AllCount % OnePageCount == 0 ? 0 : 1);

			for (int i = 1; i <= pageCount; i++)
			{
				HTMLcontent += $"<a style='text-decoration: none; color: #000; display: flex; justify-content: center; padding: 2px 0px; width: 30px; {(i == CurrentPage ? "background: #4c66eb;" : "")}' href='{UrlFunc(i)}'>{i}</a>";
			}

			bool st = context.AllAttributes.TryGetAttribute("style", out TagHelperAttribute attr);
            output.TagName = "div";
			output.Attributes.SetAttribute("style", $"width: max-content; display: flex; overflow: hidden; border-radius: 20px; {(st?attr.Value:"")}");
			output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.SetHtmlContent(HTMLcontent);
        }
    }
}

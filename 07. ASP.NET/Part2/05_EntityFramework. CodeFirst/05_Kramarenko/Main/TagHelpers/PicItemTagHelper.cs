using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main.TagHelpers
{
	public class PicItemTagHelper : TagHelper
	{
		public string ContainerClass { get; set; } = "";
		public string ShowHandler { get; set; } = "";

		public int ImgID { get; set; }
		public string ImgSrc { get; set; } = "";

		public string DeleteButtonClass { get; set; } = "";
		public string DeleteHandler { get; set; } = "";

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "div";
			output.TagMode = TagMode.StartTagAndEndTag;
			output.Attributes.SetAttribute("id", ImgID);
			output.Attributes.SetAttribute("class", ContainerClass);
			output.Attributes.SetAttribute("onclick", $"{ShowHandler}({ImgID})");

			string HTMLcontent = $"<img src=\"{ImgSrc}\" alt=\"photo\" />" +
				$"<input type=\"button\" onclick=\"{DeleteHandler}({ImgID})\" class=\"{DeleteButtonClass}\" value=\"delete\" />";

			output.Content.SetHtmlContent(HTMLcontent);
		}
	}
}

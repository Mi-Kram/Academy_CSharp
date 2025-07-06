using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main.TagHelpers
{
	public class PicCategoryTagHelper : TagHelper
	{
		public string InputClass { get; set; } = "";
		public string InputID { get; set; } = "";
		public string Onchange { get; set; } = "";
		public string Group { get; set; } = "";
		public bool IsChecked { get; set; } = false;
		public string CategoryID { get; set; } = "";

		public string LabelClass { get; set; } = "";
		public string Content { get; set; } = "";


		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			string HTMLcontent = $"<input class=\"{InputClass}\" value=\"{CategoryID}\" id=\"{InputID}\" onchange=\"{Onchange}\" type=\"radio\" name=\"{Group}\" {(IsChecked?"Checked":"")} /><label class=\"{LabelClass}\" for=\"{InputID}\">{Content}</label>";
			output.Content.SetHtmlContent(HTMLcontent);
		}
	}
}

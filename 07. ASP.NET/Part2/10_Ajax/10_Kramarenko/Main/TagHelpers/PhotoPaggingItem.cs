using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main.TagHelpers
{
	public class PhotoPaggingItem : TagHelper
	{
		public int Value { get; set; } = 1;
		public string InputClass { get; set; } = "";
		public string LabelClass { get; set; } = "";
		public int SelectedValue { get; set; } = 1;
		public string Onchange { get; set; } = "";

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			string HTMLcontent = $"<input class=\"{InputClass}\" value=\"{Value}\" id=\"ppg{Value}\" onchange=\"{Onchange}\" type=\"radio\" name=\"ppg\" {(Value == SelectedValue ? "Checked" : "")} /><label class=\"{LabelClass}\" for=\"ppg{Value}\">{Value}</label>";
			output.Content.SetHtmlContent(HTMLcontent);
		}
	}
}

using Main.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Main.CarHelpers
{
	public static class CarTablePaging
	{
		public static MvcHtmlString TablePaging(this HtmlHelper helper, PageInfo pageInfo, Func<int, string> pageUrl)
		{
			if (pageInfo.TotalPages <= 1) return MvcHtmlString.Create("");
			string result = "";
			for (int i = 1; i <= pageInfo.TotalPages; i++)
			{
				TagBuilder tag = new TagBuilder("a");

				tag.MergeAttribute("href", pageUrl(i));
				tag.InnerHtml = i.ToString();

				tag.AddCssClass("btn");
				tag.AddCssClass(i == pageInfo.CurrentPage ? "btn-primary" : "btn-default");

				result += tag.ToString();
			}
			return MvcHtmlString.Create(result);
		}
	}
}
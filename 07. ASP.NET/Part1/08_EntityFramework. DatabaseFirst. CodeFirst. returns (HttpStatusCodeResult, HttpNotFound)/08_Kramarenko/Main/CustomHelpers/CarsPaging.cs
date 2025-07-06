using Main.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Main.CustomHelpers
{
	public static class CarsPaging
	{
		public static MvcHtmlString PagingButtons1(this HtmlHelper helper, PageInfo pageInfo, int startPage, Func<int, int, string> pageUrl)
		{
      string result = "";
      for (int i = 1; i <= pageInfo.TotalPages; i++)
      {
        TagBuilder tag = new TagBuilder("a");

        tag.MergeAttribute("href", pageUrl(i, startPage));
        tag.InnerHtml = i.ToString();

        if (i == pageInfo.CurrentPage)
        {
          tag.AddCssClass("btn-primary");
        }
        tag.AddCssClass("btn btn-default");

        result += tag.ToString();
      }
      return MvcHtmlString.Create(result);
    }
		public static MvcHtmlString PagingButtons2(this HtmlHelper helper, PageInfo pageInfo, int typePage, Func<int, int, string> pageUrl)
		{
      string result = "";
      for (int i = 1; i <= pageInfo.TotalPages; i++)
      {
        TagBuilder tag = new TagBuilder("a");

        tag.MergeAttribute("href", pageUrl(typePage, i));
        tag.InnerHtml = i.ToString();

        if (i == pageInfo.CurrentPage)
        {
					tag.AddCssClass("btn-primary");
				}
				tag.AddCssClass("btn btn-default");
				result += tag.ToString();
      }
      return MvcHtmlString.Create(result);
    }
  }
}
using MVC_Helpers.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC_Helpers.CustomHelpers
{
    public static class CustomPaging
    {
        public static MvcHtmlString PagingButtons(this HtmlHelper html, PageInfo pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");

                // pageUrl(i) - вызов метода, принимающего int, возвращающего string, через делегат
                tag.MergeAttribute("href", pageUrl(i));
                //tag.MergeAttribute("href", $"Books?pageNumber={i}");
                tag.InnerHtml = i.ToString();

                // стили для текущей страницы
                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}
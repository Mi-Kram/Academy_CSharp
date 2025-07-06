using CoreMVC_Helpers.ViewModels;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace CoreMVC_Helpers.App_Code
{
    public static class CustomPaging
    {
        public static HtmlString PagingButtons(this IHtmlHelper html, PageInfo pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml.Append(i.ToString());

                // стили для текущей страницы
                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");

                var writer = new System.IO.StringWriter();
                tag.WriteTo(writer, HtmlEncoder.Default);
                result.Append(new HtmlString(writer.ToString()));
            }

            return new HtmlString(result.ToString());
        }
    }
}

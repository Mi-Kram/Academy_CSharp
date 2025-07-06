using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace CoreMVC_Helpers.App_Code
{
    public static class MyCustomButton
    {
        public static HtmlString CustomButton(this IHtmlHelper helper, string ButtonText, string ButtonName, string cssName = "default_class")
        {
            TagBuilder tag = new TagBuilder("Button");

            // удаление пробелов и запрещённых символов
            tag.MergeAttribute("name", TagBuilder.CreateSanitizedId(ButtonName, ""));

            // генерация Id на основе имени
            tag.GenerateId(ButtonName, "");

            // установка надписи на кнопке
            tag.InnerHtml.Append(ButtonText);

            // добавление класса CSS
            tag.AddCssClass(cssName);

            var writer = new System.IO.StringWriter();
            tag.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
    }
}

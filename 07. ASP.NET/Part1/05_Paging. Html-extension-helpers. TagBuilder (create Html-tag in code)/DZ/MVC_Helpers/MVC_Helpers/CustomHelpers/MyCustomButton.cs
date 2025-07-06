using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Helpers.CustomHelpers
{
    public static class MyCustomButton
    {
        public static MvcHtmlString CustomButton(this HtmlHelper helper, string ButtonText, string ButtonName, string cssName = "default_class")
        {
            TagBuilder tag = new TagBuilder("Button");

            // удаление пробелов и запрещённых символов
            tag.MergeAttribute("name", TagBuilder.CreateSanitizedId(ButtonName));

            // генерация Id на основе имени
            tag.GenerateId(ButtonName);

            // установка надписи на кнопке
            tag.InnerHtml = ButtonText; 

            // добавление класса CSS
            tag.AddCssClass(cssName);

            return MvcHtmlString.Create(tag.ToString());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoneymoonShop.Model.DressModels;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Html;

namespace HoneymoonShop.CustomHtmlHelper
{
    public class HtmlHelpers
    {
        public static HtmlString CC(Color color)
        {
            //'{0}' ---> Ivory/IvoryColor/Color circle
            // {1} ----> De text die er moet komen.
            String colorName = color.ToString();
            String html = String.Format("<a class='{0} circle'></a>", color);
            return new HtmlString(html);
        }

        public static HtmlString CheckboxCustom(String bn)
        {
            //'{0}' ---> Ivory/IvoryColor/Color circle
            // {1} ----> De text die er moet komen.
            String html = String.Format("<input type = 'checkbox' name='Brands' value='{0}' />", bn);
            return new HtmlString(html);
        }

    }
}

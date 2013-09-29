using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.WebPages.Html;

namespace System.Web.Mvc
{
    public static class HtmlHelperExtensionMethods
    {
        public static IHtmlString RequireJsScript(this HtmlHelper htmlHelper, string mainPath)
        {
            return new HtmlString(string.Format("<script type=\"text/javascript\" src=\"{0}\" data-main=\"{1}\"></script>",
                VirtualPathUtility.ToAbsolute("~/Scripts/require.js"),
                VirtualPathUtility.ToAbsolute(mainPath)));
        }
    }
}
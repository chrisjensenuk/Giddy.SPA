using Giddy.SPA.Hosting.Filter.Web;
using System.Web;
using System.Web.Mvc;

namespace Giddy.SPA.Hosting
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
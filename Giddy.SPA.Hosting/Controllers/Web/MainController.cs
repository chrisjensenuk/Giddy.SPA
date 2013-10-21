using AttributeRouting.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Giddy.SPA.Hosting.Controllers.Web
{
    public partial class MainController : Controller
    {
        [HttpGet]
        [GET("/Main")]
        [AllowAnonymous]
        public virtual ActionResult Index()
        {
            return View();
        }

    }
}

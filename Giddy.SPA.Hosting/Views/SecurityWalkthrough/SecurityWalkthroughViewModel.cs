using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Giddy.SPA.Hosting.Views.SecurityWalkthrough
{
    public class SecurityWalkthroughViewModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public ActionResult NextRoute { get; set; }
    }
}
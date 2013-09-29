using Giddy.SPA.Hosting.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Giddy.SPA.Hosting
{
    public abstract class MvcControllerBase : Controller
    {
        public MvcControllerBase() : base() { }

        public ISecurityManager SecurityMgr { get; set; }
    }
}
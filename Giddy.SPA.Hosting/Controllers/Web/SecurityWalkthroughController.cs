using AttributeRouting.Web.Mvc;
using Giddy.SPA.Hosting.Security;
using Giddy.SPA.Hosting.Views.SecurityWalkthrough;
using NetSqlAzMan.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Giddy.SPA.Hosting.Controllers.Web
{
    public partial class SecurityWalkthroughController : MvcControllerBase
    {
       

        [HttpGet]
        [GET("/SecurityWalkthrough/NotSecured")]
        public virtual ActionResult NotSecured()
        {
            return View(MVC.SecurityWalkthrough.Views.SecurityWalkthroughView, new SecurityWalkthroughViewModel
            {
                Title = "Not Secured",
                Message = "This page is not secured",
                NextRoute = MVC.SecurityWalkthrough.Secured()
            });
        }

        [HttpGet]
        [Authorize]
        [GET("/SecurityWalkthrough/Secured")]
        public virtual ActionResult Secured()
        {
            //The Action can only been seen by logged in users
            return View(MVC.SecurityWalkthrough.Views.SecurityWalkthroughView, new SecurityWalkthroughViewModel
            {
                Title = "Secured",
                Message = "This page is secured and can only been seen by logged in users",
                NextRoute = MVC.SecurityWalkthrough.AdministratorRole()
            });
        }

        [HttpGet]
        [Authorize(Roles = "Administrators")]
        [GET("/SecurityWalkthrough/AdministratorRole")]
        public virtual ActionResult AdministratorRole()
        {
            //The Action can only be seen by users in the Administrator Role

            return View(MVC.SecurityWalkthrough.Views.SecurityWalkthroughView, new SecurityWalkthroughViewModel
            {
                Title = "Administrator Role",
                Message = "This page can only be view by users that are part of the administrator role",
                NextRoute = MVC.SecurityWalkthrough.OperationSecured()
            });
        }

        [HttpGet]
        [OperationAuthorize("Execute the operation")]
        [GET("/SecurityWalkthrough/OperationBasedSecurity")]
        public virtual ActionResult OperationSecured()
        {
            //The Action can only be seen by users with the 'Execute Secured Operation' permission
            return View(MVC.SecurityWalkthrough.Views.SecurityWalkthroughView, new SecurityWalkthroughViewModel
            {
                Title = "Operation Secured",
                Message = "This page can only be view by users that have the 'Execute the operation' permission",
                NextRoute = MVC.SecurityWalkthrough.PartlySecuredOperation()
            });
        }

        [HttpGet]
        [GET("/SecurityWalkThrough/PartlySecuredOperation")]
        public virtual ActionResult PartlySecuredOperation()
        {

            var message = "Authenticated users will see this.";

            if (CheckOperationAccess("Execute the operation"))
            {
                message += " However the 'admin' user will also see this.";
            }

            //The Action can only be seen by users with the 'Execute Secured Operation' permission
            return View(MVC.SecurityWalkthrough.Views.SecurityWalkthroughView, new SecurityWalkthroughViewModel
            {
                Title = "Partly Secured Operation",
                Message = message,
                NextRoute = MVC.SecurityWalkthrough.MultiTenant("companyA")
            });
        }

        [HttpGet]
        [GET("/SecurityWalkthrough/MultiTenant/{companyId}")]
        [OperationAuthorize("{companyId} - View Accounts")]
        public virtual ActionResult MultiTenant(string companyId)
        {
            return View(MVC.SecurityWalkthrough.Views.SecurityWalkthroughViewWithPost, new SecurityWalkthroughViewModel
            {
                Title = "Multi-tenant",
                Message = "Only admin can view companyA",
                NextRoute = MVC.SecurityWalkthrough.MultiTenantWithModel()
            });
        }

        public class MutliTenantViewModel
        {
            public string CompanyId { get; set; }
        }

        [HttpPost]
        [POST("/SecurityWalkthrough/MultiTenantWithModel/{companyId}")]
        [OperationAuthorize("{model.CompanyId} - View Accounts")]
        public virtual ActionResult MultiTenantWithModel(MutliTenantViewModel model)
        {
            return View(MVC.SecurityWalkthrough.Views.SecurityWalkthroughView, new SecurityWalkthroughViewModel
            {
                Title = "Multi-tenant With Model",
                Message = "Only admin can view companyA",
                NextRoute = MVC.Home.Index()
            });
        }

        private bool CheckOperationAccess(string operation)
        {
            return AuthorizationMgr.CheckAccess(operation);
        }

    }
    
}

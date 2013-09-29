// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace Giddy.SPA.Hosting.Controllers.Web
{
    public partial class SecurityWalkthroughController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public SecurityWalkthroughController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected SecurityWalkthroughController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult MultiTenant()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.MultiTenant);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult MultiTenantWithModel()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.MultiTenantWithModel);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public SecurityWalkthroughController Actions { get { return MVC.SecurityWalkthrough; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "SecurityWalkthrough";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "SecurityWalkthrough";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string NotSecured = "NotSecured";
            public readonly string Secured = "Secured";
            public readonly string AdministratorRole = "AdministratorRole";
            public readonly string OperationSecured = "OperationSecured";
            public readonly string PartlySecuredOperation = "PartlySecuredOperation";
            public readonly string MultiTenant = "MultiTenant";
            public readonly string MultiTenantWithModel = "MultiTenantWithModel";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string NotSecured = "NotSecured";
            public const string Secured = "Secured";
            public const string AdministratorRole = "AdministratorRole";
            public const string OperationSecured = "OperationSecured";
            public const string PartlySecuredOperation = "PartlySecuredOperation";
            public const string MultiTenant = "MultiTenant";
            public const string MultiTenantWithModel = "MultiTenantWithModel";
        }


        static readonly ActionParamsClass_MultiTenant s_params_MultiTenant = new ActionParamsClass_MultiTenant();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_MultiTenant MultiTenantParams { get { return s_params_MultiTenant; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_MultiTenant
        {
            public readonly string companyId = "companyId";
        }
        static readonly ActionParamsClass_MultiTenantWithModel s_params_MultiTenantWithModel = new ActionParamsClass_MultiTenantWithModel();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_MultiTenantWithModel MultiTenantWithModelParams { get { return s_params_MultiTenantWithModel; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_MultiTenantWithModel
        {
            public readonly string model = "model";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string SecurityWalkthroughView = "SecurityWalkthroughView";
                public readonly string SecurityWalkthroughViewModel = "SecurityWalkthroughViewModel";
                public readonly string SecurityWalkthroughViewWithPost = "SecurityWalkthroughViewWithPost";
            }
            public readonly string SecurityWalkthroughView = "~/Views/SecurityWalkthrough/SecurityWalkthroughView.cshtml";
            public readonly string SecurityWalkthroughViewModel = "~/Views/SecurityWalkthrough/SecurityWalkthroughViewModel.cs";
            public readonly string SecurityWalkthroughViewWithPost = "~/Views/SecurityWalkthrough/SecurityWalkthroughViewWithPost.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_SecurityWalkthroughController : Giddy.SPA.Hosting.Controllers.Web.SecurityWalkthroughController
    {
        public T4MVC_SecurityWalkthroughController() : base(Dummy.Instance) { }

        partial void NotSecuredOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult NotSecured()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.NotSecured);
            NotSecuredOverride(callInfo);
            return callInfo;
        }

        partial void SecuredOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult Secured()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Secured);
            SecuredOverride(callInfo);
            return callInfo;
        }

        partial void AdministratorRoleOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult AdministratorRole()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.AdministratorRole);
            AdministratorRoleOverride(callInfo);
            return callInfo;
        }

        partial void OperationSecuredOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult OperationSecured()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.OperationSecured);
            OperationSecuredOverride(callInfo);
            return callInfo;
        }

        partial void PartlySecuredOperationOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        public override System.Web.Mvc.ActionResult PartlySecuredOperation()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.PartlySecuredOperation);
            PartlySecuredOperationOverride(callInfo);
            return callInfo;
        }

        partial void MultiTenantOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, string companyId);

        public override System.Web.Mvc.ActionResult MultiTenant(string companyId)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.MultiTenant);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "companyId", companyId);
            MultiTenantOverride(callInfo, companyId);
            return callInfo;
        }

        partial void MultiTenantWithModelOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Giddy.SPA.Hosting.Controllers.Web.SecurityWalkthroughController.MutliTenantViewModel model);

        public override System.Web.Mvc.ActionResult MultiTenantWithModel(Giddy.SPA.Hosting.Controllers.Web.SecurityWalkthroughController.MutliTenantViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.MultiTenantWithModel);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            MultiTenantWithModelOverride(callInfo, model);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
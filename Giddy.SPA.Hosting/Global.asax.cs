using Giddy.SPA.Hosting.Data;
using Giddy.SPA.Hosting.Filters.Http;
using Giddy.SPA.Hosting.IoC;
using Giddy.SPA.Hosting.Security;
using Giddy.SPA.Hosting.Services;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Http.Validation;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Giddy.SPA.Hosting
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Set up IoC
            var container = SimpleInjectorInitializer.Initialize();

            //Prevent ModelState from prefixing properties with unwanted property name
            GlobalConfiguration.Configuration.Services.Replace(typeof(IBodyModelValidator),
                new PrefixlessBodyModelValidator(GlobalConfiguration.Configuration.Services.GetBodyModelValidator()));

            //ensure json is returned in camelCase
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //Set up the Http View Model filter globally so applied to all Web Api models
            GlobalConfiguration.Configuration.Filters.Add(new ValidationActionFilterAttribute(new ValidatorFactory(container)));

            //intialize simple membership provider
            SimpleMembershipInitializer.Initialize();

            //intialize the Role Provider and Operation Security Provider
            NetSqlAzManInitializer.Intialize();

            //create an 'admin' user
            //TODO - abstract this into my own SecurityManager class
            NetSqlAzManInitializer.Seed();

        }

        protected void Application_AcquireRequestState(Object sender, EventArgs e)
        {
            //All the user's Operation Permissions will be saved in the session at timeof login to save hitting the DB
            if (User.Identity.IsAuthenticated && UserSession.UserPermissionCache == null)
            {
                UserSession.UserPermissionCache = NetSqlAzManInitializer.GetUserPermissionCache(User.Identity.Name);
            }
        }

        

        
    }
}
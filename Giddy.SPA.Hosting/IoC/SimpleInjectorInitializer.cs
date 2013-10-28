namespace Giddy.SPA.Hosting.IoC
{
    using System.Reflection;
    using SimpleInjector;
    using SimpleInjector.Integration.Web.Mvc;
    using SimpleInjector.Extensions;
    using Giddy.SPA.Hosting.Security;
    using NetSqlAzMan.Interfaces;
    using NetSqlAzMan.Cache;
    using System;
    using System.Web.Mvc;
    using Giddy.SPA.Hosting.Services;
    using System.Web.Http.Dispatcher;
    using System.Web.Http;
    using FluentValidation;
    using Giddy.SPA.Hosting.Models;
    using Giddy.SPA.Hosting.Filter.Web;
    
    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC Dependency Resolver.</summary>
        public static Container Initialize()
        {
            // Did you know the container can diagnose your configuration? Go to: http://bit.ly/YE8OJj.
            var container = new Container();

            //Make SimpleInjector play nicely with T4MVC
            container.Options.ConstructorResolutionBehavior =
                new T4MvcControllerConstructorResolutionBehavior(
                    container.Options.ConstructorResolutionBehavior);
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            container.RegisterMvcAttributeFilterProvider();

            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            return container;
        }
     
        private static void InitializeContainer(Container container)
        {
            
            //register Http Controllers
            var services = GlobalConfiguration.Configuration.Services;
            var controllerTypes = services.GetHttpControllerTypeResolver().GetControllerTypes(services.GetAssembliesResolver());

            // register Web API controllers (important! http://bit.ly/1aMbBW0)
            foreach (var controllerType in controllerTypes)
            {
                container.Register(controllerType);
            }

            container.Register<IGiddySPAContext, GiddySPAContext>();

            container.Register<ISecurityManager, WebSecurityManager>();

            //register Fluent Validators
            container.Register<IValidatorFactory, ValidatorFactory>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            container.RegisterManyForOpenGeneric(typeof(IValidator<>), assemblies);
            
            //Only need one of these object per web request
            container.RegisterPerWebRequest<IAuthorizationManager, AuthorizationManager>();

            container.Register<IUserPermissionCacheFactory, UserPermissionCacheFactory>();

            container.Register<IDurandalRouteManager, DurandalRouteManager>();
            
            container.RegisterInitializer<OperationAuthorizeAttribute>(handler =>
            {
                handler.AuthorizationMgr = container.GetInstance<IAuthorizationManager>();
            });

            container.RegisterInitializer<GiddyWebAuthorizeAttribute>(handler =>
            {
                handler.AuthorizationMgr = container.GetInstance<IAuthorizationManager>();
            });
            

            container.RegisterInitializer<MvcControllerBase>(handler =>
                {
                    handler.AuthorizationMgr = container.GetInstance<IAuthorizationManager>();
                });

        }

        
    }

    
}
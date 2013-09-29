namespace Giddy.SPA.Hosting.IoC
{
    using System.Reflection;
    using SimpleInjector;
    using SimpleInjector.Integration.Web.Mvc;
    using Giddy.SPA.Hosting.Security;
    using NetSqlAzMan.Interfaces;
    using NetSqlAzMan.Cache;
    using System;
    using System.Web.Mvc;
    
    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
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
        }
     
        private static void InitializeContainer(Container container)
        {
            // For instance:
            // container.Register<IUserRepository, SqlUserRepository>();
            container.Register<IGiddySPAContext, GiddySPAContext>();
            
            //Only need one of these object per web request
            container.RegisterPerWebRequest<ISecurityManager, SecurityManager>();
            
            container.Register<IUserPermissionCacheFactory, UserPermissionCacheFactory>();
            
            container.RegisterInitializer<OperationAuthorizeAttribute>(handler =>
            {
                handler.SecurityMgr = container.GetInstance<ISecurityManager>();
            });

            container.RegisterInitializer<MvcControllerBase>(handler =>
                {
                    handler.SecurityMgr = container.GetInstance<ISecurityManager>();
                });
        }
    }

    
}
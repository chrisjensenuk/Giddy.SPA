using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

using SimpleInjector.Advanced;

namespace SimpleInjector
{
    /// <summary>
    /// T4MVC and SimpleInjector don't play nicely together as T4MVC create a default constructor for Controllers and SimpleInjector expects only one constructor
    /// </summary>
    public class T4MvcControllerConstructorResolutionBehavior
        : IConstructorResolutionBehavior
    {
        private IConstructorResolutionBehavior defaultBehavior;

        public T4MvcControllerConstructorResolutionBehavior(
            IConstructorResolutionBehavior defaultBehavior)
        {
            this.defaultBehavior = defaultBehavior;
        }

        [DebuggerStepThrough]
        public ConstructorInfo GetConstructor(Type serviceType,
            Type impType)
        {
            if (typeof(IController).IsAssignableFrom(impType))
            {
                var nonDefaultConstructors =
                    from constructor in impType.GetConstructors()
                    where constructor.GetParameters().Length > 0
                    select constructor;

                if (nonDefaultConstructors.Count() == 1)
                {
                    return nonDefaultConstructors.Single();
                }
            }

            // fall back to the container's default behavior.
            return this.defaultBehavior.GetConstructor(serviceType,
                impType);
        }
    }
}
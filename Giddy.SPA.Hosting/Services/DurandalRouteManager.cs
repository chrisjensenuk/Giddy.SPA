using Giddy.SPA.Hosting.Models;
using Giddy.SPA.Hosting.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Giddy.SPA.Hosting.Services
{
    public interface IDurandalRouteManager
    {
        IEnumerable<DurandalRoute> GetRoutes();
    }

    public class DurandalRouteManager : IDurandalRouteManager
    {

        private static IEnumerable<DurandalRoute> _routeDictionary = new List<DurandalRoute>
        {
            new DurandalRoute { Route = new[] { "" }, Title = "Home", ModuleId = "home/index", Nav = true },
            new DurandalRoute { Route = new[] { "register" }, Title = "Register", ModuleId = "home/register", Nav = true },
            new SecuredDurandalRoute {Operation = "ROUTE|secured", Route = new[] { "secured" }, Title = "Secured", ModuleId = "home/secure", Nav = true },
        };

        private IAuthorizationManager _authorizationMgr;
 
        public DurandalRouteManager(IAuthorizationManager authorizationMgr)
        {
            _authorizationMgr = authorizationMgr;
        }

        public IEnumerable<DurandalRoute> GetRoutes()
        {
            foreach (var route in _routeDictionary)
            {
                //if it is a secured route ensure we are authorized to use it
                if (route is SecuredDurandalRoute)
                {
                    if(_authorizationMgr.CheckAccess(((SecuredDurandalRoute)route).Operation))
                    {
                        yield return route;
                    }
                }
                else
                {
                    //this is an unsecured route
                    yield return route;
                }

                
            }
        }
    }
}
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
            new AnonymousDurandalRoute { Route = new[] { "register" }, Title = "Register", ModuleId = "account/register", Nav = true },
            new SecuredDurandalRoute {Operation = "ROUTE|manage", Route = new[] { "manage" }, Title = "Change Password", ModuleId = "account/manage", Nav = true },
        };

        private ISecurityManager _securityMgr;
        private IAuthorizationManager _authorizationMgr;
 
        public DurandalRouteManager(ISecurityManager securityMgr, IAuthorizationManager authorizationMgr)
        {
            _authorizationMgr = authorizationMgr;
            _securityMgr = securityMgr;
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
                else if (route is AnonymousDurandalRoute)
                {
                    if (!_securityMgr.LoggedIn)
                    {
                        yield return route;
                    }
                }
                else if (route is DurandalRoute)
                {
                    //this is an unsecured route
                    yield return route;
                }
                else
                {
                    throw new NotImplementedException("Don;t know how to handle this route type");
                }

                
            }
        }
    }
}
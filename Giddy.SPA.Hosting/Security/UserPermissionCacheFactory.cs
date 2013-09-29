using NetSqlAzMan.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Giddy.SPA.Hosting.Security
{
    /// <summary>
    /// Using a factory as the permission cache is reliant on Session which may not be immediately available
    /// </summary>
    public interface IUserPermissionCacheFactory
    {
        IAzManUserPermissionCache GetPermssionCache();
    }

    public class UserPermissionCacheFactory : IUserPermissionCacheFactory
    {
        public IAzManUserPermissionCache GetPermssionCache()
        {
            return UserSession.UserPermissionCache;
        }
    }
}
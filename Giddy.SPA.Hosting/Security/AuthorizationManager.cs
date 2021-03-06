﻿using Giddy.SPA.Hosting.Security;
using NetSqlAzMan;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Giddy.SPA.Hosting.Security
{

    public interface IAuthorizationManager
    {
        bool CheckAccess(string operation);
    }

    /// <summary>
    /// Using a factory to return the UserPermissionCache as the cache is relying on Session which doesn't exist at time
    /// </summary>
    public class AuthorizationManager : IAuthorizationManager
    {
        readonly IUserPermissionCacheFactory _userPermissionCacheFactory;
        public AuthorizationManager(IUserPermissionCacheFactory userPermissionCacheFactory)
        {
            _userPermissionCacheFactory = userPermissionCacheFactory;
        }

        public bool CheckAccess(string operation)
        {
            try
            {
                var permissionCache = _userPermissionCacheFactory.GetPermssionCache();

                AuthorizationType access;
                if (permissionCache == null)
                {
                    //we don't have a cache. Probably because we are using webapi and not using Session.
                    if (string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name)) return false;

                    var provider = (NetSqlAzManRoleProvider)Roles.Provider;
                    var storage = provider.GetStorage();
                    var dbUser = storage.GetDBUser(HttpContext.Current.User.Identity.Name);

                    if (dbUser == null) return false;

                    access = storage.CheckAccess("GiddySPA Store", "GiddySPA", operation, dbUser, DateTime.Now, true, null);
                }
                else
                {
                    access = permissionCache.CheckAccess(operation, DateTime.Now);
                }

                return (access == AuthorizationType.Allow || access == AuthorizationType.AllowWithDelegation);
            }
            catch (SqlAzManException)
            {
                //a missing operation will throw an exception
                return false;
            }
        }
    }
}
using NetSqlAzMan.Cache;
using NetSqlAzMan.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Giddy.SPA.Hosting
{
    public static class UserSession
    {
        const string _userPermissionCache = "UserPermissionCache";

        public static IAzManUserPermissionCache UserPermissionCache
        {
            get
            {
                if (HttpContext.Current.Session == null) return null;

                return (IAzManUserPermissionCache)HttpContext.Current.Session[_userPermissionCache];
            }
            set
            {
                if (HttpContext.Current.Session == null) return;

                HttpContext.Current.Session[_userPermissionCache] = value;
            }
        }
    }
}
using Giddy.SPA.Hosting.Data;
using Giddy.SPA.Hosting.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


[assembly: WebActivator.PreApplicationStartMethod(typeof(Giddy.SPA.Hosting.GlobalConfig), "Start")]

namespace Giddy.SPA.Hosting
{
    /// <summary>
    /// NetSqlAzMan's RoleProvider relies on the Database being created so need to initialize the database before the providers kick in.
    /// </summary>
    public static class GlobalConfig
    {
        public static void Start()
        {

            //configure the database
            DatabaseBootstrapper.Configure();

            //create required NetSqlAzMan database assets
            NetSqlAzManInitializer.Configure();
        }
    }
}
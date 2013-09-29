using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Giddy.Utility;
using System.Reflection;
using System.Data.Objects;
using System.Configuration;
using System.Data.SqlClient;

namespace Giddy.SPA.Hosting.Data
{
    /// <summary>
    /// Database intialization. Ensure this gets called once
    /// </summary>
    public class DatabaseBootstrapper
    {
        public static void Configure()
        {
            using (var context = new GiddySPAContext())
            {
                context.Database.Initialize(false);

                if (!context.Database.Exists())
                {
                    // Create the SimpleMembership database without Entity Framework migration schema
                    ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                }
            }
        }

        

    }
}
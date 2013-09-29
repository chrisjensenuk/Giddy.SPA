using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using WebMatrix.WebData;

namespace Giddy.SPA.Hosting.Security
{
    public class SimpleMembershipInitializer
    {
        public static void Initialize()
        {
            try
            {
                WebSecurity.InitializeDatabaseConnection("GiddySPADb", "UserProfile", "UserId", "UserName", autoCreateTables: true);

                //add an admin user if doesn't exist
                if(!WebSecurity.UserExists("admin")) WebSecurity.CreateUserAndAccount("admin", "admin");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("The ASP.NET Simple Membership database could not be initialized. For more information, please see http://go.microsoft.com/fwlink/?LinkId=256588", ex);
            }
        }
    }
}
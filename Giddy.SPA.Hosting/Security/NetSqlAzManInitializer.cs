using Giddy.Utility;
using NetSqlAzMan;
using NetSqlAzMan.Cache;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.Providers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;


namespace Giddy.SPA.Hosting.Security
{
    
    public static class NetSqlAzManInitializer
    {
        static string _connectionString = ConfigurationManager.ConnectionStrings["GiddySPADb"].ConnectionString;

        public static void Configure()
        {
            
            var sqlFile = Assembly.GetExecutingAssembly().GetManifestResourceToString("Giddy.SPA.Hosting.Security.NetSqlAzMan_SqlServer.sql");

            using (var context = new GiddySPAContext())
            {

                //Set the SQL file to use the name of our Database rather than the default
                sqlFile = sqlFile.Replace("[NetSqlAzManStorage]", "[" + context.Database.Connection.Database + "]");

                //SQL file contains lots of 'GO' statments to break up SQL into batches. Split into multiple statements
                var statements = sqlFile.Split(new[] { "\r\nGO\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var statement in statements)
                {
                    context.Database.ExecuteSqlCommand(statement);
                }
            }

            //setup the application in the NetSQLAzMan tables
            using (IAzManStorage storage = new SqlAzManStorage(_connectionString))
            {
                try
                {
                    storage.OpenConnection();

                    var store = storage.CreateStore("GiddySPA Store", "Store for Giddy.SPA");

                    var app = store.CreateApplication("GiddySPA", "A showcase Single Page Application");

                }
                catch
                {
                    throw;
                }
                finally
                {
                    storage.CloseConnection();
                }
            }
        }

        /// <summary>
        /// This needs to be called after the SimpleMembershipProvder tables have been created
        /// </summary>
        /// <param name="connectionString"></param>
        public static void Intialize()
        {
            using (var context = new GiddySPAContext())
            {
                
                //1) Update the table function [netsqlazman_GetDBUsers] so it points to SimpleMembership users
                var sqlGetDbUsers = Assembly.GetExecutingAssembly().GetManifestResourceToString("Giddy.SPA.Hosting.Security.NetSqlAzMan_GetDBUsers.sql");

                context.Database.ExecuteSqlCommand(sqlGetDbUsers);
                
                //2) now drop the UsersDemo table as it is only used by the table function [netsqlazman_GetDBUsers]
                context.Database.ExecuteSqlCommand("DROP TABLE UsersDemo");
            }
        }

        public static void Seed()
        {
            //setup the application in the NetSQLAzMan tables
            using (IAzManStorage storage = new SqlAzManStorage(_connectionString))
            {
                try
                {
                    storage.OpenConnection();
                    
                    var store = storage.GetStore("GiddySPA Store");
                    var app = store.GetApplication("GiddySPA");

                    //create a SQL Group
                    var administratorsGroup = app.CreateApplicationGroup(SqlAzManSID.NewSqlAzManSid(), "Administrators", "System Administrators", null, GroupType.Basic);

                    //add the 'admin' user to the 'Administrators' group
                    var dbUser = storage.GetDBUser("admin");
                    administratorsGroup.CreateApplicationGroupMember(dbUser.CustomSid, WhereDefined.Database, true);

                    //create an Role we can use IsInRole for attribute based security
                    var adminRole = app.CreateItem("Administrators", "Administrators", ItemType.Role);

                    //For the moment just add the administrator group to the administrator role
                    var administratorGroupAuth = adminRole.CreateAuthorization(dbUser.CustomSid, WhereDefined.Database, administratorsGroup.SID, WhereDefined.Application, AuthorizationType.AllowWithDelegation, null, null);

                    //create a new task
                    var newTask = app.CreateItem("New Task", "Task description", ItemType.Task);
                    
                    //Create a new Operation
                    IAzManItem newOp = app.CreateItem("Execute the operation", "Example operation", ItemType.Operation);
                    
                    //Add "New Operation" as a sid of "New Task"
                    newTask.AddMember(newOp);

                    //only let the 'admin' user perform the operation
                    newOp.CreateAuthorization(dbUser.CustomSid, WhereDefined.Database, dbUser.CustomSid, WhereDefined.Database, AuthorizationType.Allow, null, null);
                    
                    //add another Task to show multi-tenancy and 2 attached operations
                    var companyATask = app.CreateItem("CompanyA", "Company A Task", ItemType.Task);

                    var viewAccountsOp = app.CreateItem("CompanyA - View Accounts", "Allows the user to view accounts of company A", ItemType.Operation);
                    var editAccountsOp = app.CreateItem("CompanyA - Edit Accounts", "Allows the user to edit accounts of company A", ItemType.Operation);
                
                    companyATask.AddMember(viewAccountsOp);
                    companyATask.AddMember(editAccountsOp);

                    viewAccountsOp.CreateAuthorization(dbUser.CustomSid, WhereDefined.Database, dbUser.CustomSid, WhereDefined.Database, AuthorizationType.Allow, null, null);
                }
                catch
                {
                    throw;
                }
                finally
                {
                    storage.CloseConnection();
                }

                //bust the cache to ensure added items are included
                NetSqlAzMan.Providers.NetSqlAzManRoleProvider provider = ((NetSqlAzMan.Providers.NetSqlAzManRoleProvider)Roles.Provider);
                provider.InvalidateCache(true);
            }
        }

        public static UserPermissionCache GetUserPermissionCache(string userName)
        {
            var provider = (NetSqlAzManRoleProvider)Roles.Provider;
            var storage = provider.GetStorage();
            var dbUser = storage.GetDBUser(userName);
            var userPermissionCache = new NetSqlAzMan.Cache.UserPermissionCache(storage, "GiddySPA Store", "GiddySPA", dbUser, true, true);
            return userPermissionCache;
        }

        
    }
}
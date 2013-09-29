using Giddy.SPA.Hosting.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Giddy.SPA.Hosting
{
    public interface IGiddySPAContext : IDisposable
    {
        DbSet<UserProfile> UserProfiles { get; set; }
        Database Database { get; }
    }

    public class GiddySPAContext : DbContext, IGiddySPAContext
    {
        public GiddySPAContext()
            : base("GiddySPADb")
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<GiddySPAContext>());
        }

        public DbSet<UserProfile> UserProfiles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Don't pluralize tables
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //TODO: Add Db specific logic here. Don't polute the entities with DB specific info (e.g. Key, etc);

        }

        
    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace Giddy.SPA.Hosting.ExtensionMethods
{
    public static class EntityTypeConfigurationExtensionMethods
    {
        public static EntityTypeConfiguration<T> Table<T>(this EntityTypeConfiguration<T> entityConfig,  string tableName) where T : class
        {
            //Oddly EntityTypeConfiguration<T>.ToTable is not fluent and doens't return the entity config so creating my own fluent method
            entityConfig.ToTable(tableName);
            return entityConfig;
        }
    }
}
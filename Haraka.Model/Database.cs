using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Haraka.Model
{
    public abstract class Database : DbContext, IDatabase
    {
        public Database(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(x=>!x.FullName.StartsWith("System") && !x.FullName.StartsWith("Microsoft"))
                .SelectMany(a => a.GetTypes())
                .Where(t => typeof(Entity).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)                
                .ForEach(e =>
                {
                    if (modelBuilder.Model.FindEntityType(e) is null)
                        modelBuilder.Model.AddEntityType(e);
                });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}

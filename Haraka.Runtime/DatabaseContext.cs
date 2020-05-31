using Haraka.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Haraka.Runtime
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Assembly
                 .GetExecutingAssembly()
                 .GetTypes()
                 .Where(t => typeof(Entity).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
                 .ForEach(t =>
                 {
                     if (modelBuilder.Model.FindEntityType(t) == null)
                         return;

                     modelBuilder.Model.AddEntityType(t);
                 });



            base.OnModelCreating(modelBuilder);
        }
    }
}

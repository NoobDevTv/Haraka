using Haraka.Core.IoC;
using Haraka.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Haraka.SQLite
{
    public sealed class SqliteDatabaseProvider : IDbProvider
    {      
        public void RegisterDatabase(string source, ITypeContainer typeContainer)
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseSqlite($"Data Source={source}");
            var db = new SqliteDatabase(builder.Options);
            db.Database.EnsureCreated();
            //db.Database.Migrate();
            typeContainer.Register<IDatabase>(db);
        }
    }
}

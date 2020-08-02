using Haraka.Core.IoC;
using Haraka.Model;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haraka.SQLite
{
    public static class SqliteStartup
    {
        public static void Register(ITypeContainer typeContainer)
        {
            typeContainer.Register<IDbProvider, SqliteDatabaseProvider>();
        }
    }
}

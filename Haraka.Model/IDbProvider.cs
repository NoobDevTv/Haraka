using Haraka.Core.IoC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haraka.Model
{
    public interface IDbProvider
    {
        void RegisterDatabase(string source, ITypeContainer typeContainer);
    }
}

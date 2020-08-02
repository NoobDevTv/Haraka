
using Haraka.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Haraka.Model.Entities
{
    public class Villager : IdEntity<int>
    {
        public int Age { get; set; }
        public int MaxAge { get; set; }

        public Gender Gender { get; set; }

        public Villager()
        {

        }

        internal int GetResourceFactor(ResourceDefinition resourceDefinition)
            => 1;
    }
}

﻿using Haraka.Runtime.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace Haraka.Runtime
{
    public class Villager
    {
        public int Age { get; set; }
        public int MaxAge { get; set; }

        public Gender Gender { get; set; }

        public Villager()
        {

        }

        internal int GetResourceFactor(ResourceDefinition resourceDefinition) => throw new NotImplementedException();
    }
}

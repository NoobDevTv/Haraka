using System;
using System.Collections.Generic;
using System.Text;

namespace Haraka.Core
{
    public readonly struct ResourceDefinition : IEquatable<ResourceDefinition>
    {     
        public readonly string Name { get;  }
        public readonly int CollectAmountBase { get;  }

        public ResourceDefinition(string name, int collectAmountBase)
        {
            Name = name;
            CollectAmountBase = collectAmountBase;
        }

        public readonly override bool Equals(object obj) 
            => obj is ResourceDefinition definition 
               && Equals(definition);

        public readonly bool Equals(ResourceDefinition other) 
            => Name == other.Name 
               && CollectAmountBase == other.CollectAmountBase;

        public readonly override int GetHashCode() 
            => HashCode.Combine(Name, CollectAmountBase);

        public static bool operator ==(ResourceDefinition left, ResourceDefinition right) 
            => left.Equals(right);
        public static bool operator !=(ResourceDefinition left, ResourceDefinition right) 
            => !(left == right);
    }
}

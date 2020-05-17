using System;
using System.Collections.Generic;
using System.Text;

namespace Haraka.Runtime.Resources
{
    public class ResourceSource
    {
        public ResourceDefinition ResourceDefinition { get; set; }

        public int Amount { get; set; }
        public bool IsEmpty => Amount < 1;

        public ResourceSource(ResourceDefinition resourceDefinition, int amount)
        {
            ResourceDefinition = resourceDefinition;
            Amount = amount;
        }

        internal int Collect(Villager villager, int maxValue)
        {
            var collect = villager.GetResourceFactor(ResourceDefinition) * ResourceDefinition.CollectAmountBase;

            collect = Math.Min(maxValue, Math.Min(collect, Amount));
            Amount -= collect;
            return collect;
        }
    }
}

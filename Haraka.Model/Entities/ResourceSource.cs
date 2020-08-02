using Haraka.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Haraka.Model.Entities
{
    public class ResourceSource : IdEntity<int>
    {
        [NotMapped]
        public ResourceDefinition ResourceDefinition { get; set; }

        public int Amount { get; set; }
        public bool IsEmpty => Amount < 1;

        public ResourceSource()
        {

        }
        public ResourceSource(ResourceDefinition resourceDefinition, int amount)
        {
            ResourceDefinition = resourceDefinition;
            Amount = amount;
        }

        public int Collect(Villager villager, int maxValue)
        {
            var collect = villager.GetResourceFactor(ResourceDefinition) * ResourceDefinition.CollectAmountBase;

            collect = Math.Min(maxValue, Math.Min(collect, Amount));
            Amount -= collect;
            return collect;
        }
    }
}

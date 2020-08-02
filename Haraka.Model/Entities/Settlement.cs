using Haraka.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Haraka.Model.Entities
{
    public class Settlement : IdEntity<int>
    {
        public string Name { get; set; }
        public virtual SettlementMap Map { get; set; }
        public int MaxVillagers { get; set; }

        public virtual List<Villager> Villagers { get; }

        [NotMapped]
        public virtual List<Job> Jobs { get;  }

        [NotMapped]
        public Dictionary<ResourceDefinition, int> StockPile { get; }

        public Settlement()
        {
            Villagers = new List<Villager>();
            Jobs = new List<Job>();
            StockPile = new Dictionary<ResourceDefinition, int>();
        }

        public void SimulateTick(DayTime dayTime)
        {
            foreach (var job in Jobs.ToArray())
            {
                job.Execute(this);
            }
        }

        public void AddResource(ResourceDefinition resourceDefinition, int sum)
        {
            if (StockPile.ContainsKey(resourceDefinition))
                StockPile[resourceDefinition] += sum;
            else
                StockPile.Add(resourceDefinition, sum);
        }

        public void AddJobs(params Job[] jobs)
        {
            Jobs.AddRange(jobs);
        }
    }
}
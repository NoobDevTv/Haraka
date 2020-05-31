using Haraka.Runtime.Jobs;
using Haraka.Runtime.Resources;
using System;
using System.Collections.Generic;

namespace Haraka.Runtime
{
    public class Settlement
    {
        public string Name { get; set; }
        public SettlementMap Map { get; set; }
        public int MaxVillagers { get; set; }

        public List<Villager> Villagers { get; }

        public List<Job> Jobs { get;  }

        public Dictionary<ResourceDefinition, int> StockPile { get; }

        public Settlement()
        {
            Villagers = new List<Villager>();
            Jobs = new List<Job>();
            StockPile = new Dictionary<ResourceDefinition, int>();
        }

        internal void SimulateTick(DayTime dayTime)
        {
            foreach (var job in Jobs)
            {
                job.Execute(this);
            }
        }

        internal void AddResource(ResourceDefinition resourceDefinition, int sum)
        {
            if (StockPile.ContainsKey(resourceDefinition))
                StockPile[resourceDefinition] += sum;
            else
                StockPile.Add(resourceDefinition, sum);
        }

        internal void AddJobs(params Job[] jobs)
        {
            Jobs.AddRange(jobs);
        }
    }
}
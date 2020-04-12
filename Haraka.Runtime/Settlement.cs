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

        public Settlement()
        {
            Villagers = new List<Villager>();
            Jobs = new List<Job>();
        }

        internal void SimulateTick(DayTime dayTime)
        {
            foreach (var job in Jobs)
            {
                job.Execute();
            }
        }
    }
}
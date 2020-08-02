using Haraka.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Haraka.Model.Entities
{
    public abstract class Job : IdEntity<int>
    {
        public int MaxVillagers { get; set; }
        public bool Permanent { get; protected set; }

        public virtual List<Villager> AssignedVillagers { get;  }

        public int Tick { get; set; }

        public int TickCount { get; protected set; }

        public Job()
        {
            AssignedVillagers = new List<Villager>();
        }

        public abstract void Execute(Settlement settlement);

        public abstract bool CanExecute();

        public virtual void Cancel()
        {
        }
    }
}

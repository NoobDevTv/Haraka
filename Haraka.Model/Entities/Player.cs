using Haraka.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Haraka.Model.Entities
{
    public class Player : IdEntity<int>
    {
        public virtual List<Settlement> Settlements { get; }

        public Player()
        {
            Settlements = new List<Settlement>();
        }

        internal void SimulateTick(DayTime dayTime)
        {
            foreach (var settlement in Settlements)
            {
                settlement.SimulateTick(dayTime);
            }
        }
    }
}
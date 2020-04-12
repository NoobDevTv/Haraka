using System;
using System.Collections.Generic;
using System.Linq;

namespace Haraka.Runtime
{
    public class Player
    {
        public List<Settlement> Settlements { get; }

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
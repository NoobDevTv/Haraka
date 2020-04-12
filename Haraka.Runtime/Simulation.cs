using System;
using System.Collections.Generic;
using System.Text;

namespace Haraka.Runtime
{
    public sealed class Simulation
    {
        private readonly World world;

        public DayTime DayTime { get; set; }

        public Simulation(World world)
        {
            this.world = world;
        }

        internal void Tick()
        {
            DayTime = (DayTime)((int)(DayTime + 1) % 6);
            world.SimulateTick(DayTime);
        }
    }
}

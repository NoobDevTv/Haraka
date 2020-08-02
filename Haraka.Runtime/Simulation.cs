using Haraka.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Haraka.Runtime
{
    public sealed class Simulation
    {
        private readonly World world;
               
        public Simulation(World world)
        {
            this.world = world;
        }

        internal void Tick(long tick)
        {            
            world.SimulateTick(tick);
        }
    }
}

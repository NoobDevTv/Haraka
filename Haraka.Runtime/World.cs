using System;
using System.Collections.Generic;
using System.Text;

namespace Haraka.Runtime
{
    public sealed class World
    {
        public List<Player> Players { get;  }
        public Map Map { get;  }
        public DayTime DayTime { get; set; }

        public World(Map map)
        {
            Players = new List<Player>();
            Map = map;
        }

        internal void SimulateTick(long tick)
        {
            DayTime = (DayTime)((int)(DayTime + 1) % 6);
            foreach (var player in Players)
            {
                player.SimulateTick(DayTime);
            }
        }
    }
}

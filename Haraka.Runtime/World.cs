using System;
using System.Collections.Generic;
using System.Text;

namespace Haraka.Runtime
{
    public sealed class World
    {
        public List<Player> Players { get;  }
        public Map Map { get;  }

        public World(Map map)
        {
            Players = new List<Player>();
            Map = map;
        }

        internal void SimulateTick(DayTime dayTime)
        {
            foreach (var player in Players)
            {
                player.SimulateTick(dayTime);
            }
        }
    }
}

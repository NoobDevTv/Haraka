using Haraka.Core;
using Haraka.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Haraka.Model.Entities
{
    public  class World : IdEntity<int>
    {
        public virtual List<Player> Players { get;  }
        public virtual Map Map { get;  }
        public DayTime DayTime { get; set; }

        public World()
        {

        }

        public World(Map map)
        {
            Players = new List<Player>();
            Map = map;
        }

        public void SimulateTick(long tick)
        {
            DayTime = (DayTime)((int)(DayTime + 1) % 6);
            foreach (var player in Players)
            {
                player.SimulateTick(DayTime);
            }
        }
    }
}

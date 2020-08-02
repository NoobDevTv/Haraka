using Haraka.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Haraka.Model.Entities
{
    public class Game : IdEntity<int>
    {
        public virtual World World { get; set; }
        public TimeSpan TickTime { get; set; }
        public virtual Player Owner { get; set; }

        public Game()
        {
            World = new World(new Map());
            //TickTime = TimeSpan.FromSeconds(10);
            Owner = new Player();
            Owner.Settlements.Add(new Settlement());
            World.Players.Add(Owner);
        }
    }
}

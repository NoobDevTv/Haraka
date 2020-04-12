using System.Collections.Generic;

namespace Haraka.Runtime
{
    public class SettlementMap
    {
        public List<Hexagon> Hexagons { get;  }
        public List<Building> Buildings { get;  }

        public SettlementMap()
        {
            Hexagons = new List<Hexagon>();
            Buildings = new List<Building>();
        }
    }
}
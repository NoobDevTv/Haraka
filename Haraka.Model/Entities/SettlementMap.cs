using Haraka.Model;
using System.Collections.Generic;

namespace Haraka.Model.Entities
{
    public class SettlementMap : IdEntity<int>
    {
        public virtual List<Hexagon> Hexagons { get;  }
        public virtual List<Building> Buildings { get;  }

        public SettlementMap()
        {
            Hexagons = new List<Hexagon>();
            Buildings = new List<Building>();
        }
    }
}
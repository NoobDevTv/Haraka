using System;
using System.Collections.Generic;
using System.Text;

namespace Haraka.Model.Entities
{
    public class Hexagon : IdEntity<int>
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
    }
}

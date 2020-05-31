using Haraka.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Haraka.Runtime
{
    public class Game : IdEntity
    {
        public World World { get; set; }
        public TimeSpan TickTime { get; set; }
        public Player Owner { get; set; }
    }
}

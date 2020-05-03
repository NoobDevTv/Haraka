using System;
using System.Collections.Generic;
using System.Text;

namespace Haraka.Runtime
{
    public class Settings
    {
        public World World { get; set; }
        public TimeSpan TickTime { get; set; }
        public Player Owner { get; set; }
    }
}

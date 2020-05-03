using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haraka.Runtime
{
    public static class GameService
    {
        public static IObservable<long> Simulate(Simulation simulation, TimeSpan tickTime)
            => Observable
                .Interval(tickTime)
                .Do(simulation.Tick);

    }
}

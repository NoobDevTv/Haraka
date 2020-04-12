using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haraka.Runtime
{
    public static class GameService
    {
        public static void Simulate(Simulation simulation, TimeSpan tickTime)
            => Observable.Create<int>((observer, token) => Task.Run(async () =>
            {
                while (true)
                {
                    simulation.Tick();
                    await Task.Delay(tickTime, token);
                    observer.OnNext(0);
                }
            }, token));
    }
}

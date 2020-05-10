using System;
using System.Collections.Generic;
using System.Linq;
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


        public static IDisposable Create()
             => Observable.Create<long>(observer =>
             {
                 var observables = DataService
                     .GetSettings()
                     .Select(s => Simulate(new Simulation(s.World), s.TickTime));

                 return Observable
                            .Merge(observables)
                            .Subscribe(observer);
             }).Subscribe();

    }
}

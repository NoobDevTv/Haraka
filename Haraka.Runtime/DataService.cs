using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
using System.Text;

namespace Haraka.Runtime
{
    public static class DataService
    {
        private static readonly Subject<Game> gameSubject;

        static DataService()
        {
            gameSubject = new Subject<Game>();
        }

        public static void InsertGame(Game game)
            => gameSubject.OnNext(game);

        public static IObservable<Game> GetGames()
            => gameSubject;

    }
}

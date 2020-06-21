using Haraka.Runtime;
using Haraka.WebApp.Shared.Information;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Haraka.WebApp.Shared.Services
{
    public sealed class GameService
    {
        private readonly Game game;
        public GameService()
        {
            game = new Game();
            DataService.InsertGame(game);
        }

        public GameInfo Create(GameInfo gameInfo)
        {
            //using DatabaseContext context = null;

            //var gameTable = context.Set<Game>();
            //var game = gameInfo.ToGame();
            //gameTable.Add(game);
            //DataService.InsertGame(game);

            //context.SaveChanges();
            //return GameInfo.FromGame(game);
            throw new NotImplementedException();
        }

        public bool JoinGame(int id)
        {
            throw new NotImplementedException();
        } 

        public bool LeaveGame(int id)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public GameInfo GetInfo(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GameInfo> GetGames()
        {
            throw new NotImplementedException();
        }

        public Game GetCurrentDemoGame()
            => game;
    }
}

using Haraka.WebApp.Shared.Information;
using Haraka.WebApp.Shared.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Haraka.WebApp.Shared.Test
{
    public class MainIntegrationTest
    {
        [Test]
        public void Main()
        {
            var game = new GameInfo
            {
                Name = "My Game",
                Interval = TimeSpan.FromMinutes(12)
            };
            var gameService = new GameService();
            game = gameService.Create(game);

        }
    }
}

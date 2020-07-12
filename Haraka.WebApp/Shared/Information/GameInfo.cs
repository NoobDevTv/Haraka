using Haraka.Model.Entities;
using Haraka.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Haraka.WebApp.Shared.Information
{
    public class GameInfo
    {
        public string Name { get; set; }
        public TimeSpan Interval { get; set; }

        internal Game ToGame() => throw new NotImplementedException();
        internal static GameInfo FromGame(Game game) => throw new NotImplementedException();
    }
}

using Haraka.Model.Entities;
using Haraka.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace Haraka.WebApp.Shared.Model
{
    public sealed class User
    {
        public string Username { get; set; }
        public List<Player> Players { get; set; }
    }
}

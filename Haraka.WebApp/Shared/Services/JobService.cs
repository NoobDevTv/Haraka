using Haraka.Runtime;
using Haraka.Runtime.Jobs;
using Haraka.WebApp.Shared.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Haraka.WebApp.Shared.Services
{
    public sealed class JobService
    {
        private readonly GameService gameService;
        public JobService(GameService gameService)
        {
            this.gameService = gameService;
        }

        public bool TryCreate(JobInfo jobInfo)
        {
            var game = gameService.GetCurrentDemoGame();
            game
                .World
                .Players
                .FirstOrDefault()?
                .Settlements
                .FirstOrDefault()?
                .Jobs
                .Add(jobInfo.ToJob());
            return true;
        }

        //public JobInfo TryRead(int id)
        //{

        //}

        //public bool Update(JobInfo job)
        //{

        //}

        //public bool TryRemove(JobInfo job)
        //{

        //}
    }
}

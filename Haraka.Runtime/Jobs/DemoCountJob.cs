using Haraka.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Haraka.Runtime.Jobs
{
    public sealed class DemoCountJob : Job
    {
        public int HexId { get; }
        public int Counter { get; private set; }

        public DemoCountJob(int hexId)
        {
            HexId = hexId;
            Permanent = true;
        }

        public override void Execute(Settlement settlement)
        {
            if (!CanExecute())
            {
                Cancel();
                return;
            }

            TickCount++;

            if (Tick % TickCount != 0)
                return;

            Counter++;
            Console.WriteLine($"{nameof(DemoCountJob)}: " + Counter);
        }

        public override bool CanExecute()
        {
            if (!Permanent)
                return false;


            return true;
        }
    }
}

using Haraka.Runtime.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Haraka.Runtime.Jobs
{
    public sealed class CollectJob : Job
    {
        public ResourceSource Resource { get; }

        public int Limit { get; set; }

        public bool LimitReached => collected >= Limit;

        private int collected;

        public CollectJob(ResourceSource source)
        {
            Resource = source;
        }

        public override void Execute()
        {
            if (!CanExecute())
                Cancel();

            TickCount++;

            if (Tick % TickCount != 0)
                return;

            

            AssignedVillagers
                .Sum(v =>
                {
                    var collect = Resource.Collect(v, Limit - collected);
                    collected += collect;
                    return collect;
                });

            //Target?
        }

        public override bool CanExecute()
        {
            if (Resource.IsEmpty || LimitReached)
                return false;


            return true;
        }
    }
}

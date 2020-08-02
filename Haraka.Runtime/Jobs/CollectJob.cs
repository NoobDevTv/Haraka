
using Haraka.Model.Entities;
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
            Resource = source ?? throw new ArgumentNullException(nameof(source));
            Limit = source.Amount;
            //Tick += Distance / 3;
        }

        public CollectJob(ResourceSource source, int limit) : this(source)
            => Limit = limit;

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

            var sum = AssignedVillagers
                    .Sum(v =>
                    {
                        var collect = Resource.Collect(v, Limit - collected);
                        collected += collect;
                        return collect;
                    });

            settlement?.AddResource(Resource.ResourceDefinition, sum);
        }

        public override bool CanExecute()
        {
            if (Resource.IsEmpty || (LimitReached && !Permanent))
                return false;


            return true;
        }
    }
}

using Haraka.Runtime.Jobs;
using Haraka.Runtime.Resources;
using Microsoft.VisualStudio.TestPlatform.CrossPlatEngine.Discovery;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Haraka.Runtime.Test.Jobs
{
    [TestOf(typeof(CollectJob))]
    public class CollectJobTests
    {
        [TestCaseSource(typeof(CollectJobTestCases), nameof(CollectJobTestCases.CreateCollectJobCases))]
        public void CreateCollectJob(ResourceSource source)
        {
            var job = new CollectJob(source);
            Assert.AreEqual(source, job.Resource);
            Assert.AreEqual(source.Amount, job.Limit);
            Assert.AreEqual(source.Amount <= 0, job.LimitReached);
        }

        [TestCaseSource(typeof(CollectJobTestCases), nameof(CollectJobTestCases.CreateCollectJobWithLimitCases))]
        public void CreateCollectJobWithLimit(ResourceSource source, int limit)
        {
            var job = new CollectJob(source, limit);
            Assert.AreEqual(source, job.Resource);
            Assert.AreEqual(limit, job.Limit);
            Assert.AreEqual(limit <= 0, job.LimitReached);
        }

        [Test]
        public void PassNullToCtor()
        {
            Assert.Throws<ArgumentNullException>(() => new CollectJob(null));
            Assert.Throws<ArgumentNullException>(() => new CollectJob(null, 0));
        }

        [Test]
        public void ExecuteJobOnce()
        {
            var settlement = new Settlement();
            var villager = new Villager();
            var definition = new ResourceDefinition("Stone", 1);
            var job = new CollectJob(new ResourceSource(definition, 15));
            job.AssignedVillagers.Add(villager);
            job.Execute(settlement);
            Assert.IsFalse(job.LimitReached);
            Assert.AreEqual(1, job.AssignedVillagers.Count());
            Assert.AreEqual(1, settlement.StockPile[definition]);
        }

        [TestCaseSource(typeof(CollectJobTestCases), nameof(CollectJobTestCases.ExecuteJobToLimitCases))]
        public int ExecuteJobToLimit(ResourceDefinition definition, int resourceAmount, int limit)
        {
            var settlement = new Settlement();
            var villager = new Villager();
            var job = new CollectJob(new ResourceSource(definition, resourceAmount), limit);
            job.AssignedVillagers.Add(villager);
            Assert.IsFalse(job.LimitReached);
            for (var i = 0; i < resourceAmount; i++)
            {
                job.Execute(settlement);
            }

            Assert.AreEqual(1, job.AssignedVillagers.Count());
            return settlement.StockPile[definition];
        }

        public static class CollectJobTestCases
        {
            public static IEnumerable<TestCaseData> CreateCollectJobCases()
            {
                yield return new TestCaseData(new ResourceSource(default, 0))
                                   .SetName("ctor with Empty Source");

                yield return new TestCaseData(new ResourceSource(default, 15))
                                   .SetName("ctor with Valid Source");

                yield return new TestCaseData(new ResourceSource(default, -42))
                                   .SetName("ctor with invalid Source");
            }

            public static IEnumerable<TestCaseData> CreateCollectJobWithLimitCases()
            {
                yield return new TestCaseData(new ResourceSource(default, 0), 0)
                                   .SetName("ctor with Empty Source and 0 limit");

                yield return new TestCaseData(new ResourceSource(default, 15), 25)
                                   .SetName("ctor with Valid Source and 25 limit");

                yield return new TestCaseData(new ResourceSource(default, 15), 5)
                                   .SetName("ctor with Valid Source and 5 limit");

                yield return new TestCaseData(new ResourceSource(default, -42), 30)
                                   .SetName("ctor with invalid Source and 30 limit");

                yield return new TestCaseData(new ResourceSource(default, 0), -30)
                                   .SetName("ctor with invalid Source and minus 30");
            }

            internal static IEnumerable<TestCaseData> ExecuteJobToLimitCases()
            {
                yield return new TestCaseData(new ResourceDefinition("Stone", 1), 15, 15)
                                .SetName("execute Job with Limit and Source 15")
                                .Returns(15);

                yield return new TestCaseData(new ResourceDefinition("Stone", 1), 10, 15)
                                .SetName("execute Job with Limit 15 and Source 10")
                                .Returns(10);

                yield return new TestCaseData(new ResourceDefinition("Stone", 1), 100, 42)
                                .SetName("execute Job with Limit 42 and Source 100")
                                .Returns(42);

                yield return new TestCaseData(new ResourceDefinition("Stone", 2), 15, 15)
                                .SetName("execute Job with Limit 15 and Source 15, Resource base 2")
                                .Returns(15);
            }
        }
    }
}

using Haraka.Model.Entities;
using Haraka.Runtime.Jobs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Haraka.WebApp.Shared.Information
{
    public struct JobInfo
    {
        public int Id { get; set; }
        
        public int HexagonId { get; set; }
        
        public string JobType { get; set; }

        internal Job ToJob()
        {
            return new DemoCountJob(HexagonId);
        }
    }
}

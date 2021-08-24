using AMS.Models.Jobs;
using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Config.Job
{
    public class JobsRegistry : Registry
    {
        public JobsRegistry()
        {
            Schedule<GenerateDailyAgentAttendance>().ToRunEvery(1).Days().At(10, 00);
            
           
        }
    }
}
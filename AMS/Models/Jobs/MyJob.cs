using System;
using FluentScheduler;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AMS.Models.Jobs
{
    public class MyJob : IJob
    {
        void IJob.Execute()
        {
            Console.WriteLine("Hummam , The time is now:" + DateTime.Now);
        }
    }
}
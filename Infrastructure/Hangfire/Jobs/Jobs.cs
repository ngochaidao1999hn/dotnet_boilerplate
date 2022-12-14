using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Hangfire.Jobs
{
    public class Jobs
    {
        public void SendGetRequest()
        {
            Console.WriteLine("test1");
        }
    }
}

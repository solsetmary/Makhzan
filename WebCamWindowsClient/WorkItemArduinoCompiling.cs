using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebCamWindowsClient
{
    public class WorkItemArduinoCompiling
    {
        public Task<string> DoWork(WebCamService.WebCamServiceClient c, string s)
        {

            //create task, of which runs our work of a thread pool thread
            return Task.Factory.StartNew<string>(() => PerformWork(c, s));
        }

        private string PerformWork(WebCamService.WebCamServiceClient client, string arduinoSource)
        {
            Thread.Sleep(2000);//do work here...

            string res = client.arduinoCompiling(arduinoSource);

            //return result of work
            return res;
        }
    }
}

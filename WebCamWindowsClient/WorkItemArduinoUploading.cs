using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebCamWindowsClient
{
    public class WorkItemArduinoUploading
    {
        public Task<string> DoWork(WebCamService.WebCamServiceClient c)
        {
            return Task.Factory.StartNew<string>(() => PerformWork(c));
        }

        private string PerformWork(WebCamService.WebCamServiceClient client)
        {
            Thread.Sleep(2000);

            string res = client.arduinoUploading();

            return res;
        }

    }
}

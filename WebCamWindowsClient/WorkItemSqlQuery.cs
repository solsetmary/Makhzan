using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebCamWindowsClient
{
    public class WorkItemSqlQuery
    {
        public Task<string> DoWork(string s, NameValueCollection n)
        {
            return Task.Factory.StartNew<string>(() => PerformWork(s, n));
        }

        private string PerformWork(string s, NameValueCollection n)
        {
            Thread.Sleep(1000);
            string responsebody = "error";
            using (WebClient client = new WebClient())
            {
                byte[] responsebytes = client.UploadValues(s, "POST", n);
                responsebody = Encoding.UTF8.GetString(responsebytes);
            }

            return responsebody;
        }

        public Task<string> DoWorkGet(string s, NameValueCollection n)
        {
            return Task.Factory.StartNew<string>(() => PerformWorkGet(s, n));
        }

        private string PerformWorkGet(string s, NameValueCollection n)
        {
            Thread.Sleep(1000);
            string responsebody = "error";
            using (WebClient client = new WebClient())
            {
                byte[] responsebytes = client.UploadValues(s, "POST", n);
                responsebody = Encoding.UTF8.GetString(Convert.FromBase64String(Encoding.UTF8.GetString(responsebytes)));
            }

            return responsebody;
        }

    }
}


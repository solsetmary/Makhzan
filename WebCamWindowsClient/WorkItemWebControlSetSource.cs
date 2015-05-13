using Awesomium.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCamWindowsClient
{
    public class WorkItemWebControlSetSource
    {
        public Task<string> DoWork(WebControl w, string s)
        {

            //create task, of which runs our work of a thread pool thread
            return Task.Factory.StartNew<string>(() => PerformWork(w, s));
        }

        private string PerformWork(WebControl w, string s)
        {
            w.Source = new Uri(s);
            //return result of work
            return "Done";
        }
    }
}

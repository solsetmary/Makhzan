using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RobotWebCamServer
{
    class ArduinoDBService
    {

        #region Variables

        public string lastLabID;
        public string lastDevID;
        public string lastDevType;
        public string lastDevPort;
        public string lastArg1;
        public string lastArg2;

        public volatile int clientID;

        private string arduinoCompilerFile = "ArduinoUploader.exe";
        private string arduinoCompilerPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Arduino");
        private string arduinoBuildPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Arduino", "Build");
        public string arduinoSourceFile = "";
        public string batchFilePath = "";
        public string arduinoPort = "";
        public bool isUploading = false;

        List<bufferUserList> clientUserBuffer;

        List<bufferTextList> arduinoSourceCodeBuffer;
        List<bufferTextList> arduinoSerialDataBuffer;
        List<bufferUserList> arduinoUserBuffer;
        private ArduinoBoard arduinoBoardInterface;

        #endregion

        public ArduinoDBService(string labID, string arduinoID, string arduinoPort, string basePort, string serverPort)
        {
            this.lastLabID = labID;
            this.lastDevID = arduinoID;
            this.lastDevPort = arduinoPort;
            this.lastArg1 = basePort;
            this.lastArg2 = serverPort;
        }
        
        private string postWebClient(NameValueCollection reqParam, string postURL)
        {
            string responseBody = "";
            using (WebClient client = new WebClient())
            {
                byte[] responseBytes = client.UploadValues(postURL, "POST", reqParam);
                responseBody = Encoding.UTF8.GetString(responseBytes);
            }

            return responseBody;
        }

        public void Start()
        {
            getUserCommands();
        }


        private void getUserCommands()
        {
            bool isStreamCommands = true;

            new Thread(delegate()
            {
                System.Diagnostics.Stopwatch swFrameRate = new System.Diagnostics.Stopwatch();
                swFrameRate.Start();
                while (isStreamCommands)
                {
                    if (swFrameRate.Elapsed.TotalMilliseconds >= 1000)
                    {
                        swFrameRate.Restart();

                        try
                        {
                            var t = Task.Factory.StartNew(() =>
                            {
                                System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
                                reqparm.Add("first", lastLabID);
                                reqparm.Add("second", lastDevID);
                                postWebClient(reqparm, sharedVariables.Instance.homeURL + sharedVariables.Instance.setimagespreviewURL);
                            });
                            t.Wait();
                            long bytes = Process.GetCurrentProcess().WorkingSet64;
                            bytes = (long)((bytes / 1024f) / 1024f);
                            Console.Write(string.Format("\r{0} , ({1} ms) , ({2} Mbytes) physical memory allocated. ", DateTime.Now.ToShortTimeString(), (int)swFrameRate.Elapsed.TotalMilliseconds, bytes));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message.ToString());
                        }
                    }
                }

            }).Start();
        }
    }
}

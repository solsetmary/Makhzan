using AForge.Video.DirectShow;
using LumiSoft.Media.Wave;
using LumiSoft.Net.STUN.Client;
using RobotWebCamServer;
using System;
using System.Collections.Specialized;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RobocodServer
{
    class Program
    {
        public static string serverType = "camera";
        public static string labID = "1";
        public static string devID = "1";
        public static string devIndex = "0";
        public static string basePort = "32600";
        public static string serverPort = "32601";

        private static WebcamDBService WebcamDBServiceInterface;

        public static string globalIPaddress = "";
        //Shared 256 bit Key and IV here
        const string sKy = "lkirwf897+22#bbtrm8814z5qq=498j5"; //32 chr shared ascii string (32 * 8 = 256 bit)
        const string sIV = "741952hheeyy66#cs!9hjv887mxx7@8y"; //32 chr shared ascii string (32 * 8 = 256 bit)

        public static Boolean isclosing;

        static Thread _thread;
        static ManualResetEvent _shutdownEvent;
        static TimeSpan _threadDelay;


        #region unmanaged

        //[DllImport("winmm.dll", EntryPoint = "mciSendStringA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        //private static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);

        private static bool ConsoleCtrlCheck(CtrlTypes ctrlType)
        {
            // Put your own handling here:

            switch (ctrlType)
            {
                case CtrlTypes.CTRL_C_EVENT:

                    isclosing = true;

                    //CurrentDomain_ProcessExit(null, null);

                    break;

                case CtrlTypes.CTRL_BREAK_EVENT:

                    isclosing = true;

                    //CurrentDomain_ProcessExit(null, null);

                    break;

                case CtrlTypes.CTRL_CLOSE_EVENT:

                    isclosing = true;
                    CurrentDomain_ProcessExit(null, null);
                    break;

                case CtrlTypes.CTRL_LOGOFF_EVENT:
                case CtrlTypes.CTRL_SHUTDOWN_EVENT:

                    isclosing = true;

                    CurrentDomain_ProcessExit(null, null);

                    break;
            }

            return true;
        }

        // Declare the SetConsoleCtrlHandler function as external and receiving a delegate.

        [DllImport("Kernel32")]

        public static extern bool SetConsoleCtrlHandler(HandlerRoutine Handler, bool Add);

        // A delegate type to be used as the handler routine for SetConsoleCtrlHandler.

        public delegate bool HandlerRoutine(CtrlTypes CtrlType);

        // An enumerated type for the control messages sent to the handler routine.

        public enum CtrlTypes
        {

            CTRL_C_EVENT = 0,
            CTRL_BREAK_EVENT,
            CTRL_CLOSE_EVENT,
            CTRL_LOGOFF_EVENT = 5,
            CTRL_SHUTDOWN_EVENT
        }

        #endregion

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += new EventHandler(CurrentDomain_ProcessExit);

            SetConsoleCtrlHandler(ConsoleCtrlCheck, true);

            /*mciSendString("open new Type waveaudio Alias recsound", "", 0, 0);
            mciSendString("record recsound", "", 0, 0);
            Console.WriteLine("recording, press Enter to stop and save ...");
            Console.ReadLine();

            mciSendString("save recsound c:\\Soheyl\\result.wav", "", 0, 0);
            mciSendString("close recsound ", "", 0, 0);*/


            //testArduinoCompiling();

            checkArg(args);

        }

        static void checkArg(string[] args)
        {
            if (args.Length == 6)
            {
                startWithFullArg(args);
                return;
            }
            else
            {
                getServerType();
            }

        }

        static void getServerType()
        {
            var t1 = Task.Factory.StartNew(() => Console.WriteLine("Server type (camera/webcam/mic/chat/arduino) [camera]: "));
            t1.Wait();
            var t2 = Task<string>.Factory.StartNew(() =>
            {
                return Console.ReadLine();
            });
            t2.Wait();
            if (t2.Result != "")
                serverType = t2.Result.ToLower();

            if (serverType == "camera")
                getArgsForCameraServer();
            else if (serverType == "webcam")
                getArgsForWebcamServer();
            else if (serverType == "mic")
                getArgsForMicServer();
            else if (serverType == "chat")
                getArgsForChatServer();
            else if (serverType == "arduino")
                getArgsForArduinoServer();
            else
            {
                var t3 = Task.Factory.StartNew(() => Console.WriteLine("No valid server type."));
                t3.Wait();
                var t4 = Task.Factory.StartNew(() => Console.WriteLine("Press any key to exit . . ."));
                var t5 = Task.Factory.StartNew(() => Console.ReadKey());
                t5.Wait();
                var t = Task.Factory.StartNew(() => Environment.Exit(0));
                t.Wait();
                return;
            }
        }

        static void startWithFullArg(string[] args)
        {
            labID = args[1];
            devID = args[2];
            devIndex = args[3];
            basePort = args[4];
            serverPort = args[5];

            if (args[0].ToLower() == "camera")
            {
                startServer();
            }
            else if (args[0].ToLower() == "webcam")
            {
                serverType = args[0];
                startWebcamService();
            }
            else if (args[0].ToLower() == "mic")
            {
                var inDevices = WaveIn.Devices;
                if (inDevices.Length == 0)
                {
                    var t = Task.Factory.StartNew(() => Console.WriteLine("There is no audio-input devices installed."));
                    t.Wait();
                    var t2 = Task.Factory.StartNew(() => Console.WriteLine("Press any key to exit . . ."));
                    t2.Wait();
                    var t1 = Task.Factory.StartNew(() => Console.ReadKey());
                    t1.Wait();
                    System.Environment.Exit(0);
                    return;
                }
                serverType = args[0];
                startServer();
            }
            else if (args[0].ToLower() == "chat")
            {
                serverType = args[0];
                startServer();
            }
            else if (args[0].ToLower() == "arduino")
            {
                serverType = args[0];
                startServer();
            }
        }

        static void getArgsForArduinoServer()
        {
            Console.WriteLine(string.Format("Lab Id [{0}]: ", labID));
            string inp = Console.ReadLine();
            if (inp != "")
                labID = inp;

            Console.WriteLine(string.Format("Arduino ID in Database [{0}]: ", devID));
            inp = Console.ReadLine();
            if (inp != "")
                devID = inp;

            Console.WriteLine("---- Available COM ports ----");
            int i = 0;
            foreach (COMPortInfo comPort in COMPortInfo.GetCOMPortsInfo())
            {
                i++;
                devIndex = comPort.Name.Replace("COM", "");
                Console.WriteLine(string.Format("{0} : [{1}]", comPort.Description, devIndex));
            }

            //String[] portNames = System.IO.Ports.SerialPort.GetPortNames();

            if (i == 0)
            {
                Console.WriteLine("No valid Arduino port found.");
                Console.WriteLine("Press any key to exit . . .");
                Console.ReadKey();
                var t = Task.Factory.StartNew(() => System.Environment.Exit(0));
                t.Wait();
                return;
            }

            Console.WriteLine("-----------------------------");
            Console.WriteLine(string.Format("Arduino COM port [{0}]: ", devIndex));
            inp = Console.ReadLine();
            if (inp != "")
                devIndex = inp;

            Console.WriteLine(string.Format("Base Port [{0}]: ", basePort));
            inp = Console.ReadLine();
            if (inp != "")
                basePort = inp;

            Console.WriteLine(string.Format("Server Port [{0}]: ", serverPort));
            inp = Console.ReadLine();
            if (inp != "")
                serverPort = inp;

            startServer();
        }

        static void getArgsForChatServer()
        {
            Console.WriteLine(string.Format("Lab Id [{0}]: ", labID));
            string inp = Console.ReadLine();
            if (inp != "")
                labID = inp;

            Console.WriteLine(string.Format("Chat ID in Database [{0}]: ", devID));
            inp = Console.ReadLine();
            if (inp != "")
                devID = inp;

            Console.WriteLine(string.Format("Chat Index [{0}]: ", devIndex));
            inp = Console.ReadLine();
            if (inp != "")
                devIndex = inp;

            Console.WriteLine(string.Format("Base Port [{0}]: ", basePort));
            inp = Console.ReadLine();
            if (inp != "")
                basePort = inp;

            Console.WriteLine(string.Format("Server Port [{0}]: ", serverPort));
            inp = Console.ReadLine();
            if (inp != "")
                serverPort = inp;

            startServer();
        }

        static void getArgsForMicServer()
        {
            Console.WriteLine(string.Format("Lab Id [{0}]: ", labID));
            string inp = Console.ReadLine();
            if (inp != "")
                labID = inp;

            Console.WriteLine(string.Format("Mic ID in Database [{0}]: ", devID));
            inp = Console.ReadLine();
            if (inp != "")
                devID = inp;

            Console.WriteLine("---- Available Audio Input Devices ----");
            WavInDevice[] inDevices;
            try
            {
                inDevices = WaveIn.Devices;
                if (inDevices.Length == 0)
                {
                    Console.WriteLine("There is no audio-input devices installed.");
                    Console.WriteLine("Press any key to exit . . .");
                    Console.ReadKey();
                    var t = Task.Factory.StartNew(() => System.Environment.Exit(0));
                    t.Wait();
                    return;
                }
                else
                {
                    for (int i = 0; i < inDevices.Length; i++)
                    {
                        Console.WriteLine(string.Format("Audio Input Index [{0}] = {1}", i, inDevices[i].Name));
                    }
                }
            }
            catch (ApplicationException)
            {
                Console.WriteLine("failed Audio Input Devices initialize, EXIT and check Audio Input Devices.");
                return;
            }
            Console.WriteLine("---------------------------");
            Console.WriteLine(string.Format("Mic Index [{0}]: ", devIndex));
            inp = Console.ReadLine();
            if (inp != "")
                devIndex = inp;
            Console.WriteLine(string.Format("Base Port [{0}]: ", basePort));
            inp = Console.ReadLine();
            if (inp != "")
                basePort = inp;
            Console.WriteLine(string.Format("Server Port [{0}]: ", serverPort));
            inp = Console.ReadLine();
            if (inp != "")
                serverPort = inp;

            startServer();
        }

        static void getArgsForWebcamServer()
        {
            Console.WriteLine(string.Format("Lab Id [{0}]: ", labID));
            string inp = Console.ReadLine();
            if (inp != "")
                labID = inp;

            Console.WriteLine("---- Webcams Positions ----");
            Console.WriteLine("Webcam ID [1] = Front Camera");
            Console.WriteLine("Webcam ID [2] = Behind Camera");
            Console.WriteLine("Webcam ID [3] = Top Camera");
            Console.WriteLine("Webcam ID [4] = Free Camera");
            Console.WriteLine("---------------------------");
            Console.WriteLine(string.Format("Webcam ID [{0}]: ", devID));
            inp = Console.ReadLine();
            if (inp != "")
                devID = inp;

            Console.WriteLine("---- Available Webcams ----");
            FilterInfoCollection videoDevices;
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count == 0)
                {
                    Console.WriteLine("no webcam, EXIT and check Webcam connection.");
                    return;
                }
                else
                {
                    for (int i = 0; i < videoDevices.Count; i++)
                    {
                        Console.WriteLine(string.Format("Webcam Index [{0}] = {1}", i, videoDevices[i].Name));
                    }
                }
            }
            catch (ApplicationException)
            {
                Console.WriteLine("failed webcam initialize, EXIT and check Webcam connection.");
                return;
            }
            Console.WriteLine("---------------------------");
            Console.WriteLine(string.Format("Webcam Index [{0}]: ", devIndex));
            inp = Console.ReadLine();
            if (inp != "")
                devIndex = inp;

            basePort = "640";
            Console.WriteLine(string.Format("Width Pixel [{0}]: ", basePort));
            inp = Console.ReadLine();
            if (inp != "")
                basePort = inp;

            serverPort = "480";
            Console.WriteLine(string.Format("Height Pixel [{0}]: ", serverPort));
            inp = Console.ReadLine();
            if (inp != "")
                serverPort = inp;

            startWebcamService();
        }

        static void getArgsForCameraServer()
        {
            Console.WriteLine(string.Format("Lab Id [{0}]: ", labID));
            string inp = Console.ReadLine();
            if (inp != "")
                labID = inp;

            Console.WriteLine("---- Cameras Positions ----");
            Console.WriteLine("Camera ID [1] = Front Camera");
            Console.WriteLine("Camera ID [2] = Behind Camera");
            Console.WriteLine("Camera ID [3] = Top Camera");
            Console.WriteLine("Camera ID [4] = Free Camera");
            Console.WriteLine("---------------------------");
            Console.WriteLine(string.Format("Camera ID [{0}]: ", devID));
            inp = Console.ReadLine();
            if (inp != "")
                devID = inp;

            Console.WriteLine("---- Available Cameras ----");
            FilterInfoCollection videoDevices;
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count == 0)
                {
                    Console.WriteLine("no webcam, EXIT and check camera connection.");
                    return;
                }
                else
                {
                    for (int i = 0; i < videoDevices.Count; i++)
                    {
                        Console.WriteLine(string.Format("Camera Index [{0}] = {1}", i, videoDevices[i].Name));
                    }
                }
            }
            catch (ApplicationException)
            {
                Console.WriteLine("failed webcam initialize, EXIT and check camera connection.");
                return;
            }
            Console.WriteLine("---------------------------");
            Console.WriteLine(string.Format("Camera Index [{0}]: ", devIndex));
            inp = Console.ReadLine();
            if (inp != "")
                devIndex = inp;
            Console.WriteLine(string.Format("Base Port [{0}]: ", basePort));
            inp = Console.ReadLine();
            if (inp != "")
                basePort = inp;
            Console.WriteLine(string.Format("Server Port [{0}]: ", serverPort));
            inp = Console.ReadLine();
            if (inp != "")
                serverPort = inp;

            startServer();
        }

        static void startWebcamService()
        {
            sharedVariables.Instance.homeURL = "http://labpp.org/";
            sharedVariables.Instance.setimagespreviewURL = "myIP/setimagespreview.php";
            sharedVariables.Instance.setuserlivelogURL = "myIP/setuserlive.php";
            sharedVariables.Instance.deluserlivelogURL = "myIP/deluserlive.php";
            sharedVariables.Instance.serialdatalogURL = "myIP/serialdatalog.php";
            sharedVariables.Instance.stateURL = "myIP/camera.php";
            sharedVariables.Instance.statusURL = "query/do/";// "myIP/status.php";
            sharedVariables.Instance.DeviceType = serverType;
            sharedVariables.Instance.labID = labID;
            sharedVariables.Instance.devID = devID;
            sharedVariables.Instance.devIndex = devIndex;

            Console.WriteLine(string.Format("Building {0} server({1},{2},{3}) . . .", sharedVariables.Instance.DeviceType,
                sharedVariables.Instance.labID, sharedVariables.Instance.devID, sharedVariables.Instance.devIndex));

            WebcamDBServiceInterface = new WebcamDBService(labID, devID, devIndex, basePort, serverPort);

            System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
            reqparm.Add("labID", sharedVariables.Instance.labID);
            reqparm.Add("devID", sharedVariables.Instance.devID);
            reqparm.Add("devIndex", sharedVariables.Instance.devIndex);
            reqparm.Add("status", "aLive");
            reqparm.Add("devType", sharedVariables.Instance.DeviceType);
            reqparm.Add("portNr", serverPort);
            postWebClient(reqparm, sharedVariables.Instance.homeURL + sharedVariables.Instance.stateURL);


            //Console.WriteLine(string.Format("Server is running on port {0} , base port {1} ...", serverPort, basePort));
            Console.WriteLine(string.Format("Webcam Servic is running, {0}x{1} ...", basePort, serverPort));
            Console.WriteLine("Press <ENTER> to terminate service.");
            Console.WriteLine("Copyright \u00a9 2014 www.labpp.org");

            WebcamDBServiceInterface.Start();

            Console.ReadLine();
            CurrentDomain_ProcessExit(null, null);
            Console.WriteLine("\n" + "please wait to exit . . .");

            var t3 = Task.Factory.StartNew(() => WebcamDBServiceInterface.Stop());
            t3.Wait();

            if (_thread != null)
                _thread.Abort();
            Environment.Exit(0);

        }

        static void startServer()
        {
            ServiceHost webcamServiceHost = null;
            try
            {
                sharedVariables.Instance.homeURL = "http://labpp.org/";
                sharedVariables.Instance.setimagespreviewURL = "myIP/setimagespreview.php";
                sharedVariables.Instance.setuserlivelogURL = "myIP/setuserlive.php";
                sharedVariables.Instance.deluserlivelogURL = "myIP/deluserlive.php";
                sharedVariables.Instance.serialdatalogURL = "myIP/serialdatalog.php";
                sharedVariables.Instance.stateURL = "myIP/camera.php";
                sharedVariables.Instance.statusURL = "query/do/";// "myIP/status.php";
                sharedVariables.Instance.DeviceType = serverType;
                sharedVariables.Instance.labID = labID;
                sharedVariables.Instance.devID = devID;
                sharedVariables.Instance.devIndex = devIndex;

                Console.WriteLine(string.Format("Building {0} server({1},{2},{3}) . . .", sharedVariables.Instance.DeviceType,
                    sharedVariables.Instance.labID, sharedVariables.Instance.devID, sharedVariables.Instance.devIndex));

                BasicHttpBinding binding = new BasicHttpBinding() { Name = "binding1", HostNameComparisonMode = HostNameComparisonMode.StrongWildcard };
                binding.Security.Mode = BasicHttpSecurityMode.None;
                binding.TransferMode = TransferMode.Buffered;
                binding.MaxBufferSize = 2147483647;
                binding.MaxBufferPoolSize = 524288;
                binding.MaxReceivedMessageSize = 2147483647;


                Uri baseAddress = new Uri(string.Format("http://localhost:{0}/baseAddresses/RobotWebCamServer", basePort));
                Uri address = new Uri(string.Format("http://localhost:{0}/RobotWebCamServer", serverPort));

                // service host takes care of streaming
                webcamServiceHost = new ServiceHost(typeof(RobocodServer.WebCamService), baseAddress);
                webcamServiceHost.Opened += new EventHandler(host_Opened);
                webcamServiceHost.Closed += new EventHandler(host_Closed);
                
                webcamServiceHost.AddServiceEndpoint(typeof(IWebCamService), binding, address);
                webcamServiceHost.Open();
                System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
                reqparm.Add("labID", sharedVariables.Instance.labID);
                reqparm.Add("devID", sharedVariables.Instance.devID);
                reqparm.Add("devIndex", sharedVariables.Instance.devIndex);
                reqparm.Add("status", "aLive");
                reqparm.Add("devType", sharedVariables.Instance.DeviceType);
                reqparm.Add("portNr", serverPort);
                postWebClient(reqparm, sharedVariables.Instance.homeURL + sharedVariables.Instance.stateURL);
                //string output = new WebClient().DownloadString(string.Format(stateALiveURLpath, labID, devID, devIndex, serverType, serverPort));
            }
            catch (CommunicationException ce)
            {
                webcamServiceHost.Abort();
                //if (ce is System.ServiceModel.AddressAlreadyInUseException)
                Console.WriteLine(ce.Message);
                webcamServiceHost.Close();
                System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
                reqparm.Add("labID", sharedVariables.Instance.labID);
                reqparm.Add("devID", sharedVariables.Instance.devID);
                reqparm.Add("devIndex", sharedVariables.Instance.devIndex);
                reqparm.Add("status", "off");
                reqparm.Add("devType", sharedVariables.Instance.DeviceType);
                reqparm.Add("portNr", serverPort);
                postWebClient(reqparm, sharedVariables.Instance.homeURL + sharedVariables.Instance.stateURL);
                //string output = new WebClient().DownloadString(string.Format(stateOffURLpath, labID, devID, devIndex, serverType, serverPort));
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.ReadKey();
                CurrentDomain_ProcessExit(null, null);
                Console.WriteLine("\n" + "please wait to exit . . .");
                var t1 = Task.Factory.StartNew(() => Environment.Exit(0));
                t1.Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                var t2 = Task.Factory.StartNew(() => webcamServiceHost.Close());
                t2.Wait();
                System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
                reqparm.Add("labID", sharedVariables.Instance.labID);
                reqparm.Add("devID", sharedVariables.Instance.devID);
                reqparm.Add("devIndex", sharedVariables.Instance.devIndex);
                reqparm.Add("status", "off");
                reqparm.Add("devType", sharedVariables.Instance.DeviceType);
                reqparm.Add("portNr", serverPort);
                postWebClient(reqparm, sharedVariables.Instance.homeURL + sharedVariables.Instance.stateURL);
                //string output = new WebClient().DownloadString(string.Format(stateOffURLpath, labID, devID, devIndex, serverType, serverPort));
                Console.WriteLine("Press <ENTER> to terminate service.");
                Console.ReadKey();
                CurrentDomain_ProcessExit(null, null);
                Console.WriteLine("\n" + "please wait to exit . . .");
                var t = Task.Factory.StartNew(() => Environment.Exit(0));
                t.Wait();
            }

            Console.WriteLine(string.Format("Server is running on port {0} , base port {1} ...", serverPort, basePort));
            Console.WriteLine("Press <ENTER> to terminate service.");
            Console.WriteLine("Copyright \u00a9 2014 www.labpp.org");

            Console.ReadLine();
            CurrentDomain_ProcessExit(null, null);
            Console.WriteLine("\n" + "please wait to exit . . .");
            if (webcamServiceHost.State == CommunicationState.Created)
            {
                var t3 = Task.Factory.StartNew(() => webcamServiceHost.Close());
                t3.Wait();
            }
            _thread.Abort();
            Environment.Exit(0);
        }

        static void host_Closed(object sender, EventArgs e)
        {
            Console.WriteLine("Service Closed");
        }

        static void host_Opened(object sender, EventArgs e)
        {
            Console.WriteLine("Service Started");
            ThreadStart ts = new ThreadStart(ServiceMain);

            // create an un-signaled shutdown event
            _shutdownEvent = new ManualResetEvent(false);

            // create and start the worker thread
            _thread = new Thread(ts);
            _thread.Start();
        }

        static void CurrentDomain_ProcessExit(object sender, EventArgs e)
        {
            if (_thread != null)
                _thread.Abort();
            System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
            reqparm.Add("labID", sharedVariables.Instance.labID);
            reqparm.Add("devID", sharedVariables.Instance.devID);
            reqparm.Add("devIndex", sharedVariables.Instance.devIndex);
            reqparm.Add("status", "off");
            reqparm.Add("devType", sharedVariables.Instance.DeviceType);
            reqparm.Add("portNr", serverPort);
            postWebClient(reqparm, sharedVariables.Instance.homeURL + sharedVariables.Instance.stateURL);
            //string myoutput = new WebClient().DownloadString(string.Format(stateOffURLpath, labID, devID, devIndex, serverType, serverPort));
            Console.WriteLine("exit Process");
        }

        static void ServiceMain()
        {
            // initialize delay object with hours, minutes, seconds
            _threadDelay = new TimeSpan(0, 10, 0);

            // main service loop...

            bool bSignaled = false;
            while (true)
            {
                // wait for time delay or signal
                bSignaled = _shutdownEvent.WaitOne(new TimeSpan(0, 0, 5), true);

                // Q - were we signaled to terminate ?
                /*if (bSignaled == true)
                {
                    // yes - exit the while forever loop...
                    break;
                }*/
                STUN_Result result = null;

                try
                {
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    socket.Bind(new IPEndPoint(IPAddress.Any, 0));

                    result = STUN_Client.Query("stun1.l.google.com", int.Parse("19302"), socket);

                }
                catch (Exception x)
                {
                    break;
                }
                finally
                {
                    if (result.PublicEndPoint != null & result.PublicEndPoint.Address.ToString() != globalIPaddress)
                    {
                        string query = string.Format("Update prorobot_lab SET labIP='{0}' WHERE labID='{1}'", 
                            result.PublicEndPoint.Address, sharedVariables.Instance.labID);

                        query = sharedVariables.Instance.homeURL + sharedVariables.Instance.statusURL + sharedVariables.Instance.EncryptRJ256(sKy, sIV, query);
                        query = postWebClient(null, query);

                        globalIPaddress = result.PublicEndPoint.Address.ToString();
                        Console.WriteLine(string.Format("\r{0}: {1} server({2},{3},{4}), Server IP address updated.       ", DateTime.Now.ToShortTimeString(), 
                            sharedVariables.Instance.DeviceType, sharedVariables.Instance.labID, sharedVariables.Instance.devID,
                            sharedVariables.Instance.devIndex));
                    }
                    else
                        Console.WriteLine(string.Format("\r{0}: {1} server({2},{3},{4}), IP address does not changed.      ", DateTime.Now.ToShortTimeString(),
                            sharedVariables.Instance.DeviceType, sharedVariables.Instance.labID, sharedVariables.Instance.devID,
                            sharedVariables.Instance.devIndex));
                }
                // wait for time delay or signal
                bSignaled = _shutdownEvent.WaitOne(_threadDelay, true);
                /*
                string externalip = new WebClient().DownloadString("http://icanhazip.com");
                MethodInvoker action = delegate { textBoxIP.Text += externalip; };
                textBoxIP.BeginInvoke(action);
                */
            }
        }

        static string postWebClient(NameValueCollection reqParam, string postURL)
        {
            string responseBody = "";
            using (WebClient client = new WebClient())
            {
                if (reqParam == null)
                    responseBody = client.DownloadString(postURL);
                else
                {
                    byte[] responseBytes = client.UploadValues(postURL, "POST", reqParam);
                    responseBody = Encoding.UTF8.GetString(responseBytes);
                }
            }

            return responseBody;

        }
    }

}

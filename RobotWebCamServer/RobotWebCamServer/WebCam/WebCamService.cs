using AForge.Video;
using AForge.Video.DirectShow;
using LumiSoft.Media.Wave;
using RobotWebCamServer;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Script.Serialization;

// arduino 2 6 35 32618 32619
namespace RobocodServer
{
    /* Arduino Command Line for compiling
        ArduinoUploader [sketch/HEX file] [board type] [serial port/usbasp]
        Board types:
        1 - Arduino Uno
        2 - Arduino Leonardo
        3 - Arduino Esplora
        4 - Arduino Micro
        5 - Arduino Duemilanove (328)
        6 - Arduino Duemilanove (168)
        7 - Arduino Nano (328)
        8 - Arduino Nano (168)
        9 - Arduino Mini (328)
        10 - Arduino Mini (168)
        11 - Arduino Pro Mini (328)
        12 - Arduino Pro Mini (168)
        13 - Arduino Mega 2560/ADK
        14 - Arduino Mega 1280
        15 - Arduino Mega 8
        16 - Microduino Core+ (644)
        17 - Freematics OBD-II Adapter
    */

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WebCamService" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class WebCamService : IWebCamService, IDisposable
    {

        #region Variables

        private string camName;
        private VideoCaptureDevice videoSource = null;
        //private Bitmap latestImage;
        private Bitmap newImage;
        public string lastLabID;
        public string lastDevID;
        public string lastDevType;

        public volatile int clientID;

        public volatile string waveMD5;
        public WaveIn waveIn = null;
        public WaveIn micWaveIn = null;
        System.Timers.Timer timerUploadImage;
        System.Timers.Timer timerCheckAliveUser;

        private const int waveBuffer = 8000;
        private byte[] newMicBuffer;
        private byte[] tempMicBuffer;
        private byte[] sendingMicBuffer;
        public bool isRecordVoice = false;

        private string arduinoCompilerFile = "ArduinoUploader.exe";
        private string arduinoCompilerPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Arduino");
        private string arduinoBuildPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Arduino", "Build");
        public string arduinoSourceFile = "";
        public string batchFilePath = "";
        public string arduinoPort = "";
        public bool isUploading = false;

        List<bufferUserList> clientUserBuffer;

        List<bufferTextList> chatTextBuffer;
        List<bufferUserList> chatUserBuffer;

        List<bufferTextList> arduinoSourceCodeBuffer;
        List<bufferTextList> arduinoSerialDataBuffer;
        List<bufferUserList> arduinoUserBuffer;
        private ArduinoBoard arduinoBoardInterface;

        #endregion

        private void SerialDataLog(string lName, string content)
        {
            try
            {
                System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
                reqparm.Add("l", lastLabID);
                reqparm.Add("d", lastDevID);
                reqparm.Add("u", lName);
                reqparm.Add("c", content);
                postWebClient(reqparm, sharedVariables.Instance.homeURL + sharedVariables.Instance.serialdatalogURL);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        private void AddUserToUserLiveLog(string lName)
        {
            try
            {
                System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
                reqparm.Add("l", lastLabID);
                reqparm.Add("d", lastDevID);
                reqparm.Add("u", lName);
                postWebClient(reqparm, sharedVariables.Instance.homeURL + sharedVariables.Instance.setuserlivelogURL);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        private void RemoveUserFromUserLiveLog(string lName)
        {
            try
            {
                System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
                reqparm.Add("l", lastLabID);
                reqparm.Add("d", lastDevID);
                reqparm.Add("u", lName);
                postWebClient(reqparm, sharedVariables.Instance.homeURL + sharedVariables.Instance.deluserlivelogURL);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
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

        private void startCheckUserTimer()
        {
            if (timerCheckAliveUser == null)
            {
                timerCheckAliveUser = new System.Timers.Timer(5000); //check Users ALive every 5 seconds
                timerCheckAliveUser.Elapsed += new ElapsedEventHandler(timerCheckAliveUser_Tick);
                timerCheckAliveUser.Start();
            }
        }

        private void timerCheckAliveUser_Tick(object sender, ElapsedEventArgs e)
        {
            int listCount = clientUserBuffer.Count;

            for (int i = (listCount - 1); i >= 0; i--)
            {
                if (clientUserBuffer[i].DateStamp <= DateTime.Now.AddSeconds(-15))
                {
                    Console.WriteLine(string.Format("\n{0} , client({1}): {2} , is gone.({3})", DateTime.Now.ToShortTimeString(), 
                        i, clientUserBuffer[i].LoginName, sharedVariables.Instance.DeviceType));
                    removeUser(clientUserBuffer[i].LoginName);
                }
            }
        }

        private void removeUser(string lName)
        {
            if(lastDevType == "chat")
                stop_Chat(lName);
            else if (lastDevType == "arduino")
                stop_arduino(lName);
            else if (lastDevType == "camera")
                stop_record(lName);
        }

        #region Webcam Server

        public int CamerasNr()
        {
            FilterInfoCollection videoDevices;
            int Numbers;
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count == 0)
                {
                    Numbers = 0;
                    throw new ApplicationException("no webcams");
                }
                else
                {
                    Numbers = videoDevices.Count;
                }
            }
            catch (ApplicationException)
            {
                Numbers = -1;
                throw new ApplicationException("failed web cams initialize");
            }
            Console.WriteLine(string.Format("{0} , Cameras Number({1}) requested.", DateTime.Now.ToShortTimeString(), Numbers));
            return Numbers;

        }

        public string[] CamerasNames()
        {
            FilterInfoCollection videoDevices;
            string[] cNames;
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count == 0)
                {

                    throw new ApplicationException("no webcams");
                }
                else
                {
                    cNames = new string[videoDevices.Count];
                    for (int i = 0; i < videoDevices.Count; i++)
                    {
                        cNames[i] = videoDevices[i].Name;
                    }
                }
            }
            catch (ApplicationException)
            {

                throw new ApplicationException("failed web cams initialize");
            }
            Console.WriteLine(string.Format("{0} , Cameras Name requested.", DateTime.Now.ToShortTimeString()));
            return cNames;

        }

        public string[] CamerasValues()
        {
            FilterInfoCollection videoDevices;
            string[] cNames;
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count == 0)
                {
                    throw new ApplicationException("no webcams");
                }
                else
                {
                    cNames = new string[videoDevices.Count];
                    for (int i = 0; i < videoDevices.Count; i++)
                    {
                        cNames[i] = videoDevices[i].MonikerString;
                    }
                }
            }
            catch (ApplicationException)
            {

                throw new ApplicationException("failed web cams initialize");
            }
            Console.WriteLine(string.Format("{0} , Cameras Moniker String requested.", DateTime.Now.ToShortTimeString()));
            return cNames;

        }

        public void Record(string loginName, string cValue, string cName, string lID, string cID)
        {
            //Console.WriteLine(DateTime.Now.ToShortTimeString());
            lastDevType = "camera";
            startCheckUserTimer();
            FilterInfoCollection videoDevices;
            string camDescr;
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count == 0)
                {
                    throw new ApplicationException("no webcams");
                }
                else
                {
                    camName = videoDevices[Convert.ToInt32(cValue)].MonikerString;
                    camDescr = videoDevices[Convert.ToInt32(cValue)].Name;
                }
            }
            catch (ApplicationException)
            {
                throw new ApplicationException("failed web cams initialize");
            }

            if (clientUserBuffer != null)
            {
                if (clientUserBuffer.FindIndex(p => p.LoginName == loginName) >= 0)
                {
                    clientUserBuffer.RemoveAll(p => p.LoginName == loginName);
                    clientID--;
                }
            }

            clientID++;
            Console.WriteLine(string.Format("\n{0} , client : {1} , Name : {2} , webCam : {3} on {4}", DateTime.Now.ToShortTimeString(), clientID, loginName, camDescr, cName));

            lastLabID = lID; 
            lastDevID = cID;

            AddUserToUserLiveLog(loginName);

            if (clientID == 1)
            {
                clientUserBuffer = new List<bufferUserList>();
                bufferUserList newuser = new bufferUserList() { LoginName = loginName, DateIn = DateTime.Now, DateStamp = DateTime.Now };
                clientUserBuffer.Add(newuser);

                if (videoSource != null)
                    return;
            }

            if (clientUserBuffer.FindIndex(p => p.LoginName == loginName) >= 0)
                clientUserBuffer.RemoveAll(p => p.LoginName == loginName);

            bufferUserList mynewuser = new bufferUserList() { LoginName = loginName, DateIn = DateTime.Now, DateStamp = DateTime.Now };
            clientUserBuffer.Add(mynewuser);

            if (videoSource != null)
                return;
            
            videoSource = new VideoCaptureDevice(camName);
            videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            videoSource.DesiredFrameSize = new Size(320, 240);//new Size(480, 360);
            videoSource.DesiredFrameRate = 10;
            videoSource.Start();

            if (timerUploadImage == null)
            {
                timerUploadImage = new System.Timers.Timer(300);
                timerUploadImage.Elapsed += new ElapsedEventHandler(timerUploadImage_Tick);
                timerUploadImage.Start();
            }
        }

        public void stop_record(string loginName)
        {
            if (clientUserBuffer.FindIndex(p => p.LoginName == loginName) >= 0)
                clientUserBuffer.RemoveAll(p => p.LoginName == loginName);
            clientID--;

            RemoveUserFromUserLiveLog(loginName);

            if (clientID <= 0)
            {
                if (!(videoSource == null))
                    if (videoSource.IsRunning)
                    {
                        long mm = GC.GetTotalMemory(true);
                        Console.WriteLine(string.Format("\n{0} , ({1} bytes) last Connection closed. ", DateTime.Now.ToShortTimeString(), mm));
                        /*videoSource.SignalToStop();
                        newImage = null;
                        videoSource = null;
                        timerUploadImage.Stop();
                        timerUploadImage = null;*/
                    }
            }
            else
            {
                long mm = GC.GetTotalMemory(true);
                Console.WriteLine(string.Format("\n{0} , client(s) {1} , ({2} bytes) Connection closed. ", DateTime.Now.ToShortTimeString(), clientID, mm));
            }

        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                newImage = (Bitmap)eventArgs.Frame.Clone();
                //newImage.RotateFlip(RotateFlipType.Rotate270FlipNone);
                //lock (latestImage)
                //{
                //    latestImage = (Bitmap)newImage.Clone();
                //}
                //latestImage.Save(@"c:\temp\" + Guid.NewGuid().ToString() + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                long bytes = Process.GetCurrentProcess().WorkingSet64;
                bytes = (long)((bytes / 1024f) / 1024f);
                Console.Write(string.Format("\r{0} , ({1} Mbytes) physical memory allocated. ", DateTime.Now.ToShortTimeString(), bytes));
            }
            catch (Exception ex)
            {
                // do nill;
            }
        }

        public void Dispose()
        {
            stop_record("");
            if (newImage != null)
            {
                newImage.Dispose();
                newImage = null;
            }
        }

        Stream IWebCamService.getWebCamImage(string loginName)
        {
            int indexUser = clientUserBuffer.FindIndex(p => p.LoginName == loginName);
            DateTime dtIn = DateTime.Now;
            DateTime dt = dtIn;
            if (indexUser >= 0)
            {
                dtIn = clientUserBuffer[indexUser].DateIn;
                clientUserBuffer.RemoveAll(p => p.LoginName == loginName);
                bufferUserList mynewuser = new bufferUserList() { LoginName = loginName, DateIn = dtIn, DateStamp = dt };
                clientUserBuffer.Add(mynewuser);
            }

            MemoryStream ms = new MemoryStream();
            if (newImage != null)
            {
                lock (newImage)
                {
                    /*RectangleF rectf = new RectangleF(0, newImage.Size.Height-50, 250, 50);

                    Graphics g = Graphics.FromImage(newImage);

                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    g.DrawString("www.soheyln.com", new Font("Tahoma", 18), Brushes.Yellow, rectf);
                    g.Flush();*/

                    lock (ms)
                    {
                        newImage.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        ms.Position = 0;
                    }
                }


                //WebOperationContext.Current.OutgoingResponse.ContentType = "image/jpeg";
                return ms;

            }
            else
            {
                return null;
            }

        }

        private void timerUploadImage_Tick(object sender, ElapsedEventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            if (newImage != null)
            {
                lock (newImage)
                {
                    lock (ms)
                    {
                        new Bitmap(newImage, new Size(320, 240)).Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        ms.Position = 0;
                    }
                }

                byte[] imageBytes = ms.ToArray();
                string strImage = Convert.ToBase64String(imageBytes);
                try
                {
                    System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
                    reqparm.Add("labID", lastLabID);
                    reqparm.Add("devID", lastDevID);
                    reqparm.Add("image", strImage);
                    postWebClient(reqparm, sharedVariables.Instance.homeURL + sharedVariables.Instance.setimagespreviewURL);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
            }
        }

        #endregion

        #region Microphone Server

        static string CalculateMD5(byte[] buffer)
        {
            MD5 algorithm = MD5.Create();
            algorithm.TransformBlock(buffer, 0, waveBuffer, buffer, 0);
            algorithm.TransformFinalBlock(buffer, 0, waveBuffer);
            byte[] md5Hash = algorithm.Hash;
            string md5HashHex = string.Join(string.Empty, md5Hash.Select(b => b.ToString("x2")));

            return md5HashHex;
        }

        public int waveMicBuffer()
        {
            return waveBuffer;
        }

        public void micStart(int mValue)
        {
            clientID++;
            var inDevices = WaveIn.Devices;
            WavInDevice inDevice = inDevices[mValue];
            Console.WriteLine(string.Format("\n{0} , client : {1} , Mic : {2}", DateTime.Now.ToShortTimeString(), clientID, inDevice.Name));
            if (waveIn != null)
            {
                if (!isRecordVoice)
                {
                    waveIn.Start();
                    isRecordVoice = true;
                }

                return;
            }
            try
            {
                waveIn = new WaveIn(inDevice, 8000, 8, 1, waveBuffer); // 8kHz * 2Byte (16bit/sample) * 1 channel * x (s) = 8000 Byte => x=500ms
                waveIn.BufferFull += waveIn_BufferFull;
                isRecordVoice = true;
                waveIn.Start();
            }
            catch (Exception exception)
            {
                if (waveIn != null)
                    waveIn.Dispose();

                Console.WriteLine(exception.Message, exception.GetType().FullName);
                return;
            }

            //micWaveIn = waveIn;
        }

        public void waveIn_BufferFull(byte[] buffer)
        {
            try
            {
                newMicBuffer = null;
                tempMicBuffer = null;
                newMicBuffer = (byte[])buffer.Clone();
                tempMicBuffer = newMicBuffer;

                waveMD5 = CalculateMD5(tempMicBuffer);

                long bytes = Process.GetCurrentProcess().WorkingSet64;
                bytes = (long)((bytes / 1024f) / 1024f);
                Console.Write(string.Format("\r{0} , ({1} Mbytes) physical memory allocated. ", DateTime.Now.ToShortTimeString(), bytes));
            }
            catch (Exception ex)
            {
                // do nill;

            }
        }

        public string getWaveMD5()
        {
            return waveMD5;
        }

        byte[] IWebCamService.getMicBuffer()
        {
            sendingMicBuffer = tempMicBuffer;
            if (sendingMicBuffer != null)
            {
                byte[] ms;
                lock (sendingMicBuffer)
                {
                    ms = (byte[])sendingMicBuffer.Clone();
                }
                //newMicBuffer = null;
                sendingMicBuffer = null;


                long bytes = Process.GetCurrentProcess().WorkingSet64;
                bytes = (long)((bytes / 1024f) / 1024f);
                Console.Write(string.Format("\r{0} , ({1} Mbytes) physical memory allocated (buffer={2}). ", DateTime.Now.ToShortTimeString(), bytes, ms.Length));

                return ms;

            }
            else
            {
                return null;
            }

        }

        public void stop_Mic()
        {
            clientID--;
            if (clientID <= 0)
            {
                clientID = 0;
                if (waveIn != null)
                {
                    //micWaveIn.Stop();
                    waveIn.Stop();
                    isRecordVoice = false;
                    //micWaveIn.Dispose();
                    //waveIn.Dispose();
                    newMicBuffer = null;
                    //micWaveIn = null;
                    //waveIn = null;
                    long mm = GC.GetTotalMemory(true);
                    Console.WriteLine(string.Format("\n{0} , ({1} bytes) last Connection closed. ", DateTime.Now.ToShortTimeString(), mm));
                }
            }
            else
            {
                long mm = GC.GetTotalMemory(true);
                Console.WriteLine(string.Format("\n{0} , client(s) {1} , ({2} bytes) Connection closed. ", DateTime.Now.ToShortTimeString(), clientID, mm));
            }
        }

        #endregion

        #region Chat Server

        public void chatStart(string loginName, ref DateTime date)
        {
            lastDevType = "chat";

            startCheckUserTimer();

            if (clientUserBuffer != null)
            {
                if (clientUserBuffer.FindIndex(p => p.LoginName == loginName) >= 0)
                {
                    clientUserBuffer.RemoveAll(p => p.LoginName == loginName);
                    clientID--;
                }
            }

            clientID++;

            Console.WriteLine(string.Format("\n{0} , client : {1} , Name : {2}", DateTime.Now.ToShortTimeString(), clientID, loginName));
            date = DateTime.Now;

            if (clientID == 1)
            {
                chatTextBuffer = new List<bufferTextList>();
                clientUserBuffer = new List<bufferUserList>();
                bufferUserList newuser = new bufferUserList() { LoginName = loginName, DateIn = date, DateStamp = date };
                clientUserBuffer.Add(newuser);

                return;
            }

            if (clientUserBuffer.FindIndex(p => p.LoginName == loginName) >= 0)
                clientUserBuffer.RemoveAll(p => p.LoginName == loginName);

            bufferUserList mynewuser = new bufferUserList() { LoginName = loginName, DateIn = date, DateStamp = date };
            clientUserBuffer.Add(mynewuser);
        }

        public void setNewChatLine(string loginName, string chatText, ref DateTime date)
        {
            if (chatTextBuffer.Count > 100)
                chatTextBuffer.RemoveRange(0, 50);

            bufferTextList newline = new bufferTextList() { Content = chatText, LoginName = loginName, DateIn = DateTime.Now };

            chatTextBuffer.Add(newline);
            date = newline.DateIn;

            return;
        }

        public string getNewChatLine(string loginName, ref DateTime lastDate)
        {
            string NewLine = "Null";

            int indexUser = clientUserBuffer.FindIndex(p => p.LoginName == loginName);
            DateTime dtIn = DateTime.Now;
            DateTime dt = dtIn;
            if (indexUser >= 0)
            {
                dtIn = clientUserBuffer[indexUser].DateIn;
                clientUserBuffer.RemoveAll(p => p.LoginName == loginName);
            }
            bufferUserList mynewuser = new bufferUserList() { LoginName = loginName, DateIn = dtIn, DateStamp = dt };
            clientUserBuffer.Add(mynewuser);

            if (chatTextBuffer.Count == 0)
            {
                lastDate = DateTime.Now;
                return NewLine;
            }

            string chatLines = "";
            int listCount = chatTextBuffer.Count;

            for (int i = (listCount - 1); i >= 0; i--)
            {
                if (chatTextBuffer[i].DateIn >= lastDate)
                {
                    //chatLines = chatTextBuffer[i].LoginName + ": " + chatTextBuffer[i].Text + "\r\n" + chatLines;
                    chatLines = string.Format("[{0}]@@@{1}@@@{2}§§§", chatTextBuffer[i].DateIn, chatTextBuffer[i].LoginName, chatTextBuffer[i].Content) + chatLines;
                }
                else
                    break;
            }
            if (chatLines != "")
                NewLine = chatLines;


            lastDate = DateTime.Now;
            /*
                        var json = new JavaScriptSerializer().Serialize(chatTextBuffer);
                        NewLine = json.ToString();
            */
            if (chatTextBuffer.Count > 100)
                chatTextBuffer.RemoveRange(0, 50);

            return NewLine;
        }

        public string getNewUserList()
        {
            if (clientUserBuffer.Count == 0)
            {
                return "Null";
            }

            /*string chatUsers = "";
            int userCount = chatUserBuffer.Count;

            for (int i = (userCount - 1); i >= 0; i--)
            {
                chatUsers = string.Format("[{0}]@@@{1}§§§", chatUserBuffer[i].DateIn, chatUserBuffer[i].LoginName) + chatUsers;
            }
            if (chatUsers != "")
                NewLine = chatUsers;
            */

            var json = new JavaScriptSerializer().Serialize(clientUserBuffer);

            return json;
            //return NewLine;
        }

        public void stop_Chat(string lName)
        {
            clientID--;

            if (clientID <= 0)
            {
                clientID = 0;
                if (chatTextBuffer != null)
                {
                    chatTextBuffer = null;
                    long mm = GC.GetTotalMemory(true);
                    Console.WriteLine(string.Format("\n{0} , ({1} bytes) last Connection closed. ", DateTime.Now.ToShortTimeString(), mm));
                    clientUserBuffer.RemoveAll(p => p.LoginName == lName);

                }
            }
            else
            {
                long mm = GC.GetTotalMemory(true);
                Console.WriteLine(string.Format("\n{0} , client(s) {1} , ({2} bytes) Connection closed. ", DateTime.Now.ToShortTimeString(), clientID, mm));
                clientUserBuffer.RemoveAll(p => p.LoginName == lName);
            }
        }

        #endregion

        #region Arduino Server

        public void arduinoStart(string loginName, string aPort, string lID, string cID, string userPermission, ref DateTime date)
        {
            lastDevType = "arduino";

            startCheckUserTimer();

            if (clientUserBuffer != null)
            {
                if (clientUserBuffer.FindIndex(p => p.LoginName == loginName) >= 0)
                {
                    clientUserBuffer.RemoveAll(p => p.LoginName == loginName);
                    clientID--;
                }
            }

            clientID++;

            lastLabID = lID;
            lastDevID = cID;

            AddUserToUserLiveLog(loginName);

            Console.WriteLine(string.Format("\n{0} , client : {1} , Name : {2}", DateTime.Now.ToShortTimeString(), clientID, loginName));
            date = DateTime.Now;

            if (clientID == 1)
            {
                arduinoSourceCodeBuffer = new List<bufferTextList>();
                arduinoSerialDataBuffer = new List<bufferTextList>();
                clientUserBuffer = new List<bufferUserList>();
                bufferUserList newuser = new bufferUserList() { LoginName = loginName, Permission = userPermission, DateIn = date, DateStamp = date };
                clientUserBuffer.Add(newuser);

                if (userPermission == "rw")
                {
                    arduinoPort = aPort;
                    arduinoSourceFile = getArduinoTempFile(loginName);
                    batchFilePath = createBatchFiles(arduinoSourceFile, aPort);
                }
                arduinoBoardInterface = new ArduinoBoard(arduinoPort);
                arduinoBoardInterface.OpenArduinoConnection();
                arduinoBoardInterface.arduinoDataReceived += serialData_NewarduinoDataReceived;

                return;
            }

            if (clientUserBuffer.FindIndex(p => p.LoginName == loginName) >= 0)
                clientUserBuffer.RemoveAll(p => p.LoginName == loginName);

            bufferUserList mynewuser = new bufferUserList() { LoginName = loginName, DateIn = date, Permission = userPermission, DateStamp = date };
            clientUserBuffer.Add(mynewuser);
            if (userPermission == "rw")
            {
                arduinoPort = aPort;
                arduinoSourceFile = getArduinoTempFile(loginName);
                batchFilePath = createBatchFiles(arduinoSourceFile, aPort);
            }

        }

        public string getArduinoUserList(string loginName)
        {
            int indexUser = clientUserBuffer.FindIndex(p => p.LoginName == loginName);
            DateTime dtIn = DateTime.Now;
            DateTime dt = dtIn;
            string userPermission = "";
            if (indexUser >= 0)
            {
                dtIn = clientUserBuffer[indexUser].DateIn;
                userPermission = clientUserBuffer[indexUser].Permission;
                clientUserBuffer.RemoveAll(p => p.LoginName == loginName);
            }
            bufferUserList mynewuser = new bufferUserList() { LoginName = loginName, DateIn = dtIn, Permission = userPermission, DateStamp = dt };
            clientUserBuffer.Add(mynewuser);

            if (clientUserBuffer.Count == 0)
            {
                return "Null";
            }

            var t = Task<string>.Factory.StartNew(() =>
            {
                return new JavaScriptSerializer().Serialize(clientUserBuffer);
            });

            return t.Result;
        }

        private void serialData_NewarduinoDataReceived(object sender, EventArgs e)
        {
            string newdata = (string)sender;//arduinoBoardInterface.getSerialDataBuffer();

            if (arduinoSerialDataBuffer.Count > 200)
                arduinoSerialDataBuffer.RemoveRange(0, 100);

            bufferTextList newline = new bufferTextList() { Content = newdata, LoginName = "Arduino", DateIn = DateTime.Now, Permission = "Arduino" };

            arduinoSerialDataBuffer.Add(newline);

            SerialDataLog("Arduino", newdata);
        }

        public string getNewSerialData(string loginName, ref DateTime lastDate)
        {
            string NewLine = "Null";

            int indexUser = clientUserBuffer.FindIndex(p => p.LoginName == loginName);
            DateTime dtIn = DateTime.Now;
            DateTime dt = dtIn;
            string userPermission = "";
            if (indexUser >= 0)
            {
                dtIn = clientUserBuffer[indexUser].DateIn;
                userPermission = clientUserBuffer[indexUser].Permission;
                clientUserBuffer.RemoveAll(p => p.LoginName == loginName);
            }
            bufferUserList mynewuser = new bufferUserList() { LoginName = loginName, DateIn = dtIn, Permission = userPermission, DateStamp = dt };
            clientUserBuffer.Add(mynewuser);

            if (arduinoSerialDataBuffer.Count == 0)
            {
                lastDate = DateTime.Now;
                return NewLine;
            }

            string serialDataLines = "";
            int listCount = arduinoSerialDataBuffer.Count;
            dt = DateTime.Now;

            for (int i = (listCount - 1); i >= 0; i--)
            {
                if (arduinoSerialDataBuffer[i].DateIn >= lastDate)
                {
                    serialDataLines = string.Format("[{0}]@@@{1}@@@{2}§§§",
                                                    arduinoSerialDataBuffer[i].DateIn,
                                                    arduinoSerialDataBuffer[i].LoginName,
                                                    arduinoSerialDataBuffer[i].Content) +
                                                    serialDataLines;
                    dt = arduinoSerialDataBuffer[i].DateIn;
                }
                else
                    break;
            }
            if (serialDataLines != "")
                NewLine = serialDataLines;


            lastDate = dt.AddMilliseconds(10);//DateTime.Now;
            /*
                        var json = new JavaScriptSerializer().Serialize(chatTextBuffer);
                        NewLine = json.ToString();
            */
            if (arduinoSerialDataBuffer.Count > 200)
                arduinoSerialDataBuffer.RemoveRange(0, 100);

            return NewLine;
        }

        public void sendSerialData(string loginName, string serialText, ref DateTime date)
        {
            if (isUploading)
                return;

            arduinoBoardInterface.sendDataToArduinoBoard(serialText + Environment.NewLine);
            date = DateTime.Now;
        }

        public void stop_arduino(string lName)
        {
            clientID--;

            RemoveUserFromUserLiveLog(lName);

            if (clientID <= 0)
            {
                clientID = 0;
                if (arduinoSourceCodeBuffer != null)
                {
                    arduinoSourceCodeBuffer = null;
                    if (arduinoBoardInterface != null)
                    {
                        arduinoBoardInterface.CloseArduinoConnection();
                        arduinoBoardInterface.arduinoDataReceived -= serialData_NewarduinoDataReceived;
                    }
                    long mm = GC.GetTotalMemory(true);
                    Console.WriteLine(string.Format("\n{0} , ({1} bytes) last Connection closed. ", DateTime.Now.ToShortTimeString(), mm));
                    clientUserBuffer.RemoveAll(p => p.LoginName == lName);
                    deleteTempFlies();
                }
            }
            else
            {
                long mm = GC.GetTotalMemory(true);
                Console.WriteLine(string.Format("\n{0} , client(s) {1} , ({2} bytes) Connection closed. ", DateTime.Now.ToShortTimeString(), clientID, mm));
                clientUserBuffer.RemoveAll(p => p.LoginName == lName);
            }
        }

        private string getArduinoTempFile(string loginname)
        {
            string tempFileName = string.Format("{0}_{1:hh-mm-ss}", loginname, DateTime.Now);
            return tempFileName;
        }

        public string arduinoCompiling(string sourceCode)
        {
            //sourceCode = File.OpenText(@"C:\Users\Soheyl\Dropbox\Programming\Arduino\ProRobot\ProRobot.ino").ReadToEnd();
            bool isLocked = true;
            createArduinoSourceFile(sourceCode, batchFilePath);
            if (File.Exists(Path.Combine(batchFilePath, arduinoSourceFile) + ".compile"))
            {
                while (isLocked)
                {
                    try
                    {
                        File.Delete(Path.Combine(batchFilePath, arduinoSourceFile) + ".compile");
                        isLocked = false;
                    }
                    catch { }
                }
            }
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.FileName = Path.Combine(batchFilePath, arduinoSourceFile) + ".bat";
            p.StartInfo.CreateNoWindow = true;
            //p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Start();

            isLocked = true;
            string filename = Path.Combine(batchFilePath, arduinoSourceFile) + ".compile";
            string filename2 = Path.Combine(batchFilePath, arduinoSourceFile) + "_temp.compile";

            while (isLocked)
            {
                try
                {
                    File.Move(filename, filename2);
                    isLocked = false;
                }
                catch { }
            }
            File.Move(filename2, filename);

            string str = filterOutputCompile(File.OpenText(filename).ReadToEnd());

            return str;
        }

        private string filterOutputCompile(string outputCompile)
        {
            string str = Path.GetTempPath();
            outputCompile = outputCompile.Replace(Path.Combine(batchFilePath, arduinoSourceFile) + ".ino.hex", "compiledHex.hex");
            outputCompile = outputCompile.Replace(arduinoSourceFile + ".ino", "sourceCode");
            outputCompile = outputCompile.Replace(arduinoSourceFile, "sourceCode");
            outputCompile = outputCompile.Replace(batchFilePath, "defaultPath");
            outputCompile = outputCompile.Replace(str.Substring(0, str.Length - 1), @"\temp");

            return outputCompile;
        }

        public string arduinoUploading()
        {
            isUploading = true;
            arduinoBoardInterface.CloseArduinoConnection();
            arduinoBoardInterface.arduinoDataReceived -= serialData_NewarduinoDataReceived;
            bool isLocked = true;
            if (File.Exists(Path.Combine(batchFilePath, arduinoSourceFile) + ".upload"))
            {
                while (isLocked)
                {
                    try
                    {
                        File.Delete(Path.Combine(batchFilePath, arduinoSourceFile) + ".upload");
                        isLocked = false;
                    }
                    catch { }
                }
                //File.Delete(Path.Combine(batchFilePath, arduinoSourceFile) + ".upload");
            }
            Thread.Sleep(500);
            Process p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.FileName = Path.Combine(batchFilePath, arduinoSourceFile) + "_Upload.bat";
            p.StartInfo.CreateNoWindow = true;
            //p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Start();

            string filename = Path.Combine(batchFilePath, arduinoSourceFile) + ".upload";
            string filename2 = Path.Combine(batchFilePath, arduinoSourceFile) + "_temp.upload";

            isLocked = true;
            while (isLocked)
            {
                try
                {
                    File.Move(filename, filename2);
                    isLocked = false;
                }
                catch { }
            }

            File.Move(filename2, filename);

            Thread.Sleep(1000);

            arduinoBoardInterface = new ArduinoBoard(arduinoPort);
            arduinoBoardInterface.OpenArduinoConnection();
            arduinoBoardInterface.arduinoDataReceived += serialData_NewarduinoDataReceived;
            isUploading = false;

            string str = filterOutputUpload(File.OpenText(filename).ReadToEnd());

            return str;
        }

        private string filterOutputUpload(string outputUpload)
        {
            string str = Path.GetTempPath();
            outputUpload = outputUpload.Replace(Path.Combine(batchFilePath, arduinoSourceFile) + ".ino.hex", "compiledHex.hex");
            outputUpload = outputUpload.Replace(arduinoSourceFile + ".ino", "sourceCode");
            outputUpload = outputUpload.Replace(arduinoSourceFile, "sourceCode");
            outputUpload = outputUpload.Replace(batchFilePath, "default Path");
            outputUpload = outputUpload.Replace(str.Substring(0, str.Length - 1), @"\temp");

            return outputUpload;
        }

        private void createArduinoSourceFile(string sourceCode, string batchFilePath)
        {
            string sourceFilePath = Path.Combine(batchFilePath, arduinoSourceFile) + ".ino";
            StreamWriter w = new StreamWriter(sourceFilePath);
            w.WriteLine(sourceCode);
            w.Close();
        }

        private string createBatchFiles(string sourceFile, string aPort)
        {
            string tempWorkingBuildPath = Path.Combine(arduinoBuildPath, DateTime.Now.ToString("yyyy-MM-dd"), aPort);

            if (!Directory.Exists(tempWorkingBuildPath))
            {
                DirectoryInfo di = Directory.CreateDirectory(tempWorkingBuildPath);
                tempWorkingBuildPath = di.FullName;
            }

            string tempBatchFile = sourceFile + ".bat";

            tempBatchFile = Path.Combine(tempWorkingBuildPath, tempBatchFile);
            string arduinoFullPath = Path.Combine(arduinoCompilerPath, arduinoCompilerFile);

            string arduinoSourceFile = sourceFile + ".ino";
            arduinoSourceFile = Path.Combine(tempWorkingBuildPath, arduinoSourceFile);

            string arduinoCompileFile = sourceFile + ".compile";
            arduinoCompileFile = Path.Combine(tempWorkingBuildPath, arduinoCompileFile);

            string oneLineCommand = string.Format("{0} {1} > {2} 2>&1", arduinoFullPath, arduinoSourceFile, arduinoCompileFile);
            StreamWriter w = new StreamWriter(tempBatchFile);
            w.WriteLine(oneLineCommand);
            w.Close();



            tempBatchFile = sourceFile + "_Upload.bat";
            tempBatchFile = Path.Combine(tempWorkingBuildPath, tempBatchFile);
            string arduinoUploadFile = sourceFile + ".upload";
            arduinoUploadFile = Path.Combine(tempWorkingBuildPath, arduinoUploadFile);

            oneLineCommand = string.Format("{0} {1} 2 {2} > {3} 2>&1", arduinoFullPath, arduinoSourceFile, aPort, arduinoUploadFile); ;
            w = new StreamWriter(tempBatchFile);
            w.WriteLine(oneLineCommand);
            w.Close();

            return tempWorkingBuildPath;
        }

        private void deleteTempFlies()
        {
            string mainfile = Path.Combine(batchFilePath, arduinoSourceFile);
            if (File.Exists(mainfile + ".bat"))
            {
                File.Delete(mainfile + ".bat");
            }
            if (File.Exists(mainfile + ".ino"))
            {
                File.Delete(mainfile + ".ino");
            }
            if (File.Exists(mainfile + ".compile"))
            {
                File.Delete(mainfile + ".compile");
            }
            if (File.Exists(mainfile + ".ino.hex"))
            {
                File.Delete(mainfile + ".ino.hex");
            }
            if (File.Exists(mainfile + ".upload"))
            {
                File.Delete(mainfile + ".upload");
            }
            if (File.Exists(mainfile + "_Upload.bat"))
            {
                File.Delete(mainfile + "_Upload.bat");
            }
        }

        #endregion

    }
}

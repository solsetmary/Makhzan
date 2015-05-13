using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace RobotWebCamServer
{
    class WebcamDBService
    {
        private string camName;
        private VideoCaptureDevice videoSource = null;
        private Bitmap newImage;
        System.Timers.Timer timerUploadImage;
        string labID = "1";
        string devID = "1";
        string devIndex = "1";
        string devWidth = "640";
        string devHeigth = "480";

        public WebcamDBService(string labID, string webcamID, string webcamIndex, string basePort, string serverPort)
        {
            this.labID = labID;
            this.devID = webcamID;
            this.devIndex = webcamIndex;
            this.devWidth = basePort;
            this.devHeigth = serverPort;
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
                    camName = videoDevices[Convert.ToInt32(devIndex)].MonikerString;
                    camDescr = videoDevices[Convert.ToInt32(devIndex)].Name;
                }
            }
            catch (ApplicationException)
            {
                throw new ApplicationException("failed web cams initialize");
            }

            videoSource = new VideoCaptureDevice(camName);
            videoSource.NewFrame += new NewFrameEventHandler(video_NewFrame);
            videoSource.DesiredFrameSize = new Size(Convert.ToInt32(devWidth), Convert.ToInt32(devHeigth));//new Size(320, 240);//new Size(480, 360);//new Size(640, 480);//
            videoSource.DesiredFrameRate = 29;
            videoSource.Start();

            if (timerUploadImage == null)
            {
                timerUploadImage = new System.Timers.Timer(700);
                timerUploadImage.Elapsed += new ElapsedEventHandler(timerUploadImage_Tick);
                timerUploadImage.Start();
            }
        }


        public void Stop()
        {
            if (!(videoSource == null))
            {
                if (videoSource.IsRunning)
                {
                    long mm = GC.GetTotalMemory(true);
                    Console.WriteLine(string.Format("\n{0} , ({1} bytes) webcam closed. ", DateTime.Now.ToShortTimeString(), mm));
                    videoSource.SignalToStop();
                    newImage = null;
                    videoSource = null;
                    timerUploadImage.Stop();
                    timerUploadImage = null;
                }
            }
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                newImage = (Bitmap)eventArgs.Frame.Clone();
            }
            catch (Exception ex)
            {
                // do nill;
            }
        }

        private void timerUploadImage_Tick(object sender, ElapsedEventArgs e)
        {
            timerUploadImage.Stop();
            timerUploadImage.Enabled = false;
            setDBimages();
        }


        private void setDBimages()
        {
            bool isStreamCamera = true;

            new Thread(delegate()
            {
                System.Diagnostics.Stopwatch swFrameRate = new System.Diagnostics.Stopwatch();
                swFrameRate.Start();
                while (isStreamCamera)
                {
                    if (swFrameRate.Elapsed.TotalMilliseconds >= 100)
                    {
                        swFrameRate.Restart();

                        MemoryStream ms = new MemoryStream();
                        if (newImage != null)
                        {
                            lock (newImage)
                            {
                                lock (ms)
                                {
                                    //new Bitmap(newImage, new Size(320, 240)).Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    //new Bitmap(newImage, new Size(480, 360)).Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    //new Bitmap(newImage, new Size(640, 480)).Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    new Bitmap(newImage, new Size(Convert.ToInt32(devWidth), Convert.ToInt32(devHeigth))).Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    ms.Position = 0;
                                }
                            }

                            try
                            {
                                var t = Task.Factory.StartNew(() =>
                                {
                                    byte[] imageBytes = ms.ToArray();
                                    string strImage = Convert.ToBase64String(imageBytes);
                                    System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
                                    reqparm.Add("labID", labID);
                                    reqparm.Add("devID", devID);
                                    reqparm.Add("image", strImage);
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
                }
            }).Start();
        }
    }
}

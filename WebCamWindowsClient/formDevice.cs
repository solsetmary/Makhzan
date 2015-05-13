using LumiSoft.Media.Wave;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WebCamWindowsClient
{
    public partial class formDevice : Form
    {
        private WebCamService.WebCamServiceClient client;
        private WebCamService.WebCamServiceClient clientMic;
        private int counter;
        private int counterEx;
        private int samplespersec = 8000;
        private int bitspersample = 8;
        private int wavechannel = 1;
        private int waveBuffer = 8000;
        private float syncTimeing = 100;
        public int timerInterval = 500;
        public string loginName;
        public string newIP;
        public string lastPort;
        public string lastValue;
        public string lastText;
        public string lastLab;
        public string lastLabID;
        public string lastDevID;
        public string micPort;
        public string micIndex;
        public string micText;
        private int speakerID;
        public bool isImageDelay = false;
        public bool isCalculateImageDelay = false;
        public bool isPlayVoice = true;
        public RotateFlipType rotationDirection;
        private webcamImage localWebcamImage;

        Queue webcamImageQueue = new Queue();
        System.Diagnostics.Stopwatch swDelay = new System.Diagnostics.Stopwatch();

        delegate void DeviceShowCloseDelegate();

        [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
        static extern int memcmp(byte[] b1, byte[] b2, long count);

        private bool ByteArrayCompare(byte[] b1, byte[] b2)
        {
            try
            {
                return b1.Length == b2.Length && memcmp(b1, b2, b1.Length) == 0;
            }
            catch
            {
                return false;
            }
        }

        private class ComboboxItem
        {
            public string Text { get; set; }
            public RotateFlipType Value { get; set; }
            public override string ToString() { return Text; }
        }

        private struct webcamImage
        {
            internal Image image;
        }

        class imageResponse
        {
            public String first;
            public String second;
            public String third;
        }

        private DialogResult InputBox(string title, string promptText, ref string value)
        {
            Form form = new Form();
            Label label = new Label();
            TextBox textBox = new TextBox();
            Button buttonOk = new Button();
            Button buttonCancel = new Button();

            form.Text = title;
            label.Text = promptText;
            textBox.Text = value;

            buttonOk.Text = "OK";
            buttonCancel.Text = "Cancel";
            buttonOk.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            label.SetBounds(9, 10, 372, 85);
            textBox.SetBounds(9, 100, 372, 20);
            buttonOk.SetBounds(228, 135, 75, 23);
            buttonCancel.SetBounds(309, 135, 75, 23);

            label.AutoSize = true;
            textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
            buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

            form.ClientSize = new Size(396, 170);
            form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
            form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
            form.FormBorderStyle = FormBorderStyle.FixedDialog;
            form.StartPosition = FormStartPosition.CenterScreen;
            form.MinimizeBox = false;
            form.MaximizeBox = false;
            form.AcceptButton = buttonOk;
            form.CancelButton = buttonCancel;

            DialogResult dialogResult = form.ShowDialog();
            value = textBox.Text;
            return dialogResult;
        }

        public void setParameters(string lName, string theIP, string cPort, string cValue, string cText, string lText, string lLabID, string lDevID, string mIndex, string mText, string mPort, string mLab)
        {
            loginName = lName;
            newIP = theIP;
            lastPort = cPort;
            lastValue = cValue;
            lastText = cText;
            lastLab = lText;
            lastLabID = lLabID;
            lastDevID = lDevID;
            if (lastLabID == mLab)
            {
                //buttonMic.Enabled = true;
                micIndex = mIndex;
                micText = mText;
                micPort = mPort;
            }
            this.Text = string.Format("{0} - {1}", lText, cText);
        }

        public Image Base64ToImage(string base64String)
        {
            try
            {
                string str = base64String.Substring(0, base64String.Length).Replace(@"\/", "/");
                // Convert Base64 String to byte[]
                byte[] imageBytes = Convert.FromBase64String(str);
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);
                Image image = Image.FromStream(ms, true);
                return image;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Displays the formDevice
        /// </summary>
        public void ShowFormDevice()
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new DeviceShowCloseDelegate(ShowFormDevice));
                return;
            }
            this.Show();
            Application.Run(this);
        }

        /// <summary>
        /// Closes the formDevice
        /// </summary>
        public void CloseFormDevice()
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new DeviceShowCloseDelegate(CloseFormDevice));
                return;
            }
            this.Close();
        }

        private void getStreamCamera()
        {

            if (lblCamerasNr.InvokeRequired)
            {
                lblCamerasNr.BeginInvoke((MethodInvoker)delegate
                {
                    lblCamerasNr.Text = "Streamming . . .";
                    lblCamerasNr.Refresh();
                });
            }
            else
            {
                lblCamerasNr.Text = "Streamming . . .";
                lblCamerasNr.Refresh();
            }

            try
            {
                /*string myRemoteIP = string.Format("http://{0}:{1}/RobotWebCamServer", newIP, lastPort);
                client = new WebCamService.WebCamServiceClient("NetTcpBinding_IWebCamService", myRemoteIP);
                client.Record(loginName, lastValue, lastText, lastLabID, lastDevID);*/
            }
            catch (System.ServiceModel.CommunicationException ex)
            { MessageBox.Show(ex.Message); return; }

            Thread.Sleep(50);

            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
            Thread.Sleep(25);
            if (micIndex != null)
            {
                buttonMic.Enabled = true;
                toolStripMenuItemMic.Enabled = true;
            }
        }

        private void getStreamMic()
        {
            if (micIndex != null)
            {
                var outDevices = WaveOut.Devices;
                if (outDevices.Length == 0)
                {
                    MessageBox.Show("There are no output sound devices installed.");
                    return;
                }
                /*string outDevicesRes = "";
                string resValue = "0";
                for (int i = 0; i < outDevices.Length; i++)
                {
                    outDevicesRes += "\r\n" + i + " : " + outDevices[i].Name;
                }
                if (outDevices.Length == 1)
                {
                    speakerID = 0;
                }
                else if (InputBox("Select your Speaker ID", outDevicesRes, ref resValue) == DialogResult.OK)
                {
                    speakerID = Convert.ToInt32(resValue);
                }*/
                speakerID = 0;
            }
            else
            {
                MessageBox.Show("Fatal Error!!! Please try again.");
                return;
            }
            try
            {
                string myRemoteIP = string.Format("http://{0}:{1}/RobotWebCamServer", newIP, micPort);
                clientMic = new WebCamService.WebCamServiceClient("NetTcpBinding_IWebCamService", myRemoteIP);
                clientMic.micStart(Convert.ToInt32(micIndex));
                waveBuffer = clientMic.waveMicBuffer();
            }
            catch (System.ServiceModel.CommunicationException ex)
            {
                MessageBox.Show(ex.Message); return;
            }

            Thread.Sleep(30);
            isCalculateImageDelay = true;
            timerMic.Tick += new EventHandler(timerMic_Tick);
            timerMic.Start();
        }

        public formDevice()
        {
            InitializeComponent();
            counter = 0;
            counterEx = 0;
        }

        private void buttonIncreaseTime_Click(object sender, EventArgs e)
        {
            //slower
            if (timerInterval < 2000)
                timerInterval += 100;
            else
                buttonIncreaseTime.Enabled = false;
            if (timerInterval > 100)
                buttonDecreaseTime.Enabled = true;

            timer1.Interval = timerInterval;
        }

        private void buttonDecreaseTime_Click(object sender, EventArgs e)
        {
            //faster
            if (timerInterval > 100)
                timerInterval -= 100;
            else
                buttonDecreaseTime.Enabled = false;
            if (timerInterval < 2000)
                buttonIncreaseTime.Enabled = true;

            timer1.Interval = timerInterval;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            timer1.Enabled = false;
            //runCameraStream();
            getDBimages();
            return;

            ThreadPool.QueueUserWorkItem(delegate
                    {
                        counter++;
                        if (client == null)
                            return;
                        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                        lock (client)
                        {
                            try
                            {
                                sw.Start();
                                Stream imageStream = client.getWebCamImage(loginName);

                                lock (imageStream)
                                {

                                    Image bm = Bitmap.FromStream(imageStream);
                                    bm.RotateFlip(rotationDirection);

                                    RectangleF rectf = new RectangleF(0, bm.Size.Height - 50, 350, 50);
                                    Graphics g = Graphics.FromImage(bm);
                                    g.SmoothingMode = SmoothingMode.AntiAlias;
                                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                    sw.Stop();
                                    double d = Math.Floor(sw.Elapsed.TotalMilliseconds);
                                    g.DrawString(string.Format("Lab++.org , {0} ms", d), new Font("Tahoma", 18), Brushes.Yellow, rectf);
                                    g.Flush();

                                    localWebcamImage.image = bm;
                                    webcamImageQueue.Enqueue(localWebcamImage);

                                    if (!isImageDelay)
                                    {
                                        webcamImage imageQueue = (webcamImage)webcamImageQueue.Dequeue();
                                        lock (pictureBox1)
                                        {
                                            if (pictureBox1.InvokeRequired)
                                            {
                                                pictureBox1.BeginInvoke((MethodInvoker)delegate
                                                {
                                                    pictureBox1.Image = imageQueue.image;
                                                });
                                            }
                                            else
                                            {
                                                pictureBox1.Image = imageQueue.image;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (swDelay.Elapsed.TotalMilliseconds >= syncTimeing)
                                        {
                                            swDelay.Stop();
                                            isImageDelay = false;
                                        }
                                    }
                                }

                            }
                            catch (System.ServiceModel.CommunicationException ex)
                            {
                                counterEx++;
                                if (ex.InnerException is System.ServiceModel.QuotaExceededException)
                                {
                                    if (lblCamerasNr.InvokeRequired)
                                    {
                                        lblCamerasNr.BeginInvoke((MethodInvoker)delegate
                                        {
                                            lblCamerasNr.Text = string.Format("{0}, {1}, Exceeded on Service Communication!!!", counter, counterEx);
                                            lblCamerasNr.Refresh();
                                        });
                                    }
                                    else
                                    {
                                        lblCamerasNr.Text = string.Format("{0}, {1}, Exceeded on Service Communication!!!", counter, counterEx);
                                        lblCamerasNr.Refresh();
                                    }
                                }
                                else
                                {
                                    if (lblCamerasNr.InvokeRequired)
                                    {
                                        lblCamerasNr.BeginInvoke((MethodInvoker)delegate
                                        {
                                            lblCamerasNr.Text = string.Format("{0}, {1}, Streamming . . .", counter, counterEx);// +ex.Message;
                                            lblCamerasNr.Refresh();
                                        });
                                    }
                                    else
                                    {
                                        lblCamerasNr.Text = string.Format("{0}, {1}, Streamming . . .", counter, counterEx);// +ex.Message;
                                        lblCamerasNr.Refresh();
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                counterEx++;
                                timer1.Stop();
                                try
                                {
                                    if (client != null)
                                        client.stop_record(loginName);
                                }
                                catch
                                {
                                    //client.Abort();
                                }
                                if (lblCamerasNr.InvokeRequired)
                                {
                                    lblCamerasNr.BeginInvoke((MethodInvoker)delegate
                                    {
                                        lblCamerasNr.Text = string.Format("{0}, {1}, Error on connection. Try again!!!", counter, counterEx);
                                        lblCamerasNr.Refresh();
                                    });
                                }
                                else
                                {
                                    lblCamerasNr.Text = string.Format("{0}, {1}, Error on connection. Try again!!!", counter, counterEx);
                                    lblCamerasNr.Refresh();
                                }
                            }
                        }
                    }, null);
        }

        private void getDBimages()
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
                        counter++;
                        //if (client == null)
                        //    return;
                        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                        //lock (client)
                        //{
                            try
                            {
                                sw.Start();

                                string responseBody = "";
                                using (WebClient wclient = new WebClient())
                                {
                                    System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
                                    reqparm.Add("first", lastLabID);
                                    reqparm.Add("second", lastDevID);
                                    byte[] responseBytes = wclient.UploadValues("http://labpp.org/get/new/images/", "POST", reqparm);
                                    responseBody = Encoding.UTF8.GetString(responseBytes);
                                }
                                List<imageResponse> allImages = new JavaScriptSerializer().Deserialize<List<imageResponse>>(responseBody);
                                Image bm = Base64ToImage(allImages[0].third);

                                    bm.RotateFlip(rotationDirection);

                                    RectangleF rectf = new RectangleF(0, bm.Size.Height - 20, 350, 50);
                                    Graphics g = Graphics.FromImage(bm);
                                    g.SmoothingMode = SmoothingMode.AntiAlias;
                                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                    sw.Stop();
                                    double d = Math.Floor(sw.Elapsed.TotalMilliseconds);
                                    g.DrawString(string.Format("Lab++.org , {0} ms", d), new Font("Tahoma", 12), Brushes.Yellow, rectf);
                                    g.Flush();

                                    localWebcamImage.image = bm;
                                    webcamImageQueue.Enqueue(localWebcamImage);

                                    if (!isImageDelay)
                                    {
                                        webcamImage imageQueue = (webcamImage)webcamImageQueue.Dequeue();
                                        lock (pictureBox1)
                                        {
                                            if (pictureBox1.InvokeRequired)
                                            {
                                                pictureBox1.BeginInvoke((MethodInvoker)delegate
                                                {
                                                    pictureBox1.Image = imageQueue.image;
                                                });
                                            }
                                            else
                                            {
                                                pictureBox1.Image = imageQueue.image;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (swDelay.Elapsed.TotalMilliseconds >= syncTimeing)
                                        {
                                            swDelay.Stop();
                                            isImageDelay = false;
                                        }
                                    }
                                //}

                            }
                            catch (System.ServiceModel.CommunicationException ex)
                            {
                                counterEx++;
                                if (ex.InnerException is System.ServiceModel.QuotaExceededException)
                                {
                                    isStreamCamera = false;
                                    if (lblCamerasNr.InvokeRequired)
                                    {
                                        lblCamerasNr.BeginInvoke((MethodInvoker)delegate
                                        {
                                            lblCamerasNr.Text = string.Format("{0}, {1}, Exceeded on Service Communication!!!", counter, counterEx);
                                            lblCamerasNr.Refresh();
                                        });
                                    }
                                    else
                                    {
                                        lblCamerasNr.Text = string.Format("{0}, {1}, Exceeded on Service Communication!!!", counter, counterEx);
                                        lblCamerasNr.Refresh();
                                    }
                                }
                                else
                                {
                                    if (lblCamerasNr.InvokeRequired)
                                    {
                                        lblCamerasNr.BeginInvoke((MethodInvoker)delegate
                                        {
                                            lblCamerasNr.Text = string.Format("{0}, {1}, Streamming . . .", counter, counterEx);// +ex.Message;
                                            lblCamerasNr.Refresh();
                                        });
                                    }
                                    else
                                    {
                                        lblCamerasNr.Text = string.Format("{0}, {1}, Streamming . . .", counter, counterEx);// +ex.Message;
                                        lblCamerasNr.Refresh();
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                counterEx++;
                                /*isStreamCamera = false;
                                timer1.Stop();
                                try
                                {
                                    if (client != null)
                                        client.stop_record(loginName);
                                }
                                catch
                                {
                                    //client.Abort();
                                }*/
                                if (lblCamerasNr.InvokeRequired)
                                {
                                    lblCamerasNr.BeginInvoke((MethodInvoker)delegate
                                    {
                                        lblCamerasNr.Text = string.Format("{0}, {1}, Error on connection. Try again!!!", counter, counterEx);
                                        lblCamerasNr.Refresh();
                                    });
                                }
                                else
                                {
                                    lblCamerasNr.Text = string.Format("{0}, {1}, Error on connection. Try again!!!", counter, counterEx);
                                    lblCamerasNr.Refresh();
                                }
                            }
                        //}
                    }
                }
            }).Start();
        }

        private void runCameraStream()
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
                        counter++;
                        if (client == null)
                            return;
                        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                        lock (client)
                        {
                            try
                            {
                                sw.Start();
                                Stream imageStream = client.getWebCamImage(loginName);

                                lock (imageStream)
                                {

                                    Image bm = Bitmap.FromStream(imageStream);
                                    bm.RotateFlip(rotationDirection);

                                    RectangleF rectf = new RectangleF(0, bm.Size.Height - 20, 350, 50);
                                    Graphics g = Graphics.FromImage(bm);
                                    g.SmoothingMode = SmoothingMode.AntiAlias;
                                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                    sw.Stop();
                                    double d = Math.Floor(sw.Elapsed.TotalMilliseconds);
                                    g.DrawString(string.Format("Lab++.org , {0} ms", d), new Font("Tahoma", 12), Brushes.Yellow, rectf);
                                    g.Flush();

                                    localWebcamImage.image = bm;
                                    webcamImageQueue.Enqueue(localWebcamImage);

                                    if (!isImageDelay)
                                    {
                                        webcamImage imageQueue = (webcamImage)webcamImageQueue.Dequeue();
                                        lock (pictureBox1)
                                        {
                                            if (pictureBox1.InvokeRequired)
                                            {
                                                pictureBox1.BeginInvoke((MethodInvoker)delegate
                                                {
                                                    pictureBox1.Image = imageQueue.image;
                                                });
                                            }
                                            else
                                            {
                                                pictureBox1.Image = imageQueue.image;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (swDelay.Elapsed.TotalMilliseconds >= syncTimeing)
                                        {
                                            swDelay.Stop();
                                            isImageDelay = false;
                                        }
                                    }
                                }

                            }
                            catch (System.ServiceModel.CommunicationException ex)
                            {
                                counterEx++;
                                if (ex.InnerException is System.ServiceModel.QuotaExceededException)
                                {
                                    isStreamCamera = false;
                                    if (lblCamerasNr.InvokeRequired)
                                    {
                                        lblCamerasNr.BeginInvoke((MethodInvoker)delegate
                                        {
                                            lblCamerasNr.Text = string.Format("{0}, {1}, Exceeded on Service Communication!!!", counter, counterEx);
                                            lblCamerasNr.Refresh();
                                        });
                                    }
                                    else
                                    {
                                        lblCamerasNr.Text = string.Format("{0}, {1}, Exceeded on Service Communication!!!", counter, counterEx);
                                        lblCamerasNr.Refresh();
                                    }
                                }
                                else
                                {
                                    if (lblCamerasNr.InvokeRequired)
                                    {
                                        lblCamerasNr.BeginInvoke((MethodInvoker)delegate
                                        {
                                            lblCamerasNr.Text = string.Format("{0}, {1}, Streamming . . .", counter, counterEx);// +ex.Message;
                                            lblCamerasNr.Refresh();
                                        });
                                    }
                                    else
                                    {
                                        lblCamerasNr.Text = string.Format("{0}, {1}, Streamming . . .", counter, counterEx);// +ex.Message;
                                        lblCamerasNr.Refresh();
                                    }
                                }

                            }
                            catch (Exception ex)
                            {
                                isStreamCamera = false;
                                counterEx++;
                                timer1.Stop();
                                try
                                {
                                    if (client != null)
                                        client.stop_record(loginName);
                                }
                                catch
                                {
                                    //client.Abort();
                                }
                                if (lblCamerasNr.InvokeRequired)
                                {
                                    lblCamerasNr.BeginInvoke((MethodInvoker)delegate
                                    {
                                        lblCamerasNr.Text = string.Format("{0}, {1}, Error on connection. Try again!!!", counter, counterEx);
                                        lblCamerasNr.Refresh();
                                    });
                                }
                                else
                                {
                                    lblCamerasNr.Text = string.Format("{0}, {1}, Error on connection. Try again!!!", counter, counterEx);
                                    lblCamerasNr.Refresh();
                                }
                            }
                        }
                    }
                }
            }).Start();
        }

        private void timerMic_Tick(object sender, EventArgs e)
        {
            timerMic.Enabled = false;
            var outDevices = WaveOut.Devices;
            if (outDevices.Length == 0)
            {
                MessageBox.Show("There are no output sound devices installed.");
                return;
            }

            var outDevice = outDevices[speakerID];
            var waveOut = new WaveOut(outDevice, samplespersec, bitspersample, wavechannel);

            new Thread(delegate()
            {
                try
                {
                    //int ittr = 0;
                    byte[] buffer = null;
                    byte[] bufferOld = null;
                    string waveMD5 = "";
                    string waveMD5Old = "";

                    while (isPlayVoice)
                    {
                        //ittr++;
                        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                        sw.Start();
                        if (!(clientMic == null))
                        {
                            if (isCalculateImageDelay)
                            {
                                isCalculateImageDelay = false;
                                syncTimeing = 1000; // ((waveBuffer * wavechannel) / (samplespersec * (bitspersample / 8))) * 1000;
                                syncTimeing = waveBuffer * wavechannel;
                                syncTimeing = syncTimeing / samplespersec;
                                syncTimeing = syncTimeing / (bitspersample / 8);
                                syncTimeing = (syncTimeing * 1000) + syncTimeing * 800;
                                isImageDelay = true;
                                swDelay.Start();
                            }

                            waveMD5 = clientMic.getWaveMD5();

                            if (waveMD5 != waveMD5Old)
                            {
                                buffer = clientMic.getMicBuffer();
                            }
                        }
                        else
                            return;

                        if (buffer == null)
                            return;

                        if (waveMD5 != waveMD5Old)
                        {
                            waveOut.Play(buffer, 0, buffer.Length);
                        }
                        waveMD5Old = waveMD5;

                        /*if (!ByteArrayCompare(buffer, bufferOld))
                        {
                            waveOut.Play(buffer, 0, buffer.Length);
                        }*/
                        bufferOld = buffer;

                        sw.Stop();
                        double d = Math.Floor(sw.Elapsed.TotalMilliseconds);
                        int l = buffer.Length;// / 1024;
                        waveBuffer = l;
                        if (lblCamerasNr.InvokeRequired)
                        {
                            lblCamerasNr.BeginInvoke((MethodInvoker)delegate
                            {
                                lblCamerasNr.Text = string.Format("Stream info: {0} byte , {1} ms", l, d);
                                lblCamerasNr.Refresh();
                            });
                        }
                        else
                        {
                            lblCamerasNr.Text = string.Format("Stream info: {0} byte , {1} ms", l, d);
                            lblCamerasNr.Refresh();
                        }
                    }
                }
                catch
                {
                }
            }).Start();
        }

        private void cameraForm_Shown(object sender, EventArgs e)
        {
            timer1.Interval = timerInterval;
            timer2.Enabled = true;
        }

        private void cameraForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                isPlayVoice = false;
                if (!(client == null))
                {
                    client.stop_record(loginName);
                }

                if (!(clientMic == null))
                {
                    clientMic.stop_Mic();
                }
                Thread.Sleep(30);
                if (!(client == null))
                    client.Close();
                if (!(clientMic == null))
                    clientMic.Close();
                //waveOut.Dispose();
                this.Dispose();
            }
            catch (Exception ex)
            {
                if (!(client == null))
                {
                    client.Abort();
                }
                if (!(clientMic == null))
                {
                    clientMic.Abort();
                }
                //waveOut.Dispose();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {

            if (timer2.Interval < 600)
            {
                timer2.Interval += 100;
                this.Text += " .";
                return;
            }
            timer2.Enabled = false;
            this.Text = string.Format("{0} - {1}", lastLab, "Camera");
            getStreamCamera();
        }

        private void comboBoxRotate_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            ComboboxItem selectedRotation = (ComboboxItem)cb.SelectedItem;
            rotationDirection = selectedRotation.Value;
        }

        private void cameraForm_Load(object sender, EventArgs e)
        {
            ComboboxItem newitem;
            comboBoxRotate.Items.Clear();
            var listRotate = Enum.GetNames(typeof(RotateFlipType)).ToList();
            var listRotateType = (RotateFlipType[])Enum.GetValues(typeof(RotateFlipType));
            int i = 0;
            /*newitem = new ComboboxItem();
            newitem.Text = "None";
            newitem.Value = RotateFlipType.RotateNoneFlipNone;
            comboBoxRotate.Items.Add(newitem);*/
            rotationDirection = RotateFlipType.RotateNoneFlipNone;
            foreach (var item in listRotate)
            {
                newitem = new ComboboxItem();
                newitem.Text = item;
                newitem.Value = listRotateType[i];
                comboBoxRotate.Items.Add(newitem);
                i++;
            }

        }

        private void buttonCaptureImage_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "JPEG files|*.jpg";
            saveFileDialog1.Title = "Save File";
            saveFileDialog1.FileName = string.Format("{0}-{1}-{2}_{3}-{4}-{5}_{6}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, this.Text.Replace(":", "-"));
            DialogResult dialogResult = saveFileDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                pictureBox1.Image.Save(saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else if (dialogResult == DialogResult.Cancel)
            {
            }
        }

        private void buttonMic_Click(object sender, EventArgs e)
        {
            buttonDecreaseTime.Enabled = false;
            buttonIncreaseTime.Enabled = false;
            buttonMic.Enabled = false;
            toolStripMenuItemMic.Enabled = false;
            getStreamMic();
        }

        private void buttonRotate_Click(object sender, EventArgs e)
        {
            int selctedRotationIndex = comboBoxRotate.SelectedIndex;

            if (selctedRotationIndex >= (comboBoxRotate.Items.Count - 1))
                comboBoxRotate.SelectedIndex = 0;
            else
                comboBoxRotate.SelectedIndex = selctedRotationIndex + 1;

            ComboboxItem selectedRotation = (ComboboxItem)comboBoxRotate.SelectedItem;
            rotationDirection = selectedRotation.Value;

            labelRotation.Text = selectedRotation.Text;
        }

        private void toolStripMenuItemMic_Click(object sender, EventArgs e)
        {
            buttonMic_Click(null, null);
        }

        private void toolStripMenuItemCapture_Click(object sender, EventArgs e)
        {
            buttonCaptureImage_Click(null, null);
        }

    }
}

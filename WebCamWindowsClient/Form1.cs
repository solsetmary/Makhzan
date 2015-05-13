using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using AForge.Video.DirectShow;
using System.Net;
using System.Web.Script.Serialization;
using System.Threading;
using System.Runtime.InteropServices;

namespace WebCamWindowsClient
{
    public partial class Form1 : Form
    {
        private WebCamService.WebCamServiceClient client;
        private int counter;
        private int counterEx;
        public string newIP;
        public string myJSONoutput;
        public string lastPort;
        public string lastValue;
        public string lastText;
        public int timerInterval = 500;

        public Form1()
        {
            InitializeComponent();
            counter = 0;
        }
        
        class ComboboxItem
        {
            public string Text { get; set; }
            public string Value { get; set; }
            public string Port { get; set; }
            public override string ToString() { return Text; } 
        }

        class templateResponse
        {
            public String labID;
            public String camID;
            public String camIndex;
            public String status;
            public String portNr;
            public String camComments;
            public String labName;
            public String labComments;
        }

        private void fillComboBox(string[] cn, string[] cv, string[] cp, ComboBox cb)
        {
            ComboboxItem newitem;
            
            cb.Items.Clear();
            //cb.DisplayMember = "Text";
            //cb.ValueMember = "Value";
            for (int i = 0; i < cn.Length; i++)
            {
                newitem = new ComboboxItem();
                newitem.Text = cn[i];
                newitem.Value = cv[i];
                newitem.Port = cp[i];
               // comboBoxCameras.Items.Add(new { Text = cn[i], Value = cv[i] });
                cb.Items.Add(newitem);
            }
            
        }

        void RetrieveIP()
        {
            byte[] imageData;
            using (var clientIP = new WebClient())
            {
                imageData = clientIP.DownloadData("http://www.soheyln.com/myIP/index.php");
            }
            var str = System.Text.Encoding.Default.GetString(imageData);
            int posStart = str.IndexOf("PublicIP\":\"") + 11;
            int posEnd = str.IndexOf("\"}]");
            newIP = str.Substring(posStart, posEnd - posStart);
        }

        void RetrieveActiveCameras()
        {
            string myQuery = "select rs.*, rc.Comments as camComments, rl.Name as labName, rl.Comments as labComments from prorobot_Status rs " + 
                                    "join prorobot_Camera rc on (rs.camID=rc.fixID and rs.labID=rc.labID) " +
                                    "join prorobot_lab rl on (rs.labID=rl.fixID) " +
                                    "where status='aLive'";
            myJSONoutput = new WebClient().DownloadString("http://soheyln.com/myIP/status.php?myquery=" + myQuery);
            var result = new JavaScriptSerializer().Deserialize<List<templateResponse>>(myJSONoutput);
            List<string> labsId = new List<string>();
            List<string> labsName = new List<string>();
            if (result == null)
            {
                if (buttonConnect.InvokeRequired)
                {
                    buttonConnect.BeginInvoke((MethodInvoker)delegate
                    {
                        buttonConnect.Text = "Connect";
                    });
                    MessageBox.Show("No Live Camera!!!");
                }
                else
                {
                    buttonConnect.Text = "Connect";
                    MessageBox.Show("No Live Camera!!!");
                }
                
                return;
            }
            for (int i = 0; i < result.Count; i++)
            {
                if (!labsId.Contains(result[i].labID))
                {
                    labsId.Add(result[i].labID);
                    labsName.Add(result[i].labName + " : " + result[i].labComments);
                }
            }
            if (comboBoxLabs.InvokeRequired)
            {
                comboBoxLabs.BeginInvoke((MethodInvoker)delegate
                {
                    fillComboBox(labsName.ToArray(), labsId.ToArray(), labsId.ToArray(), comboBoxLabs);
                });
            }
            else
            {
                fillComboBox(labsName.ToArray(), labsId.ToArray(), labsId.ToArray(), comboBoxLabs);
            }
        }

        void getStreamCamera(string cPort, string cValue, string cText)
        {
            lastPort = cPort;
            lastValue = cValue;
            lastText = cText;
            lblCamerasNr.Text = "Streamming . . .";
            lblCamerasNr.Refresh();
            try
            {
                string myRemoteIP = "http://" + newIP + ":" + cPort + "/RobotWebCamServer";
                client = new WebCamService.WebCamServiceClient("NetTcpBinding_IWebCamService", myRemoteIP);
                client.Record(cValue, cText);
            }
            catch (System.ServiceModel.CommunicationException ex)
            { MessageBox.Show(ex.Message); }

            System.Threading.Thread.Sleep(3000);

            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Start();
        }

        void getStarted()
        {
            counter++;
            System.Threading.Thread.Sleep(100);
            try
            {
                Stream imageStream = client.getWebCamImage();
                Image myImg = Bitmap.FromStream(imageStream);
                //pictureBox1.Image = myImg;
                if (pictureBox1.InvokeRequired)
                {
                    lblCamerasNr.Text = counter + ", InvokeRequired!!!";
                }
                else
                {
                    pictureBox1.Image = myImg;
                }
                System.Diagnostics.Debug.WriteLine(counter);
            }
            catch (System.ServiceModel.CommunicationException ex)
            {

                if (ex.InnerException is System.ServiceModel.QuotaExceededException)
                {
                    lblCamerasNr.Text = counter + ", Exceeded on Service Communication!!!";
                    lblCamerasNr.Refresh();
                    //timer1.Stop();
                    //getStreamCamera(lastPort, lastValue, lastText);
                }
                else
                {
                    //lblCamerasNr.Text = counter + ", Error on Service Communication!!!";
                    lblCamerasNr.Text = counter + ", " + ex.Message;
                    lblCamerasNr.Refresh();
                    //timer1.Stop();
                    //getStreamCamera(lastPort, lastValue, lastText);
                }

            }
            catch (System.Exception ex)
            {
                timer1.Stop();
                lblCamerasNr.Text = counter + ", Error on connection. Try again!!!";
                lblCamerasNr.Refresh();
            }

            getStarted();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = timerInterval;
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            counter++; 
            lock (client)
            {
                try
                {
                    Stream imageStream = client.getWebCamImage();
                    //pictureBox1.Image = Bitmap.FromStream(imageStream);
                    lock (imageStream)
                    {
                        lock (pictureBox1)
                        {
                            if (pictureBox1.InvokeRequired)
                            {
                                pictureBox1.BeginInvoke((MethodInvoker)delegate
                                {
                                    pictureBox1.Image = Bitmap.FromStream(imageStream);
                                });
                            }
                            else
                            {
                                pictureBox1.Image = Bitmap.FromStream(imageStream);
                            }
                            //System.Diagnostics.Debug.WriteLine(counter);
                        }
                    }
                }
                catch (System.ServiceModel.CommunicationException ex)
                {
                    counterEx++;
                    if (ex.InnerException is System.ServiceModel.QuotaExceededException)
                    {
                        lblCamerasNr.Text = counter + ", " + counterEx + ", Exceeded on Service Communication!!!";
                        lblCamerasNr.Refresh();
                        //timer1.Stop();
                        //getStreamCamera(lastPort, lastValue, lastText);
                    }
                    else
                    {
                        //lblCamerasNr.Text = counter + ", Error on Service Communication!!!";
                        lblCamerasNr.Text = counter + ", " + counterEx + ", Streamming . . .";// +ex.Message;
                        lblCamerasNr.Refresh();
                        //timer1.Stop();
                        //getStreamCamera(lastPort, lastValue, lastText);
                    }

                }
                catch (System.Exception ex)
                {
                    counterEx++;
                    timer1.Stop();
                    client.stop_record();
                    lblCamerasNr.Text = counter + ", " + counterEx + ", Error on connection. Try again!!!";
                    lblCamerasNr.Refresh();
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!(client == null))
            {
                client.stop_record();
            } 
            client = null;
            Application.Exit();
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            buttonConnect.Text = "Refresh";
            comboBoxCameras.Items.Clear();
            comboBoxLabs.Items.Clear();
            comboBoxLabs.Text = "";
            comboBoxCameras.Text = "";

            System.Threading.ThreadPool.QueueUserWorkItem(delegate
            {
                // Retrieve server IP address
                RetrieveIP();

                // Retrieve active cameras
                RetrieveActiveCameras();
            }, null);

        }

        private void textBoxPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void comboBoxCameras_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            ComboboxItem selectedCamera = (ComboboxItem)cb.SelectedItem;
            counter = 0;
            counterEx = 0;
            // Start getting stream of camera
            getStreamCamera(selectedCamera.Port, selectedCamera.Value, selectedCamera.Text);

        }

        private void comboBoxLabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;
            ComboboxItem selectedLabs = (ComboboxItem)cb.SelectedItem;
            var result = new JavaScriptSerializer().Deserialize<List<templateResponse>>(myJSONoutput);
            List<string> camIndex = new List<string>();
            List<string> camName = new List<string>();
            List<string> camPort = new List<string>();

            for (int i = 0; i < result.Count; i++)
            {
                if (selectedLabs.Value==result[i].labID)
                {
                    camIndex.Add(result[i].camIndex);
                    camName.Add(result[i].camComments);
                    camPort.Add(result[i].portNr);
                }
            }
            fillComboBox(camName.ToArray(), camIndex.ToArray(), camPort.ToArray() , comboBoxCameras);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ((PictureBox)sender).BorderStyle = BorderStyle.Fixed3D;
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox3.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ((PictureBox)sender).BorderStyle = BorderStyle.Fixed3D;
            pictureBox2.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ((PictureBox)sender).BorderStyle = BorderStyle.Fixed3D;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox3.BorderStyle = BorderStyle.FixedSingle;
        }

        private void buttonIncreaseTime_Click(object sender, EventArgs e)
        {
            if (timerInterval < 2000)
                timerInterval += 100;

            timer1.Interval = timerInterval;
        }

        private void buttonDecreaseTime_Click(object sender, EventArgs e)
        {
            if (timerInterval > 100)
                timerInterval -= 100;

            timer1.Interval = timerInterval;
        }

    }
}


using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Media;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WebCamWindowsClient
{
    public partial class formControlpanel : Form
    {

        #region Variables

        //private WebCamService.WebCamServiceClient client;
        public formWelcome fw;
        public formCalendar fc;
        public formArduino fa;
        public formChat fch;
        public formDevice fd;

        public string loginName;
        public string newIP;
        public string myJSONoutput;
        public string lastCamPort;
        public string lastCamValue;
        public string lastCamText;
        public string lastLabTitle;
        public string lastLabID;
        public string lastDevID;
        public string lastLabIP;
        public string lastDevType;
        public string micPort;
        public string micIndex;
        public string micText;
        public string micLab;

        List<string> labImages = new List<string>();
        List<string> devImages = new List<string>();
        List<imageResponse> allImages;

        //Shared 256 bit Key and IV here
        const string sKy = "lkirwf897+22#bbtrm8814z5qq=498j5"; //32 chr shared ascii string (32 * 8 = 256 bit)
        const string sIV = "741952hheeyy66#cs!9hjv887mxx7@8y"; //32 chr shared ascii string (32 * 8 = 256 bit)

        #endregion


        public formControlpanel()
        {
            InitializeComponent();

        }
        
        #region Classes

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
            public String devID;
            public String devIndex;
            public String devType;
            public String status;
            public String portNr;
            public String camComments;
            public String labName;
            public String labComments;
            public String hosted;
            public String labip;
            public String labNote;
            public String mediaNote;
        }

        class imageResponse
        {
            public String labID;
            public String devID;
            public String labImage;
            public String devImage;
        }

        #endregion

        #region Methods

        public string DecryptRJ256(string prm_key, string prm_iv, string prm_text_to_decrypt)
        {

            var sEncryptedString = prm_text_to_decrypt;

            var myRijndael = new RijndaelManaged()
            {
                Padding = PaddingMode.Zeros,
                Mode = CipherMode.CBC,
                KeySize = 256,
                BlockSize = 256
            };

            var key = Encoding.ASCII.GetBytes(prm_key);
            var IV = Encoding.ASCII.GetBytes(prm_iv);

            var decryptor = myRijndael.CreateDecryptor(key, IV);
            try
            {
                var sEncrypted = Convert.FromBase64String(sEncryptedString);

                var fromEncrypt = new byte[sEncrypted.Length];

                var msDecrypt = new MemoryStream(sEncrypted);
                var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);

                csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);
                return (Encoding.ASCII.GetString(fromEncrypt).Replace("\0", string.Empty).Trim());
            }
            catch
            {
                return "false";
            }
        }

        public string EncryptRJ256(string prm_key, string prm_iv, string prm_text_to_encrypt)
        {

            var sToEncrypt = prm_text_to_encrypt;

            var myRijndael = new RijndaelManaged()
            {
                Padding = PaddingMode.Zeros,
                Mode = CipherMode.CBC,
                KeySize = 256,
                BlockSize = 256
            };

            var key = Encoding.ASCII.GetBytes(prm_key);
            var IV = Encoding.ASCII.GetBytes(prm_iv);

            var encryptor = myRijndael.CreateEncryptor(key, IV);

            var msEncrypt = new MemoryStream();
            var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

            var toEncrypt = Encoding.ASCII.GetBytes(sToEncrypt);

            csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
            csEncrypt.FlushFinalBlock();

            var encrypted = msEncrypt.ToArray();

            return (Convert.ToBase64String(encrypted));
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
                Random rnd = new Random();
                int img = rnd.Next(0, imageList1.Images.Count - 1);
                return imageList1.Images[img];
            }
        }

        public void setParameters(string login)
        {
            loginName = login;
        }

        private void fillComboBox(string[] cn, string[] cv, string[] cp, ComboBox cb)
        {
            ComboboxItem newitem;
            cb.Items.Clear();
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

        private void fillListBoxLabs(string[] lbid, string[] lbn, string[] lbc, string[] lbv, string[] did, string[] lbp, string[] lbip, string[] dt, string[] lbORdevi, exListBox lb)
        {
            lb.Items.Clear();
            ListPanelItem lpi;

            //Random rnd = new Random();

            for (int i = 0; i < lbn.Length; i++)
            {
                //int img = rnd.Next(0, imageList1.Images.Count-1);
                lb.Items.Add(new exListBoxItem(Convert.ToInt32(lbid[i]), lbn[i], lbc[i], lbv[i], did[i], lbp[i], lbip[i], dt[i], Base64ToImage(lbORdevi[i]))); //imageList1.Images[img]));
                lpi = new ListPanelItem();
                lpi.Caption = lbn[i];
                lpi.Content = lbc[i];
                lpi.Image = Base64ToImage(lbORdevi[i]); //imageList1.Images[img];
                listPanelLabs.AddItem(lpi);
            }
        }

        private void RetrieveIP()
        {
            /*byte[] imageData;
            using (var clientIP = new WebClient())
            {
                imageData = clientIP.DownloadData("http://labpp.org/check/internet/connection/");
            }
            var str = System.Text.Encoding.Default.GetString(imageData);*/
            string str = new WebClient().DownloadString("http://labpp.org/check/internet/connection/");
            int posStart = str.IndexOf("PublicIP\":\"") + 11;
            int posEnd = str.IndexOf("\"}]");
            newIP = str.Substring(posStart, posEnd - posStart);
        }

        private void RetrieveActiveCameras()
        {
            /*string myQuery = "select rs.*, rm.Comments as camComments, rl.Name as labName, rl.Comments as labComments, rl.hostBy as hosted, rl.labIP as labip, rl.note as labNote, rm.note as mediaNote from prorobot_status rs " +
                                    "join prorobot_media rm on (rs.devID=rm.devID and rs.labID=rm.labID  and rs.devType=rm.devType) " +
                                    "join prorobot_lab rl on (rs.labID=rl.labID) " +
                                    "where (status='alive')";// and rm.devType='camera'
            myJSONoutput = new WebClient().DownloadString("http://labpp.org/myIP/status.php?myquery=" + myQuery);*/
            myJSONoutput = new WebClient().DownloadString("http://labpp.org/query/do/1");
            myJSONoutput = DecryptRJ256(sKy, sIV, myJSONoutput);
            if (myJSONoutput == "false")
                return;
            var result = new JavaScriptSerializer().Deserialize<List<templateResponse>>(myJSONoutput);

            /*string imagesQuery = "select rl.labID, rm.devID, rl.Image as labImage, rm.Image as devImage from prorobot_status rs " +
                                    "join prorobot_media rm on (rs.devID=rm.devID and rs.labID=rm.labID  and rs.devType=rm.devType)" +
                                    "join prorobot_lab rl on (rs.labID=rl.labID) " +
                                    "where (status='alive')";
            string resultByte = new WebClient().DownloadString("http://labpp.org/myIP/getimages.php?myquery=" + imagesQuery);*/
            string resultImages = new WebClient().DownloadString("http://labpp.org/query/get/1");
            resultImages = DecryptRJ256(sKy, sIV, resultImages);
            if (resultImages == "false")
                return;
            allImages = new JavaScriptSerializer().Deserialize<List<imageResponse>>(resultImages);
            //string[] images = resultByte.Split(",".ToCharArray());

            List<string> labsId = new List<string>();
            List<string> devsId = new List<string>();
            List<string> labsNameComments = new List<string>();
            List<string> labsName = new List<string>();
            List<string> labsComments = new List<string>();
            List<string> labsValue = new List<string>();
            List<string> labsPort = new List<string>();
            List<string> labsIP = new List<string>();
            List<string> devType = new List<string>();

            if (result == null)
            {
                if (buttonConnect.InvokeRequired)
                {
                    buttonConnect.BeginInvoke((MethodInvoker)delegate
                    {
                        //buttonConnect.Text = "Connect";
                        buttonConnect.Enabled = true;
                        SystemSounds.Beep.Play();
                    });
                }
                else
                {
                    //buttonConnect.Text = "Connect";
                    buttonConnect.Enabled = true;
                    SystemSounds.Beep.Play();
                }

                if (this.InvokeRequired)
                {
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        MessageBox.Show(this, "No Available Lab!!!");
                    });
                }
                else
                {
                    MessageBox.Show(this, "No Available Lab!!!");
                }


                if (statusStrip1.InvokeRequired)
                {
                    statusStrip1.BeginInvoke((MethodInvoker)delegate
                    {
                        toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                    });
                }
                else
                {
                    toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                }
                if (tableLayoutPanel1.InvokeRequired)
                {
                    tableLayoutPanel1.BeginInvoke((MethodInvoker)delegate
                    {
                        tableLayoutPanel1.Visible = false;
                    });
                }
                else
                {
                    tableLayoutPanel1.Visible = false;
                }
                return;
            }
            for (int i = 0; i < result.Count; i++)
            {
                if (!labsId.Contains(result[i].labID))
                {
                    labImages.Add((allImages.Find(x => x.labID == result[i].labID)).labImage);
                    labsId.Add(result[i].labID);
                    devsId.Add(result[i].devID);
                    labsNameComments.Add(string.Format("{0} : {1}", result[i].labName, result[i].labComments));
                    labsName.Add(result[i].labName);
                    labsComments.Add(string.Format("{0}\r\nHost by: {1}, {2}", result[i].labComments, result[i].hosted, result[i].labNote));
                    labsValue.Add(result[i].labID);
                    labsPort.Add(result[i].portNr);
                    labsIP.Add(result[i].labip);
                    devType.Add("lab");
                }
            }
            /*for (int i = 0; i < allImages.Count; i++)
            {
                //labImages.Add(images[i * 2].Replace("[", "").Replace("\"", ""));
                //devImages.Add(images[(i * 2) + 1].Replace("[", "").Replace("\"", "")); 
                labImages.Add(allImages[i].labImage);
                devImages.Add(allImages[i].devImage);
            }*/
            if (comboBoxLabs.InvokeRequired)
            {
                comboBoxLabs.BeginInvoke((MethodInvoker)delegate
                {
                    fillComboBox(labsNameComments.ToArray(), labsId.ToArray(), labsId.ToArray(), comboBoxLabs);
                });
            }
            else
            {
                fillComboBox(labsNameComments.ToArray(), labsId.ToArray(), labsId.ToArray(), comboBoxLabs);
            }
            if (exListBoxLabs.InvokeRequired)
            {
                exListBoxLabs.BeginInvoke((MethodInvoker)delegate
                {
                    fillListBoxLabs(labsId.ToArray(), labsName.ToArray(), labsComments.ToArray(), labsValue.ToArray(), devsId.ToArray(), labsPort.ToArray(), labsIP.ToArray(), devType.ToArray(), labImages.ToArray(), exListBoxLabs);
                });
            }
            else
            {
                fillListBoxLabs(labsId.ToArray(), labsName.ToArray(), labsComments.ToArray(), labsValue.ToArray(), devsId.ToArray(), labsPort.ToArray(), labsIP.ToArray(), devType.ToArray(), labImages.ToArray(), exListBoxLabs);
            }
            if (buttonConnect.InvokeRequired)
            {
                buttonConnect.BeginInvoke((MethodInvoker)delegate
                {
                    //buttonConnect.Text = "Connect";
                    buttonConnect.Enabled = true;
                    SystemSounds.Beep.Play();
                });
            }
            else
            {
                //buttonConnect.Text = "Connect";
                buttonConnect.Enabled = true;
                SystemSounds.Beep.Play();
            }
            if (statusStrip1.InvokeRequired)
            {
                statusStrip1.BeginInvoke((MethodInvoker)delegate
                {
                    toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
                });
            }
            else
            {
                toolStripProgressBar1.Style = ProgressBarStyle.Blocks;
            }

        }

        public void RunFormDevice()
        {
            /*Thread Devicethread = new Thread(new ThreadStart(DeviceManager.ShowFormDevice));
            Devicethread.IsBackground = true;
            Devicethread.Start();
            DeviceManager.setParameters(lastLabIP, lastCamPort, lastCamValue, lastCamText, lastLabTitle, lastLabID, micIndex, micText, micPort, micLab);


            return;
            */
            formDevice fd = new formDevice();
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    fd.MdiParent = this;
                    fd.setParameters(loginName, lastLabIP, lastCamPort, lastCamValue, lastCamText, lastLabTitle, lastLabID, lastDevID, micIndex, micText, micPort, micLab);
                    //Application.Run(fc);
                    fd.Show();
                });
            }
            else
            {
                fd.MdiParent = this;
                fd.setParameters(loginName, lastLabIP, lastCamPort, lastCamValue, lastCamText, lastLabTitle, lastLabID, lastDevID, micIndex, micText, micPort, micLab);
                //Application.Run(fc);
                fd.Show();
            }
        }

        public void RunFormChat()
        {
            formChat fc = new formChat();
            //set parent form for the child window
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    fc.MdiParent = this;
                    fc.setParameters(lastLabIP, lastCamPort, loginName, lastCamText, lastLabTitle, lastLabID);
                    //Application.Run(fc);
                    fc.Show();
                });
            }
            else
            {
                fc.MdiParent = this;
                fc.setParameters(lastLabIP, lastCamPort, loginName, lastCamText, lastLabTitle, lastLabID);
                //Application.Run(fc);
                fc.Show();
            }
        }

        public void RunFormWelcome()
        {
            if (fw != null)
            {
                fw.Show();
                fw.WindowState = FormWindowState.Normal;
                return;
            }

            fw = new formWelcome();
            fw.FormClosed += fw_FormClosed;
            //set parent form for the child window
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    fw.MdiParent = this;
                    fw.Show();
                });
            }
            else
            {
                fw.MdiParent = this;
                fw.Show();
            }
        }

        public void RunFormCalendar()
        {
            if (fc != null)
            {
                fc.Show();
                fc.WindowState = FormWindowState.Normal;
                return;
            }

            fc = new formCalendar();
            fc.FormClosed += fc_FormClosed;
            fc.setParameters("http://labpp.org/calendar");
            //set parent form for the child window
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    fc.MdiParent = this;
                    fc.Show();
                });
            }
            else
            {
                fc.MdiParent = this;
                fc.Show();
            }
        }

        public void RunFormArduino()
        {
            if (fa != null)
            {
                if (this.InvokeRequired)
                {
                    this.BeginInvoke((MethodInvoker)delegate
                    {
                        fa.Show();
                        fa.WindowState = FormWindowState.Normal;
                    });
                }
                else
                {
                    if (this.InvokeRequired)
                    {
                        this.BeginInvoke((MethodInvoker)delegate
                        {
                            fa.MdiParent = this;
                            fa.Show();
                        });
                    }
                    else
                    {
                        fa.MdiParent = this;
                        try
                        {
                            fa.Show();
                        }
                        catch
                        {
                            fa.Close();
                            fa = null;
                            RunFormArduino();
                        }
                    }
                }

                return;
            }

            fa = new formArduino();
            fa.FormClosed += fa_FormClosed;
            //set parent form for the child window
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    if (fa.InvokeRequired)
                    {
                        fa.BeginInvoke((MethodInvoker)delegate
                        {
                            fa.MdiParent = this;
                            fa.Show();
                        });
                    }
                    else
                    {
                        fa.MdiParent = this;
                        fa.Show();
                    }
                });
            }
            else
            {
                fa.MdiParent = this;
                fa.Show();
            }
            fa.setParameters(lastLabIP, lastCamPort, lastCamValue, loginName, lastCamText, lastLabTitle, lastLabID, lastDevID);
        }

        #endregion
        
        #region Events
        
        void fw_FormClosed(object sender, FormClosedEventArgs e)
        {
            fw.Dispose();
            fw = null;
        }

        void fc_FormClosed(object sender, FormClosedEventArgs e)
        {
            fc.Dispose();
            fc = null;
        }

        void fa_FormClosed(object sender, FormClosedEventArgs e)
        {
            fa.Dispose();
            fa = null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //listPanelLabs.ItemClick +=listPanelLabs_ItemClick;
            toolStripStatusLabelLogin.Text = "Login as " + loginName;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
            {
                menuItemCloseAll_Click(null, null);
            }
            else
            {
                e.Cancel = true;
                notifyIcon.ShowBalloonTip(5000);
                Hide();
            }
        }

        public void buttonConnect_Click(object sender, EventArgs e)
        {
            toolStripProgressBar1.Style = ProgressBarStyle.Marquee;
            //buttonConnect.Text = "Refresh";
            tableLayoutPanel1.Visible = true;
            comboBoxCameras.Items.Clear();
            comboBoxLabs.Items.Clear();
            exListBoxLabs.Items.Clear();
            exListBoxCameras.Items.Clear();
            comboBoxLabs.Text = "";
            comboBoxCameras.Text = "";
            micIndex = "";
            micText = "";
            micPort = "";
            labImages.Clear();
            devImages.Clear();
            buttonConnect.Enabled = false;
            ThreadPool.QueueUserWorkItem(delegate
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
            /*ComboBox cb = (ComboBox)sender;
            ComboboxItem selectedCamera = (ComboboxItem)cb.SelectedItem;
            lastCamPort = selectedCamera.Port;
            lastCamValue = selectedCamera.Value;
            lastCamText = selectedCamera.Text;
            lastLabTitle = comboBoxLabs.Text;
            buttonAddCamera.Enabled = true;*/
        }

        private void comboBoxLabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*ComboBox cb = (ComboBox)sender;
            ComboboxItem selectedLabs = (ComboboxItem)cb.SelectedItem;
            comboBoxCameras.Text = "";
            var result = new JavaScriptSerializer().Deserialize<List<templateResponse>>(myJSONoutput);
            List<string> devIndex = new List<string>();
            List<string> devName = new List<string>();
            List<string> devPort = new List<string>();
            if (result == null)
                return;
            for (int i = 0; i < result.Count; i++)
            {
                if (selectedLabs.Value == result[i].labID)
                {
                    if (result[i].devType == "camera")
                    {
                        devIndex.Add(result[i].devIndex);
                        devName.Add(result[i].camComments);
                        devPort.Add(result[i].portNr);
                    }
                    else if (result[i].devType == "mic")
                    {
                        micLab = result[i].labID;
                        micIndex = result[i].devIndex;
                        micText = result[i].camComments;
                        micPort = result[i].portNr;
                    }
                }
            }
            fillComboBox(devName.ToArray(), devIndex.ToArray(), devPort.ToArray(), comboBoxCameras);*/
        }

        private void buttonAddCamera_Click(object sender, EventArgs e)
        {
            Thread cameraFormThread = new Thread(RunFormDevice);
            cameraFormThread.SetApartmentState(ApartmentState.STA);
            cameraFormThread.Name = string.Format("Lab: {0}, Camera: {1}", lastLabTitle, lastCamText);   // looks nice in Output window
            cameraFormThread.IsBackground = true;
            cameraFormThread.Start();
            buttonAddCamera.Enabled = false;
        }

        private void timerDateTime_Tick(object sender, EventArgs e)
        {
            timerDateTime.Interval = 1000;
            DateTime now = DateTime.Now;
            toolStripStatusLabelDate.Text = now.Date.ToLongDateString();
            toolStripStatusLabelTime.Text = now.ToShortTimeString();
        }

        private void exListBoxLabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            exListBoxItem selectedLabs = (exListBoxItem)exListBoxLabs.SelectedItem;
            lastLabTitle = selectedLabs.Title;
            var result = new JavaScriptSerializer().Deserialize<List<templateResponse>>(myJSONoutput);

            List<string> labsIP = new List<string>();
            List<string> devIndex = new List<string>();
            List<string> devName = new List<string>();
            List<string> devComments = new List<string>();
            List<string> devPort = new List<string>();
            List<string> devLabID = new List<string>();
            List<string> devID = new List<string>();
            List<string> devType = new List<string>();

            devImages.Clear();

            if (result == null)
                return;

            for (int i = 0; i < result.Count; i++)
            {
                if (selectedLabs.Id.ToString() == result[i].labID)
                {
                    devImages.Add((allImages.Find(x => (x.labID == result[i].labID & x.devID == result[i].devID))).devImage);

                    if (result[i].devType == "camera")
                    {
                        labsIP.Add(result[i].labip);
                        devIndex.Add(result[i].devIndex);
                        devName.Add(result[i].camComments);
                        devComments.Add(string.Format("{0}: {1}", result[i].hosted, result[i].mediaNote));
                        devPort.Add(result[i].portNr);
                        devLabID.Add(result[i].labID);
                        devID.Add(result[i].devID);
                        devType.Add(result[i].devType);
                    }
                    else if (result[i].devType == "webcam")
                    {
                        labsIP.Add(result[i].labip);
                        devIndex.Add(result[i].devIndex);
                        devName.Add(result[i].camComments);
                        devComments.Add(string.Format("{0}: {1}", result[i].hosted, result[i].mediaNote));
                        devPort.Add(result[i].portNr);
                        devLabID.Add(result[i].labID);
                        devID.Add(result[i].devID);
                        devType.Add(result[i].devType);
                    }
                    else if (result[i].devType == "chat")
                    {
                        labsIP.Add(result[i].labip);
                        devIndex.Add(result[i].devIndex);
                        devName.Add(result[i].camComments);
                        devComments.Add(string.Format("{0}: {1}", result[i].hosted, result[i].mediaNote));
                        devPort.Add(result[i].portNr);
                        devLabID.Add(result[i].labID);
                        devID.Add(result[i].devID);
                        devType.Add(result[i].devType);
                    }
                    else if (result[i].devType == "arduino")
                    {
                        labsIP.Add(result[i].labip);
                        devIndex.Add(result[i].devIndex);
                        devName.Add(result[i].camComments);
                        devComments.Add(string.Format("{0}: {1}", result[i].hosted, result[i].mediaNote));
                        devPort.Add(result[i].portNr);
                        devLabID.Add(result[i].labID);
                        devID.Add(result[i].devID);
                        devType.Add(result[i].devType);
                    }
                    else if (result[i].devType == "mic")
                    {
                        labsIP.Add(result[i].labip);
                        micIndex = result[i].devIndex;
                        micText = result[i].camComments;
                        micPort = result[i].portNr;
                        micLab = result[i].labID;
                        devID.Add(result[i].devID);
                    }
                }
            }
            fillListBoxLabs(devIndex.ToArray(), devName.ToArray(), devComments.ToArray(), devLabID.ToArray(), devID.ToArray(), devPort.ToArray(), labsIP.ToArray(), devType.ToArray(), devImages.ToArray(), exListBoxCameras);
        }

        private void exListBoxCameras_SelectedIndexChanged(object sender, EventArgs e)
        {
            exListBox cb = (exListBox)sender;
            exListBoxItem selectedCamera = (exListBoxItem)cb.SelectedItem;
            lastCamPort = selectedCamera.Ports;
            lastCamValue = selectedCamera.Id.ToString();
            lastCamText = selectedCamera.Details;
            lastLabID = selectedCamera.Values;
            lastLabIP = selectedCamera.labIP;
            lastDevID = selectedCamera.devID.ToString();
            lastDevType = selectedCamera.devType;
            buttonAddCamera.Enabled = true;

            if (lastDevType == "camera")
            {
                /*Thread cameraFormThread = new Thread(RunFormDevice);
                cameraFormThread.SetApartmentState(ApartmentState.STA);
                cameraFormThread.Name = string.Format("Lab: {0}, Camera: {1}", lastLabTitle, lastCamText);   // looks nice in Output window
                cameraFormThread.IsBackground = true;
                cameraFormThread.Start();
                buttonAddCamera.Enabled = false;*/
                RunFormDevice();
            }
            else if (lastDevType == "webcam")
            {
                /*Thread cameraFormThread = new Thread(RunFormDevice);
                cameraFormThread.SetApartmentState(ApartmentState.STA);
                cameraFormThread.Name = string.Format("Lab: {0}, Camera: {1}", lastLabTitle, lastCamText);   // looks nice in Output window
                cameraFormThread.IsBackground = true;
                cameraFormThread.Start();
                buttonAddCamera.Enabled = false;*/
                RunFormDevice();
            }
            else if (lastDevType == "chat")
            {
                /*Thread cameraFormThread = new Thread(RunFormChat);
                cameraFormThread.SetApartmentState(ApartmentState.STA);
                cameraFormThread.Name = string.Format("Lab: {0}, Chat: {1}", lastLabTitle, lastCamText);   // looks nice in Output window
                cameraFormThread.IsBackground = true;
                cameraFormThread.Start();
                buttonAddCamera.Enabled = false;*/
                RunFormChat();
            }
            else if (lastDevType == "arduino")
            {
                /*Thread cameraFormThread = new Thread(RunFormArduino);
                cameraFormThread.SetApartmentState(ApartmentState.STA);
                cameraFormThread.Name = string.Format("Lab: {0}, Arduino: {1}", lastLabTitle, lastCamText);   // looks nice in Output window
                cameraFormThread.IsBackground = true;
                cameraFormThread.Start();
                buttonAddCamera.Enabled = false;*/
                RunFormArduino();
            }
        }

        private void toolStripMenuItemConnect_Click(object sender, EventArgs e)
        {
            buttonConnect_Click(null, null);
        }

        private void toolStripMenuItemShow_Click(object sender, EventArgs e)
        {
            Show();
        }

        private void formControlpanel_Shown(object sender, EventArgs e)
        {
            RunFormWelcome();
        }

        private void toolStripStatusLabelDate_ButtonClick(object sender, EventArgs e)
        {
            RunFormCalendar();
        }

        private void ExitAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuItemCloseAll_Click(null, null);
            this.Dispose();
            Application.Exit();
        }

        private void menuItemWelcomePanel_Click(object sender, EventArgs e)
        {
            RunFormWelcome();
        }

        private void menuItemShowLists_Click(object sender, EventArgs e)
        {
            //tableLayoutPanel1.Visible = true ? false : true;
            tableLayoutPanel1.Visible = !tableLayoutPanel1.Visible;
            Application.DoEvents();
        }

        private void menuItemArrangeIcons_Click(object sender, EventArgs e)
        {
            //Arrange MDI child icons within the client region of the MDI parent form.
            this.LayoutMdi(System.Windows.Forms.MdiLayout.ArrangeIcons);
        }

        private void menuItemCascade_Click(object sender, EventArgs e)
        {
            //Cascade all child forms.
            this.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
        }

        private void menuItemArrangeHorizontal_Click(object sender, EventArgs e)
        {
            //Tile all child forms horizontally.
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);
        }

        private void menuItemArrangeVertical_Click(object sender, EventArgs e)
        {
            //Tile all child forms horizontally.
            this.LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);
        }

        private void menuItemMaximizeAll_Click(object sender, EventArgs e)
        {
            //Gets forms that represent the MDI child forms 
            //that are parented to this form in an array
            Form[] charr = this.MdiChildren;

            //for each child form set the window state to Maximized
            foreach (Form chform in charr)
                chform.WindowState = FormWindowState.Maximized;
        }

        private void menuItemMinimizeAll_Click(object sender, EventArgs e)
        {
            //Gets forms that represent the MDI child forms 
            //that are parented to this form in an array
            Form[] charr = this.MdiChildren;

            //for each child form set the window state to Minimized
            foreach (Form chform in charr)
                chform.WindowState = FormWindowState.Minimized;
        }

        private void menuItemConnectToServer_Click(object sender, EventArgs e)
        {
            buttonConnect_Click(null, null);
        }

        private void menuItemClose_Click(object sender, EventArgs e)
        {
            //Gets the currently active MDI child window.
            Form a = this.ActiveMdiChild;
            //Close the MDI child window
            if (a == null)
                return;
            a.Close();
        }

        private void menuItemAbout_Click(object sender, EventArgs e)
        {
            /*var t1 = Task.Factory.StartNew(() => startAbout());
            t1.Wait();*/
            ThreadPool.QueueUserWorkItem(delegate
            {
                Thread splashthread = new Thread(new ThreadStart(SplashScreen.ShowSplashScreen));
                splashthread.IsBackground = true;
                splashthread.Start();
                SplashScreen.UdpateStatusText("...");
                Thread.Sleep(10000);
                SplashScreen.CloseSplashScreen();
            }, null);//
        }

        private object startAbout()
        {
            Thread splashthread = new Thread(new ThreadStart(SplashScreen.ShowSplashScreen));
            splashthread.IsBackground = true;
            splashthread.Start();
            SplashScreen.UdpateStatusText("...");
            Thread.Sleep(10000);
            SplashScreen.CloseSplashScreen();
            return "done";
        }

        private void menuItemHelpOnWebsite_Click(object sender, EventArgs e)
        {

        }

        private void menuItemCheckForUpdate_Click(object sender, EventArgs e)
        {

        }

        private void menuItemContactUs_Click(object sender, EventArgs e)
        {

        }

        private void menuItemSupportLiveChat_Click(object sender, EventArgs e)
        {

        }

        private void menuItemCloseAll_Click(object sender, EventArgs e)
        {
            //Gets forms that represent the MDI child forms 
            //that are parented to this form in an array
            Form[] charr = this.MdiChildren;

            //for each child form set the window state to Minimized
            foreach (Form chform in charr)
                chform.Close();
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            menuItemCloseAll_Click(null, null);
            this.Dispose();
            Application.Exit();
        }

        private void menuItemOptions_Click(object sender, EventArgs e)
        {

        }

        private void menuItemTutorials_Click(object sender, EventArgs e)
        {

        }

        #endregion
    }
}


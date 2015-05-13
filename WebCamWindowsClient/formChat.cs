using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace WebCamWindowsClient
{
    public partial class formChat : Form
    {
        private WebCamService.WebCamServiceClient client;
        //private speechBalloon MyBalloon;
        DateTime lastDateTime = DateTime.Now;
        public string newIP;
        public string lastPort;
        public string loginName;
        public string lastText;
        public string lastLab;
        public string lastLabID;
        public string recentMessage;
        public string chatTextBufferToCopy;
        List<bufferUserList> chatUserBuffer;

        public formChat()
        {
            InitializeComponent();
        }

        /*class chatTextList
        {
            public string Text { get; set; }
            public string LoginName { get; set; }
            public DateTime DateIn { get; set; }
        }

        class chatUserList
        {
            public string LoginName { get; set; }
            public DateTime DateIn { get; set; }
        }*/

        public void setParameters(string theIP, string cPort, string login, string cText, string lText, string lLabID)
        {
            newIP = theIP;
            lastPort = cPort;
            loginName = login;
            lastText = cText;
            lastLab = lText;
            lastLabID = lLabID;
            this.Text = string.Format("{0} - {1}", lText, cText);
            chatUserBuffer = new List<bufferUserList>();
        }

        private void fillListBoxUsers(List<bufferUserList> uList, exListBox lb)
        {
            lb.Items.Clear();

            Random rnd = new Random();
            try
            {
                for (int i = 0; i < uList.Count; i++)
                {
                    int img = rnd.Next(0, imageList1.Images.Count);
                    lb.Items.Add(new exListBoxItem(i, uList[i].LoginName, uList[i].DateIn.ToString(), uList[i].LoginName, uList[i].LoginName, uList[i].LoginName, uList[i].LoginName, uList[i].DateIn.ToString(), imageList1.Images[img]));
                }
            }
            catch
            {
                Console.WriteLine("Unknown Error on ListBox");
            }
        }

        internal void SendChat(Color color, string dt, string from, string message)
        {
            if (richTextBoxChat.InvokeRequired)
            {
                richTextBoxChat.Invoke(new MethodInvoker(() => SendChat(color, dt, from, message)));
                return;
            }
            //Random randomGen = new Random();
            //KnownColor[] names = (KnownColor[])Enum.GetValues(typeof(KnownColor));
            //KnownColor randomColorName = names[randomGen.Next(names.Length)];
            //Color randomColor = Color.FromKnownColor(randomColorName);

            richTextBoxChat.SelectionColor = Color.Black;
            richTextBoxChat.AppendText(string.Format("[{0}] {1}: ", dt, from));
            richTextBoxChat.SelectionColor = color;
            richTextBoxChat.AppendText(message);
            richTextBoxChat.AppendText(Environment.NewLine);
            richTextBoxChat.ScrollToCaret();

            recentMessage = message;

            ListPanelItem lpi = new ListPanelItem();
            lpi.Paint += lpi_Paint;
            lpi.MouseDown += listPanelChatItem_MouseDown;
            lpi.Caption = from;
            lpi.CaptionColor = Color.Transparent;
            lpi.Content = message;
            lpi.backgroundColor = color;
            lpi.ContentColor = Color.Transparent;
            if (loginName == from)
                lpi.Image = imageList1.Images[0];
            else
                lpi.Image = imageList1.Images[1];//randomGen.Next(imageList1.Images.Count
            listPanelChat.AddItem(lpi);

        }

        void lpi_Paint(object sender, PaintEventArgs e)
        {
            speechBalloon MyBalloon = new speechBalloon()
            {
                /*MyBalloon.RedrawRequired += MyBalloon_RedrawRequired;*/
                Left = 50 /* (listPanelChat.Width / 2) - (MyBalloon.Width / 2);*/,
                Top = 3 /* (listPanelChat.Height / 2) - (MyBalloon.Height / 2);*/
            };
            SizeF textSize = TextRenderer.MeasureText(((ListPanelItem)sender).Content, new Font(Font.FontFamily, 9, FontStyle.Regular));
            if (textSize.Width < 380)
            {
                MyBalloon.Width = (int)textSize.Width + 20;
            }
            else
            {
                MyBalloon.Width = 400;
            }

            if (((ListPanelItem)sender).Height < 71)
            {
                if (MyBalloon.Width < 400)
                    MyBalloon.Height = (int)((textSize.Width / MyBalloon.Width) * textSize.Height + textSize.Height);//((ListPanelItem)sender).Height;
                else
                    MyBalloon.Height = (int)((textSize.Width / MyBalloon.Width) * textSize.Height + textSize.Height + 15);
            }
            else
                MyBalloon.Height = ((ListPanelItem)sender).Height - 5;

            MyBalloon.FillColor = ((ListPanelItem)sender).backgroundColor;
            MyBalloon.Shape = BalloonShape.Rectangle;
            MyBalloon.RectangleCornerRadius = 10;
            MyBalloon.BorderVisible = false;
            MyBalloon.TailLength = 142;
            MyBalloon.TailBaseWidth = 32;
            MyBalloon.TailRotation = 74;
            MyBalloon.TailVisible = false;
            MyBalloon.Text = ((ListPanelItem)sender).Content;
            if (loginName == ((ListPanelItem)sender).Caption)
                MyBalloon.TextColor = Color.Black;
            else
                MyBalloon.TextColor = Color.Blue;

            MyBalloon.Draw(e.Graphics);
        }

        void MyBalloon_RedrawRequired(object sender, EventArgs e)
        {
            //listPanelChat.Refresh();
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (textBoxMessage.Text.Trim() == "")
                return;
            if (!(client == null))
            {
                client.setNewChatLine(loginName, textBoxMessage.Text, ref lastDateTime);
            }
            textBoxMessage.Text = "";
        }

        private void timerStart_Tick(object sender, EventArgs e)
        {
            if (timerStart.Interval < 900)
            {
                timerStart.Interval += 100;
                this.Text += " .";
                return;
            }
            timerStart.Enabled = false;
            this.Text = string.Format("{0} - {1}", lastLab, "Chat");
            startChat();
        }

        private void startChat()
        {
            try
            {
                string myRemoteIP = string.Format("http://{0}:{1}/RobotWebCamServer", newIP, lastPort);
                DateTime dt = DateTime.Now;
                client = new WebCamService.WebCamServiceClient("NetTcpBinding_IWebCamService", myRemoteIP);
                client.chatStart(loginName, ref dt);
                lastDateTime = dt;

            }
            catch (System.ServiceModel.CommunicationException ex)
            { MessageBox.Show(ex.Message); return; }

            System.Threading.Thread.Sleep(50);

            timerRetreiveData.Tick += new EventHandler(timerRetreiveData_Tick);
            timerRetreiveData.Start();

            System.Threading.Thread.Sleep(50);
            buttonSend.Enabled = true;
            timerGetUserList.Tick += new EventHandler(timerGetUserList_Tick);
            timerGetUserList.Start();

        }

        private void timerGetUserList_Tick(object sender, EventArgs e)
        {
            if (client == null)
                return;
            try
            {
                timerGetUserList.Stop();
                string newUser = client.getNewUserList();
                if (newUser == "Null" || newUser == null)
                {
                    timerGetUserList.Start();
                    return;
                }

                checkForNewUsers(newUser);

            }
            catch
            {
                return;
            }
            timerGetUserList.Start();
        }

        private void checkForNewUsers(string newUser)
        {

            var result = new JavaScriptSerializer().Deserialize<List<bufferUserList>>(newUser);
            if (result == null)
                return;

            if (result.Count != chatUserBuffer.Count)
            {
                chatUserBuffer = result;
                fillListBoxUsers(result, exListBoxUsers);
                return;
            }
            bool isSync = true;
            for (int i = 0; i < result.Count; i++)
            {
                isSync = false;
                for (int j = 0; j < chatUserBuffer.Count; j++)
                {
                    if (chatUserBuffer[j].LoginName == result[i].LoginName)
                    {
                        isSync = true;
                        break;
                    }
                }
                if (!isSync)
                {
                    chatUserBuffer = result;
                    fillListBoxUsers(result, exListBoxUsers);
                    return;
                }
            }

        }

        private void timerRetreiveData_Tick(object sender, EventArgs e)
        {
            timerRetreiveData.Stop();
            timerRetreiveData.Enabled = false;
            getNewChatLine();
            return;

            if (client == null)
                return;
            try
            {
                string newline = client.getNewChatLine(loginName, ref lastDateTime);
                if (newline == "Null" || newline == null)
                    return;

                /*var result = new JavaScriptSerializer().Deserialize<List<chatTextList>>(newline);
                if (result == null)
                    return;
                for (int i = 0; i < result.Count; i++)
                {
                    if (result[i].LoginName.ToLower() == loginName.ToLower())
                        SendChat(Color.White, result[i].DateIn.ToShortTimeString(), result[i].LoginName, result[i].Text);
                    else
                        SendChat(Color.FromArgb(204, 240, 240), result[i].DateIn.ToShortTimeString(), result[i].LoginName, result[i].Text);
                }*/

                //richTextBoxChat.Text += newline+"\r\n";
                newline = newline.Replace("§§§", "‰");
                string[] lines = newline.Split("‰".ToCharArray());
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i] != "")
                    {
                        newline = lines[i].Replace("@@@", "‰");
                        string[] parts = newline.Split("‰".ToCharArray());
                        if (parts[1].ToLower() == loginName.ToLower())
                            SendChat(Color.White, (DateTime.Parse((parts[0].Replace("[", "")).Replace("]", ""))).ToShortTimeString(), parts[1], parts[2]);
                        else
                            SendChat(Color.FromArgb(204,240,240), (DateTime.Parse((parts[0].Replace("[", "")).Replace("]", ""))).ToShortTimeString(), parts[1], parts[2]);
                    }
                }
                
            }
            catch
            {
                return;
            }

        }

        private void formChat_Shown(object sender, EventArgs e)
        {
            timerStart.Enabled = true;
            //MyBalloon = new speechBalloon();
            //MyBalloon.RedrawRequired += MyBalloon_RedrawRequired;
        }

        private void formChat_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (!(client == null))
                {
                    client.stop_Chat(loginName);
                }

                System.Threading.Thread.Sleep(1000);
                client.Close();

                this.Dispose();
            }
            catch (Exception ex)
            {
                if (!(client == null))
                {
                    client.Abort();
                }
            }
        }

        private void listPanelChat_Paint(object sender, PaintEventArgs e)
        {
            //if (MyBalloon!=null)
                //MyBalloon.Draw(e.Graphics);
        }

        private void textBoxMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)10) //Ctrl+Enter key
            {
                //textBoxMessage.Text = textBoxMessage.Text + Environment.NewLine;
                int pos = textBoxMessage.SelectionStart;
                textBoxMessage.SelectedText = "\r"; 
                textBoxMessage.SelectionStart = pos;
            }
        }

        private void listPanelChat_ControlAdded(object sender, ControlEventArgs e)
        {
            listPanelChat.ScrollControlIntoView(e.Control);
            listPanelChat.VerticalScroll.Value = listPanelChat.VerticalScroll.Maximum;
            listPanelChat.PerformLayout();
        }

        private void listPanelChatItem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (((ListPanelItem)sender).Caption == loginName)
                    contextMenuChat.Items[0].Text = "myself ;)";
                else
                    contextMenuChat.Items[0].Text = ((ListPanelItem)sender).Caption;
                chatTextBufferToCopy = ((ListPanelItem)sender).Content;
                Point p = this.PointToClient(Cursor.Position);
                contextMenuChat.Show(listPanelChat, p.X - listPanelChat.Location.X, p.Y - listPanelChat.Location.Y);
            }
        }

        private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(chatTextBufferToCopy);
        }


        private void getNewChatLine()
        {
            bool isStreamChatLine = true;

            new Thread(delegate()
            {
                System.Diagnostics.Stopwatch swFrameRate = new System.Diagnostics.Stopwatch();
                swFrameRate.Start();
                while (isStreamChatLine)
                {
                    if (swFrameRate.Elapsed.TotalMilliseconds >= 2000)
                    {
                        swFrameRate.Restart();
                        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
                        try
                        {
                            sw.Start();


                            if (client == null)
                                continue;

                            string newline = client.getNewChatLine(loginName, ref lastDateTime);
                            if (newline == "Null" || newline == null)
                                continue;

                            /*var result = new JavaScriptSerializer().Deserialize<List<chatTextList>>(newline);
                            if (result == null)
                                return;
                            for (int i = 0; i < result.Count; i++)
                            {
                                if (result[i].LoginName.ToLower() == loginName.ToLower())
                                    SendChat(Color.White, result[i].DateIn.ToShortTimeString(), result[i].LoginName, result[i].Text);
                                else
                                    SendChat(Color.FromArgb(204, 240, 240), result[i].DateIn.ToShortTimeString(), result[i].LoginName, result[i].Text);
                            }*/

                            //richTextBoxChat.Text += newline+"\r\n";
                            newline = newline.Replace("§§§", "‰");
                            string[] lines = newline.Split("‰".ToCharArray());
                            for (int i = 0; i < lines.Length; i++)
                            {
                                if (lines[i] != "")
                                {
                                    newline = lines[i].Replace("@@@", "‰");
                                    string[] parts = newline.Split("‰".ToCharArray());
                                    if (parts[1].ToLower() == loginName.ToLower())
                                        SendChat(Color.White, (DateTime.Parse((parts[0].Replace("[", "")).Replace("]", ""))).ToShortTimeString(), parts[1], parts[2]);
                                    else
                                        SendChat(Color.FromArgb(204, 240, 240), (DateTime.Parse((parts[0].Replace("[", "")).Replace("]", ""))).ToShortTimeString(), parts[1], parts[2]);
                                }
                            }
                        }
                        catch
                        {
                            isStreamChatLine = false;
                        }
                    }
                }
            }).Start();
        }

    }
}

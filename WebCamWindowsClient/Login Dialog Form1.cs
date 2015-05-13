using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace WebCamWindowsClient
{
    public partial class Login_Dialog_Form1 : Form
    {
        public Login_Dialog_Form1()
        {
            this.Hide();
            Thread splashthread = new Thread(new ThreadStart(SplashScreen.ShowSplashScreen));
            splashthread.IsBackground = true;
            splashthread.Start();
            InitializeComponent();
        }
        private bool ValidateUsername()
        {

            //TODO: add code to validate User Name here.
            return true;
        }
        private bool ValidatePassword()
        {
            if (!ValidateUsername())
            {
                MessageBox.Show("Wrong Username", "Invalid Username", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                if (false)
                {
                    MessageBox.Show("Wrong Password", "Invalid Password", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    //TODO: add code to validate password.
                    return true;
                }
            }
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!ValidatePassword())
            {
                txtUserName.Clear();
                txtPassword.Clear();
                return;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Hide();
                formControlpanel f = new formControlpanel();
                f.setParameters(txtUserName.Text);
                f.Show();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtUserName.Clear();
            txtPassword.Clear();
            this.Close();
        }

        private void Login_Dialog_Form1_Load(object sender, EventArgs e)
        {
            Application.DoEvents();
            SplashScreen.UdpateStatusText("Checking Connection...");
            if (CheckForInternetConnection())
                SplashScreen.UdpateStatusTextWithStatus("Success Message", TypeOfMessage.Success);
            else
                SplashScreen.UdpateStatusTextWithStatus("Error Message", TypeOfMessage.Error);
            Thread.Sleep(2000);

            this.Show();
            SplashScreen.CloseSplashScreen();
            this.Activate();
        }

        private bool CheckForInternetConnection()
        {
            try
            {
                byte[] imageData;
                using (var clientIP = new WebClient())
                {
                    imageData = clientIP.DownloadData("http://www.soheyln.com/myIP/index.php");
                    var str = System.Text.Encoding.Default.GetString(imageData);
                    int posStart = str.IndexOf("PublicIP\":\"") + 11;
                    int posEnd = str.IndexOf("\"}]");
                    string newIP = str.Substring(posStart, posEnd - posStart);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}

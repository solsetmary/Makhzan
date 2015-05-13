using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WebCamWindowsClient
{
    public partial class formCalendar : Form
    {
        public formCalendar()
        {
            InitializeComponent();
        }


        public void setParameters(string webbrowserURL)
        {
            webBrowser1.Navigate(webbrowserURL);
        }

        private void webBrowser1_Resize(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }


    }
}

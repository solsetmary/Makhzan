//using AForge.Video;
//using AForge.Video.DirectShow;
//using AForge.Video.FFMPEG;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;

namespace WebCamWindowsClient
{
    public partial class formWelcome : Form
    {
        /*private FilterInfoCollection VideoCaptureDevices;
        private VideoCaptureDevice FinalVideo = null;
        private VideoCaptureDeviceForm captureDevice;
        private Bitmap video;
        VideoFileWriter writer;
        string filenameupload;
        string pathupload;
        ftp ftpClient;*/
        int i = 0;
        int j = 0;

        public formWelcome()
        {
            InitializeComponent();
        }

        /*void FinalVideo_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (button5.Text == "Stop Record")
            {
                video = (Bitmap)eventArgs.Frame.Clone();
                if (writer.IsOpen)
                {
                    writer.WriteVideoFrame(video);
                    i++;
                    if(i>80)
                        button6_Click(null, null);
                }
            }
            else
            {
                video = (Bitmap)eventArgs.Frame.Clone();
            }
            if (pictureBox1.InvokeRequired)
            {
                pictureBox1.BeginInvoke((MethodInvoker)delegate
                {
                    pictureBox1.Image = video;
                });
            }
            else
            {
                pictureBox1.Image = video;
            }
        }*/

        private void buttonConnection_Click(object sender, EventArgs e)
        {
            formControlpanel parent = (formControlpanel)this.MdiParent;
            parent.buttonConnect_Click(null,null);
        }

        private void buttonCalendar_Click(object sender, EventArgs e)
        {
            formControlpanel parent = (formControlpanel)this.MdiParent;
            parent.RunFormCalendar();

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            //create Prcess start info object with devcon.exe : 
            //>>>>>>>>>>>> devcon hwids =ports
            //USB\VID_2341&PID_8036&MI_00\8&3A3900D5&0&0000
            //    Name: Arduino Leonardo (COM34)
            //    Hardware ID's:
            //        USB\VID_2341&PID_8036&REV_0100&MI_00
            //        USB\VID_2341&PID_8036&MI_00
            //    Compatible ID's:
            //        USB\Class_02&SubClass_02&Prot_01
            //        USB\Class_02&SubClass_02
            //        USB\Class_02
            //1 matching device(s) found.
            //>>>>>>>>>>>> devcon disable USB\Class_02
            //USB\VID_2341&PID_8036&MI_00\8&3A3900D5&0&0000               : Disabled
            //USB\VID_2341&PID_8036\7&31FCF732&0&3                        : Disable failed
            //1 device(s) disabled.
            //>>>>>>>>>>>> devcon enable USB\Class_02
            //USB\VID_2341&PID_8036&MI_00\8&3A3900D5&0&0000               : Enabled
            //USB\VID_2341&PID_8036\7&31FCF732&0&3                        : Enabled
            //2 device(s) enabled.
            ProcessStartInfo PSI = new ProcessStartInfo(@"C:\Users\Soheyl\Desktop\hyperterminal\devcon.exe");

            PSI.Arguments = @"disable USB\Class_02";
            Process.Start(PSI);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            /*if (captureDevice.ShowDialog(this) == DialogResult.OK)
            {
                VideoCaptureDevice videoSource = captureDevice.VideoDevice;
                FinalVideo = captureDevice.VideoDevice;
                FinalVideo.NewFrame += new NewFrameEventHandler(FinalVideo_NewFrame);
                FinalVideo.Start();
            }*/
        }

        private void button5_Click(object sender, EventArgs e)
        {
            /*try
            {
                string path = Path.GetDirectoryName(Application.ExecutablePath) + @"\Videos\";
                if (Directory.Exists(path) == false)
                {
                    Directory.CreateDirectory(path);
                }

                int h = captureDevice.VideoDevice.VideoResolution.FrameSize.Height;
                int w = captureDevice.VideoDevice.VideoResolution.FrameSize.Width;

                filenameupload = "VID" + DateTime.Now.ToString("hhmmss") + ".mp4";
                pathupload = path + filenameupload;
                writer.Open(pathupload, w, h, 8, VideoCodec.MPEG4);
                button5.Text = "Stop Record";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }*/
        }

        private void button6_Click(object sender, EventArgs e)
        {
            /*if (FinalVideo == null)
            { return; }
            if (FinalVideo.IsRunning)
            {
                writer.Close();
            }
            else
            {
                this.FinalVideo.Stop();
                writer.Close();
            }
            i = 0;
            j++;
            if (j < 6)
                button5_Click(null, null);*/

            /*bool isLocked = true;
            string filename = pathupload;
            string filename2 = pathupload + "_temp";

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

            ftpClient.upload("cam/2/3/" + filenameupload, pathupload);*/
            SystemSounds.Beep.Play();
        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

            //create Prcess start info object with devcon.exe : 
            //>>>>>>>>>>>> devcon hwids =ports
            //USB\VID_2341&PID_8036&MI_00\8&3A3900D5&0&0000
            //    Name: Arduino Leonardo (COM34)
            //    Hardware ID's:
            //        USB\VID_2341&PID_8036&REV_0100&MI_00
            //        USB\VID_2341&PID_8036&MI_00
            //    Compatible ID's:
            //        USB\Class_02&SubClass_02&Prot_01
            //        USB\Class_02&SubClass_02
            //        USB\Class_02
            //1 matching device(s) found.
            //>>>>>>>>>>>> devcon disable USB\Class_02
            //USB\VID_2341&PID_8036&MI_00\8&3A3900D5&0&0000               : Disabled
            //USB\VID_2341&PID_8036\7&31FCF732&0&3                        : Disable failed
            //1 device(s) disabled.
            //>>>>>>>>>>>> devcon enable USB\Class_02
            //USB\VID_2341&PID_8036&MI_00\8&3A3900D5&0&0000               : Enabled
            //USB\VID_2341&PID_8036\7&31FCF732&0&3                        : Enabled
            //2 device(s) enabled.
            ProcessStartInfo PSI = new ProcessStartInfo(@"C:\Users\Soheyl\Desktop\hyperterminal\devcon.exe");

            PSI.Arguments = @"enable USB\Class_02";
            Process.Start(PSI);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //ftpClient.upload("cam/2/3/" + filenameupload, pathupload);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //pictureBox1.Image.Save(pathupload + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private void formWelcome_Load(object sender, EventArgs e)
        {
            /*VideoCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            captureDevice = new VideoCaptureDeviceForm();
            try
            { writer = new VideoFileWriter(); }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            ftpClient = new ftp(@"ftp://ftp.labpp.org/", "labpp.org", "soheyln1357");*/

        }
    }
}

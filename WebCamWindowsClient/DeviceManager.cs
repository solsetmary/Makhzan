using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebCamWindowsClient
{
    public static class DeviceManager
    {
        static formDevice fd = null;

        /// <summary>
        /// Displays the formDevice
        /// </summary>
        public static void ShowFormDevice()
        {
            if (fd == null)
            {
                fd = new formDevice();
                fd.ShowFormDevice();
            }
        }

        /// <summary>
        /// Closes the SplashScreen
        /// </summary>
        public static void CloseFormDevice()
        {
            if (fd != null)
            {
                fd.CloseFormDevice();
                fd = null;
            }
        }

        /// <summary>
        /// Update Parameters
        /// </summary>
        /// <param name="Text">Message</param>
        public static void setParameters(string lName,string theIP, string cPort, string cValue, string cText, string lText, string lLabID, string lDevID, string mIndex, string mText, string mPort, string mLab)
        {
            if (fd != null)
                fd.setParameters(lName, theIP,  cPort,  cValue,  cText,  lText,  lLabID, lDevID, mIndex,  mText,  mPort,  mLab);

        }
        
    }
}

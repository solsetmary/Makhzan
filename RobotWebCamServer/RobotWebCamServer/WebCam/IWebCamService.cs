using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Drawing;
using System.ServiceModel.Web;

namespace RobocodServer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IWebCamService" in both code and config file together.
    [ServiceContract]
    public interface IWebCamService
    {

        [OperationContract]
        void arduinoStart(string loginName, string aPort, string lastLabID, string lastDevID, string userPermission, ref DateTime date);

        [OperationContract]
        string getArduinoUserList(string loginName);

        [OperationContract]
        void sendSerialData(string loginName, string serialText, ref DateTime date);

        [OperationContract]
        string getNewSerialData(string loginName, ref DateTime lastDate);
        
        [OperationContract]
        string arduinoCompiling(string sourceCode);

        [OperationContract]
        string arduinoUploading();

        [OperationContract]
        void stop_arduino(string loginName);

        [OperationContract]
        void chatStart(string loginName, ref DateTime date);

        [OperationContract]
        void setNewChatLine(string loginName, string lineChat, ref DateTime date);

        [OperationContract]
        string getNewChatLine(string loginName, ref DateTime date);

        [OperationContract]
        string getNewUserList();

        [OperationContract]
        void stop_Chat(string loginName);

        [OperationContract]
        string getWaveMD5();

        [OperationContract]
        byte[] getMicBuffer();

        [OperationContract]
        void micStart(int mValue);

        [OperationContract]
        int waveMicBuffer();

        [OperationContract]
        void stop_Mic();

        [OperationContract]
        void Record(string loginName, string cValue, string cName, string lID, string cID);

        [OperationContract]
        void stop_record(string loginName);

        [OperationContract]
        System.IO.Stream getWebCamImage(string loginName);

        [OperationContract]
        int CamerasNr();

        [OperationContract]
        string[] CamerasNames();

        [OperationContract]
        string[] CamerasValues();
    }
}

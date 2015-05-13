using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RobotWebCamServer
{
    public sealed class sharedVariables
    {
        private static readonly sharedVariables _instance = new sharedVariables();
        private string _homeURL;
        private string _setimagespreviewURL;
        private string _setuserlivelogURL;
        private string _deluserlivelogURL;
        private string _serialdatalogURL;
        private string _stateURL;
        private string _statusURL;
        private string _DeviceType;
        private string _labID;
        private string _devID;
        private string _devIndex;
        private string _arduinoBoard;    
   
        public static sharedVariables Instance
        {
            get{return _instance;}
        }

        static sharedVariables(){}

        private sharedVariables(){}// do constructor logic here
        
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

        public string homeURL
        {
            get { return _homeURL; }
            set { _homeURL = value; }
        }

        public string setimagespreviewURL
        {
            get { return _setimagespreviewURL; }
            set { _setimagespreviewURL = value; }
        }

        public string setuserlivelogURL
        {
            get { return _setuserlivelogURL; }
            set { _setuserlivelogURL = value; }
        }

        public string deluserlivelogURL
        {
            get { return _deluserlivelogURL; }
            set { _deluserlivelogURL = value; }
        }

        public string serialdatalogURL
        {
            get { return _serialdatalogURL; }
            set { _serialdatalogURL = value; }
        }

        public string stateURL
        {
            get { return _stateURL; }
            set { _stateURL = value; }
        }

        public string statusURL
        {
            get { return _statusURL; }
            set { _statusURL = value; }
        }

        public string DeviceType
        {
            get { return _DeviceType; }
            set { _DeviceType = value; }
        }

        public string labID
        {
            get { return _labID; }
            set { _labID = value; }
        }

        public string devID
        {
            get { return _devID; }
            set { _devID = value; }
        }

        public string devIndex
        {
            get { return _devIndex; }
            set { _devIndex = value; }
        }

        public string arduinoBoard
        {
            get { return _arduinoBoard; }
            set { _arduinoBoard = value; }
        }
    }
}

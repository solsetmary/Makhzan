using AutocompleteMenuNS;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Media;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using UrielGuy.SyntaxHighlightingTextBox;

namespace WebCamWindowsClient
{
    public partial class formArduino : Form
    {

        #region Variables

        private WebCamService.WebCamServiceClient client;
        System.Timers.Timer timerRetreiveSerialData;
        DateTime lastDateTime = DateTime.Now;
        public string newIP;
        public string lastPort;
        public string loginName;
        public string lastText;
        public string lastLab;
        public string lastLabID;
        public string lastDevID;
        public string lastCOMport;
        public string sdDate;
        public string sdTime;
        List<bufferUserList> arduinoUserList;
        string[] keywords = { "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const", "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event", "explicit", "extern", "false", "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in", "int", "interface", "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out", "override", "params", "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short", "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true", "try", "typeof", "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile", "while", "add", "alias", "ascending", "descending", "dynamic", "from", "get", "global", "group", "into", "join", "let", "orderby", "partial", "remove", "select", "set", "value", "var", "where", "yield" };
        string[] methods = { "Equals()", "GetHashCode()", "GetType()", "ToString()" };
        string[] snippets = { "if(^)\n{\n}", "if(^)\n{\n}\nelse\n{\n}", "for(^;;)\n{\n}", "while(^)\n{\n}", "do${\n^}while();", "switch(^)\n{\n\tcase : break;\n}" };
        string[] declarationSnippets = { 
               "public class ^\n{\n}", "private class ^\n{\n}", "internal class ^\n{\n}",
               "public struct ^\n{\n}", "private struct ^\n{\n}", "internal struct ^\n{\n}",
               "public void ^()\n{\n}", "private void ^()\n{\n}", "internal void ^()\n{\n}", "protected void ^()\n{\n}",
               "public ^{ get; set; }", "private ^{ get; set; }", "internal ^{ get; set; }", "protected ^{ get; set; }"
               };

        //Shared 256 bit Key and IV here
        const string sKy = "lkirwf897+22#bbtrm8814z5qq=498j5"; //32 chr shared ascii string (32 * 8 = 256 bit)
        const string sIV = "741952hheeyy66#cs!9hjv887mxx7@8y"; //32 chr shared ascii string (32 * 8 = 256 bit)
        bool isStreamSerialData = true;

        #endregion

        public formArduino()
        {
            InitializeComponent();
            BuildAutocompleteMenu();
        }

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

        public string Base64ToString(string base64String)
        {
            try
            {
                string str = base64String.Substring(0, base64String.Length).Replace(@"\/", "/");

                byte[] srialdataByte = Convert.FromBase64String(str);
                string srialdataString = Encoding.UTF8.GetString(srialdataByte); 
                return srialdataString;
            }
            catch
            {
                return "error";
            }
        }

        private void BuildAutocompleteMenu()
        {
            var items = new List<AutocompleteItem>();

            foreach (var item in snippets)
                items.Add(new SnippetAutocompleteItem(item) { ImageIndex = 1 });
            foreach (var item in declarationSnippets)
                items.Add(new DeclarationSnippet(item) { ImageIndex = 0 });
            foreach (var item in methods)
                items.Add(new MethodAutocompleteItem(item) { ImageIndex = 2 });
            foreach (var item in keywords)
                items.Add(new AutocompleteItem(item) { ImageIndex = 3 });

            items.Add(new InsertSpaceSnippet());
            items.Add(new InsertSpaceSnippet(@"^(\w+)([=<>!:]+)(\w+)$"));
            items.Add(new InsertEnterSnippet());

            //set as autocomplete source
            autocompleteMenu1.SetAutocompleteItems(items);
        }

        public string ResultUploading { get { return this.richTextBoxOutputUpload.Text; } set { this.richTextBoxOutputUpload.Text = value; } }

        public void setParameters(string theIP, string cPort, string aPort, string login, string cText, string lText, string lLabID, string lDevID)
        {
            startProgressBar();
            newIP = theIP;
            lastPort = cPort;
            loginName = login;
            lastText = cText;
            lastLab = lText;
            lastLabID = lLabID;
            lastDevID = lDevID;
            lastCOMport = aPort;
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate
                {
                    this.Text = string.Format("{0} - {1}", lText, cText); //cross thread
                });
            }
            else
            {
                this.Text = string.Format("{0} - {1}", lText, cText); //cross thread
            }

            ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();
            MenuItem menuItem = new MenuItem("Undo        Ctrl+Z");
            menuItem.Click += new EventHandler(UndoAction);
            contextMenu.MenuItems.Add(menuItem);
            menuItem = new MenuItem("Redo       Ctrl+Y");
            menuItem.Click += new EventHandler(RedoAction);
            contextMenu.MenuItems.Add(menuItem);
            menuItem = new MenuItem("-");
            contextMenu.MenuItems.Add(menuItem);
            menuItem = new MenuItem("Cut           Ctrl+X");
            menuItem.Click += new EventHandler(CutAction);
            contextMenu.MenuItems.Add(menuItem);
            menuItem = new MenuItem("Copy         Ctrl+C");
            menuItem.Click += new EventHandler(CopyAction);
            contextMenu.MenuItems.Add(menuItem);
            menuItem = new MenuItem("Paste        Ctrl+P");
            menuItem.Click += new EventHandler(PasteAction);
            contextMenu.MenuItems.Add(menuItem);
            menuItem = new MenuItem("Delete       Del");
            menuItem.Click += new EventHandler(DeleteAction);
            contextMenu.MenuItems.Add(menuItem);
            menuItem = new MenuItem("-");
            contextMenu.MenuItems.Add(menuItem);
            menuItem = new MenuItem("Select All   Ctrl+A");
            menuItem.Click += new EventHandler(SelectAllAction);
            contextMenu.MenuItems.Add(menuItem);

            syntaxHighlightingTextBoxSourceCode.ContextMenu = contextMenu;

            arduinoUserList = new List<bufferUserList>();
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

        private void startArduino()
        {
            string strPermission = checkForPermission(loginName);

            //RegistryKey registryKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry32);

            //Console.WriteLine("registryKey" + registryKey);
            //registryKey = registryKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\USBSTOR");
            //Object val = registryKey.GetValue("Start");
            //Console.WriteLine("The val is:" + val);
            ////disable USB storage...
            //Microsoft.Win32.Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\USBSTOR", "Start", 4, Microsoft.Win32.RegistryValueKind.DWord);

            ////enable USB storage...
            //Microsoft.Win32.Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\USBSTOR", "Start", 3, Microsoft.Win32.RegistryValueKind.DWord);

            try
            {
                ThreadPool.QueueUserWorkItem(delegate
                {
                    string myRemoteIP = string.Format("http://{0}:{1}/RobotWebCamServer", newIP, lastPort);
                    DateTime dt = DateTime.Now;
                    client = new WebCamService.WebCamServiceClient("NetTcpBinding_IWebCamService", myRemoteIP);
                    try
                    {
                        client.arduinoStart(loginName, "COM" + lastCOMport, lastLabID, lastDevID, strPermission, ref dt);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    lastDateTime = dt;
                }, null);

            }
            catch (System.ServiceModel.CommunicationException ex)
            {
                //MessageBox.Show(ex.Message); 
                return;
            }

            Thread.Sleep(100);
            GetArduinoUserList(loginName);
            stopProgressBar();
            setSyntaxHighlight(syntaxHighlightingTextBoxSourceCode, "Arduino");
            //string mysource = File.OpenText(@"test\test.ino").ReadToEnd(); ;
            //syntaxHighlightingTextBoxSourceCode.Text = mysource;
            toolStripButtonOpenSerial.Enabled = true;
            toolStripButtonCompile.Enabled = true;
            toolStripButtonUpload.Enabled = true;
            timerGetUserList.Tick += new EventHandler(timerGetUserList_Tick);
            timerGetUserList.Start();

            syntaxHighlightingTextBoxSourceCode.Enabled = true;
            toolStripButtonZoomIn.Enabled = true;
            toolStripButtonZoomOut.Enabled = true;

            startProgressBar();
            this.StartBackgroundWorkSqlQueryGetUser()
                    .ContinueWith((t) => this.setResultSqlGetUserQuery(t.Result), TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void AddUserToUserLiveLog(string lName)
        {
            try
            {
                System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
                reqparm.Add("l", lastLabID);
                reqparm.Add("d", lastDevID);
                reqparm.Add("u", lName);
                postWebClient(reqparm, "http://labpp.org/get/new/sd/");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
            }
        }

        private string postWebClient(NameValueCollection reqParam, string postURL)
        {
            string responseBody = "";
            using (WebClient client = new WebClient())
            {
                byte[] responseBytes = client.UploadValues(postURL, "POST", reqParam);
                responseBody = Encoding.UTF8.GetString(responseBytes);
            }

            return responseBody;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        private string checkForPermission(string loginName)
        {
            DialogResult dr = MessageBox.Show(this, string.Format("Would you like to have the Read/Write permission for {0}?", loginName), "Permission", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
                return "rw";
            else
                return "ro";
        }

        private void GetArduinoUserList(string loginName)
        {
            if (client == null)
                return;
            try
            {
                timerGetUserList.Stop();
                string newUser = client.getArduinoUserList(loginName);
                if (newUser == "Null" || newUser == null)
                {
                    timerGetUserList.Start();
                    return;
                }

                checkForNewUsers(newUser);

            }
            catch
            {
                timerGetUserList.Start();
                return;
            }

        }

        private void checkForNewUsers(string newUser)
        {

            var result = new JavaScriptSerializer().Deserialize<List<bufferUserList>>(newUser);
            if (result == null)
                return;

            if (result.Count != arduinoUserList.Count)
            {
                arduinoUserList = result;
                fillListBoxUsers(result, exListBoxUsers);
                return;
            }
            bool isSync = true;
            for (int i = 0; i < result.Count; i++)
            {
                isSync = false;
                for (int j = 0; j < arduinoUserList.Count; j++)
                {
                    if (arduinoUserList[j].LoginName == result[i].LoginName)
                    {
                        isSync = true;
                        break;
                    }
                }
                if (!isSync)
                {
                    arduinoUserList = result;
                    fillListBoxUsers(result, exListBoxUsers);
                    return;
                }
            }

        }

        private void setSyntaxHighlight(SyntaxHighlightingTextBox shtb, string p)
        {
            shtb.Font = new Font("courier new", 9);
            //shtb.Location = new Point(0, 0);
            //shtb.Dock = DockStyle.Fill;
            shtb.Seperators.Add(' ');
            shtb.Seperators.Add('\r');
            shtb.Seperators.Add('\n');
            shtb.Seperators.Add(',');
            shtb.Seperators.Add('.');
            shtb.Seperators.Add('-');
            shtb.Seperators.Add('+');
            shtb.Seperators.Add(':');
            shtb.Seperators.Add(';');
            //shtb.Seperators.Add('&');
            shtb.Seperators.Add('(');
            shtb.Seperators.Add(')');
            shtb.Seperators.Add('=');
            //Controls.Add(shtb);
            shtb.WordWrap = false;
            shtb.ScrollBars = RichTextBoxScrollBars.Both;// & RichTextBoxScrollBars.ForcedVertical;

            shtb.FilterAutoComplete = false;
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("/*", "*/", Color.Green, null, DescriptorType.ToCloseToken, DescriptorRecognition.StartsWith, false));

            //shtb.HighlightDescriptors.Add(new HighlightDescriptor("<", ">", Color.Orange, null, DescriptorType.ToCloseToken, DescriptorRecognition.StartsWith, false));

            //shtb.HighlightDescriptors.Add(new HighlightDescriptor("'", "'", Color.Yellow, null, DescriptorType.ToCloseToken, DescriptorRecognition.StartsWith, false));
            //shtb.HighlightDescriptors.Add(new HighlightDescriptor("\"", "\"", Color.Red, null, DescriptorType.ToCloseToken, DescriptorRecognition.StartsWith, false));

            shtb.HighlightDescriptors.Add(new HighlightDescriptor("//", Color.Gray, null, DescriptorType.ToEOL, DescriptorRecognition.StartsWith, false));

            shtb.HighlightDescriptors.Add(new HighlightDescriptor("*", Color.DeepPink, null, DescriptorType.Word, DescriptorRecognition.StartsWith, false));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("&", Color.DeepPink, null, DescriptorType.Word, DescriptorRecognition.StartsWith, false));

            Color c = Color.YellowGreen;
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("Soheyl", c, new Font("courier new", 11, FontStyle.Bold), DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("Nazifi", c, new Font("courier new", 11, FontStyle.Bold), DescriptorType.Word, DescriptorRecognition.WholeWord, true));

            c = Color.Black;
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("Hello", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("World", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));

            c = Color.Blue;
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("include", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("#include", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("define", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("#define", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("SoftwareSerial", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("Servo", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));

            c = Color.Violet;
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("boolean", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("long", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("int", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("string", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("String", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("array", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("float", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("char", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("double", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("unsigned", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("byte", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("word", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("short", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("const", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("char *", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));

            c = Color.Firebrick;
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("begin", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("attach", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("pinMode", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("digitalWrite", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("digitalRead", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("analogReference", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("analogRead", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("analogWrite", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("analogReadResolution", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("analogWriteResolution", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("write", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("println", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("print", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("delayMicroseconds", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("pulseIn", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("read", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("available", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("delay", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("String", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("sizeof", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("tone", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("noTone", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("shiftIn", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("shiftOut", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("millis", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("pulseIn", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("delayMicroseconds", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("delay", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("micros", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("sqrt", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("pow", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("map", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("constrain", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("abs", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("max", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("min", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("sin", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("cos", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("tan", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("ransomSeed", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("random", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("lowByte", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("highByte", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("bitRead", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("bitWrite", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("bitSet", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("bitClear", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("attachInterrupt", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("detachInterrupt", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("interrupts", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("noInterrupts", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));

            c = Color.OrangeRed;
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("if", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("while", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("for", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("else", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("switch", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("case", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("while", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("do", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("goto", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("continue", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("break", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("return", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("void", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));

            c = Color.BlueViolet;
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("HIGH", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("LOW", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("DEC", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("INPUT", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("OUTPUT", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("INPUT_PULLUP", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("LED_BUILTIN", c, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));

            c = Color.RoyalBlue;
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("Stream", c, new Font("courier new", 11, FontStyle.Italic), DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("Serial", c, new Font("courier new", 11, FontStyle.Italic), DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("loop", c, new Font("courier new", 11, FontStyle.Italic), DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("setup", c, new Font("courier new", 11, FontStyle.Italic), DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("Keyboard", c, new Font("courier new", 11, FontStyle.Italic), DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            shtb.HighlightDescriptors.Add(new HighlightDescriptor("Mouse", c, new Font("courier new", 11, FontStyle.Italic), DescriptorType.Word, DescriptorRecognition.WholeWord, true));

        }

        /// <summary>
        /// Append text of the given color.
        /// </summary>
        /// <param name="box"></param>
        /// <param name="color"></param>
        /// <param name="text"></param>
        void AppendText(RichTextBox box, Color color, string text)
        {
            Application.DoEvents();
            box.BeginInvoke((MethodInvoker)delegate
            {
                int start = box.TextLength;
                box.AppendText(text);
                int end = box.TextLength;

                // Textbox may transform chars, so (end-start) != text.Length
                box.Select(start, end - start);
                {
                    box.SelectionColor = color;
                    // could set box.SelectionBackColor, box.SelectionFont too.
                }
                box.SelectionLength = 0; // clear
                box.ScrollToCaret();
            });


        }

        private object setResultCompiling(string p)
        {
            if (p.ToLower().IndexOf("error") != -1)
                toolStripButtonUpload.Enabled = false;
            else
                toolStripButtonUpload.Enabled = true;

            setFilterOfResultString(richTextBoxOutputCompile, p);

            richTextBoxOutputCompile.ScrollToCaret();
            EnableToolStripButton(toolStripButtonCompile, true);
            stopProgressBar();

            return true;
        }

        private void setFilterOfResultString(RichTextBox richText, string p)
        {
            // builder text
            string[] builderlines = p.Split("\r\n".ToCharArray());
            richText.Text = "";

            for (int i = 0; i < 3; i++)
            {
                if (builderlines[i] != "")
                {
                    AppendText(richText, Color.Yellow, builderlines[i] + Environment.NewLine);
                    p = p.Replace(builderlines[i], "");
                }
            }

            setDelay(richText, 2000);

            AppendText(richText, Color.White, Environment.NewLine);

            //error text
            if (p.ToLower().IndexOf("error") != -1)
            {
                int end = p.ToLower().IndexOf("[compiliation");
                string str = p.Substring(0, end - 1);
                string[] errorlines = str.Split("\r\n".ToCharArray());
                for (int i = 0; i < errorlines.Length; i++)
                {
                    if (errorlines[i] != "")
                    {
                        AppendText(richText, Color.Red, errorlines[i] + Environment.NewLine);
                        p = p.Replace(errorlines[i], "");
                        setDelay(richText, 1000);
                    }
                }
            }

            //compiling text
            string[] lines = p.Split("\r\n".ToCharArray());
            for (int i = 3; i < lines.Length; i++)
            {
                if (lines[i].ToLower().IndexOf("[compiliation") != -1)
                {
                    AppendText(richText, Color.Green, Environment.NewLine + lines[i] + Environment.NewLine);
                    setDelay(richText, 2000);
                }
                else if (lines[i].ToLower().IndexOf("error") != -1)
                {
                    AppendText(richText, Color.IndianRed, Environment.NewLine + lines[i] + Environment.NewLine + Environment.NewLine);
                    setDelay(richText, 3000);
                }
                else if (lines[i].ToLower().IndexOf("connecting") != -1)
                {
                    AppendText(richText, Color.Yellow, Environment.NewLine + lines[i] + Environment.NewLine + Environment.NewLine);
                    setDelay(richText, 2000);
                }
                else if (lines[i].ToLower().IndexOf("reading |") != -1)
                {
                    AppendText(richText, Color.Green, Environment.NewLine + lines[i] + Environment.NewLine + Environment.NewLine);
                    setDelay(richText, 2000);
                }
                else if (lines[i].ToLower().IndexOf("writing |") != -1)
                {
                    AppendText(richText, Color.Green, Environment.NewLine + lines[i] + Environment.NewLine + Environment.NewLine);
                    setDelay(richText, 2000);
                }
                else if (lines[i].ToLower().IndexOf("build target") != -1)
                {
                    AppendText(richText, Color.Yellow, Environment.NewLine + lines[i] + Environment.NewLine + Environment.NewLine);
                    setDelay(richText, 3000);
                }
                else if (lines[i].ToLower().IndexOf("forcing reset") != -1)
                {
                    AppendText(richText, Color.Yellow, Environment.NewLine + lines[i] + Environment.NewLine + Environment.NewLine);
                    setDelay(richText, 5000);
                }
                else if (lines[i].ToLower().IndexOf("successful") != -1)
                {
                    AppendText(richText, Color.Green, Environment.NewLine + lines[i] + Environment.NewLine + Environment.NewLine);
                    setDelay(richText, 3000);
                }
                else if (lines[i] != "")
                {
                    AppendText(richText, Color.White, lines[i] + Environment.NewLine);
                    setDelay(richText, 1000);
                }
                else
                {
                    //AppendText(richTextBoxOutputCompile, Color.White, lines[i] + Environment.NewLine);
                }
            }
            AppendText(richText, Color.White, Environment.NewLine);
            SystemSounds.Beep.Play();
        }

        private void setDelay(RichTextBox richText, int p)
        {
            System.Diagnostics.Stopwatch swDelay = new System.Diagnostics.Stopwatch();
            swDelay.Start();
            while (swDelay.Elapsed.TotalMilliseconds <= p)
            {
                Application.DoEvents();
            }
            swDelay.Stop();
            if (richText != null)
                richText.ScrollToCaret();
        }

        private Task<string> StartBackgroundWorkCompiling()
        {
            //create new work item, start work and return
            //the task representing the asynchronous work item
            return new WorkItemArduinoCompiling().DoWork(client, syntaxHighlightingTextBoxSourceCode.Text);
        }

        private object setResultUploading(string p)
        {
            setFilterOfResultString(richTextBoxOutputUpload, p);
            //this.richTextBoxOutputUpload.Text = p;
            richTextBoxOutputUpload.ScrollToCaret();
            EnableToolStripButton(toolStripButtonUpload, true);
            stopProgressBar();

            return true;
        }

        private Task<string> StartBackgroundWorkUploading()
        {
            //create new work item, start work and return
            //the task representing the asynchronous work item
            return new WorkItemArduinoUploading().DoWork(client);
        }

        private void EnableToolStripButton(ToolStripButton obj, bool bEnable)
        {
            if (toolStrip1.InvokeRequired)
            {
                toolStrip1.BeginInvoke((MethodInvoker)delegate
                {
                    obj.Enabled = bEnable;
                });
            }
            else
            {
                obj.Enabled = bEnable;
            }
        }

        private void startProgressBar()
        {
            if (toolStrip1.InvokeRequired)
            {
                toolStrip1.BeginInvoke((MethodInvoker)delegate
                {
                    toolStripProgressBar.Style = ProgressBarStyle.Marquee;
                    toolStrip1.Refresh();
                });
            }
            else
            {
                toolStripProgressBar.Style = ProgressBarStyle.Marquee;
                toolStrip1.Refresh();
            }
        }

        private void stopProgressBar()
        {
            if (toolStrip1.InvokeRequired)
            {
                toolStrip1.BeginInvoke((MethodInvoker)delegate
                {
                    toolStripProgressBar.Style = ProgressBarStyle.Blocks;
                    toolStrip1.Refresh();
                });
            }
            else
            {
                toolStripProgressBar.Style = ProgressBarStyle.Blocks;
                toolStrip1.Refresh();
            }
        }

        private object setResultWebControlSetSource(string p)
        {
            stopProgressBar();

            return true;
        }

        private Task<string> StartBackgroundWorkWebControlSetSource(string ws)
        {
            //create new work item, start work and return
            //the task representing the asynchronous work item
            return new WorkItemWebControlSetSource().DoWork(webControl1, ws);
        }

        private Task<string> StartBackgroundWorkSqlQuery()
        {
            //create new work item, start work and return
            //the task representing the asynchronous work item
            System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
            reqparm.Add("labID", lastLabID);
            reqparm.Add("devID", lastDevID);
            reqparm.Add("scode", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(syntaxHighlightingTextBoxSourceCode.Text)));
            return new WorkItemSqlQuery().DoWork("http://labpp.org/myIP/setdevsourcecode.php", reqparm);
        }

        private object setResultSqlQuery(string p)
        {
            stopProgressBar();
            //richTextBoxOutputCompile.Text = p;
            if (p=="1")
                SystemSounds.Beep.Play();
            else
                SystemSounds.Exclamation.Play();
            return true;
        }

        private Task<string> StartBackgroundWorkSqlQueryUser()
        {
            //create new work item, start work and return
            //the task representing the asynchronous work item
            System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
            reqparm.Add("labID", lastLabID);
            reqparm.Add("devID", lastDevID);
            reqparm.Add("uname", loginName);
            reqparm.Add("scode", Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(syntaxHighlightingTextBoxSourceCode.Text)));
            return new WorkItemSqlQuery().DoWork("http://labpp.org/myIP/setusersourcecode.php", reqparm);
        }

        private object setResultSqlQueryUser(string p)
        {
            stopProgressBar();
            //richTextBoxOutputCompile.Text = p;
            if (p == "1")
                SystemSounds.Beep.Play();
            else
                SystemSounds.Exclamation.Play();
            return true;
        }

        private Task<string> StartBackgroundWorkSqlQueryGet()
        {
            //create new work item, start work and return
            //the task representing the asynchronous work item
            System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
            reqparm.Add("labID", lastLabID);
            reqparm.Add("devID", lastDevID);
            return new WorkItemSqlQuery().DoWorkGet("http://labpp.org/myIP/getdevsourcecode.php", reqparm);
        }

        private object setResultSqlGetQuery(string p)
        {
            stopProgressBar();
            syntaxHighlightingTextBoxSourceCode.Text = p;
            this.Width = this.Width - 15;
            this.Width = this.Width + 15;
            this.toolStripButtonSaveSource.Enabled = true;
            SystemSounds.Beep.Play();
            return true;
        }

        private Task<string> StartBackgroundWorkSqlQueryGetUser()
        {
            //create new work item, start work and return
            //the task representing the asynchronous work item
            System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
            reqparm.Add("labID", lastLabID);
            reqparm.Add("devID", lastDevID);
            reqparm.Add("uname", loginName);
            return new WorkItemSqlQuery().DoWorkGet("http://labpp.org/myIP/getusersourcecode.php", reqparm);
        }

        private object setResultSqlGetUserQuery(string p)
        {
            stopProgressBar();
            syntaxHighlightingTextBoxSourceCode.Text = p;
            this.Width = this.Width + 15;
            this.Width = this.Width - 15;
            SystemSounds.Beep.Play();
            return true;
        }

        private void getDBSerialData()
        {
            isStreamSerialData = true;
            new Thread(delegate()
            {
                System.Diagnostics.Stopwatch swSerialDataRate = new System.Diagnostics.Stopwatch();
                swSerialDataRate.Start();
                while (isStreamSerialData)
                {
                    if (swSerialDataRate.Elapsed.TotalMilliseconds >= 1500)
                    {
                        swSerialDataRate.Restart();
                        string responseBody = "";
                        using (WebClient wclient = new WebClient())
                        {
                            string q = "";
                            if (sdDate != null)
                            {
                                q = "select ID, date, time, content from lpp_serialdata_log where (labID=" + lastLabID + " And date >= '" + sdDate + "' AND time > '" + sdTime + "') order by ID asc";
                            }
                            else
                            {
                                q = "select ID, date, time, content from lpp_serialdata_log where (labID=" + lastLabID + ") order by ID desc limit 3";
                            }
                            q = EncryptRJ256(sKy, sIV, q);
                            System.Collections.Specialized.NameValueCollection reqparm = new System.Collections.Specialized.NameValueCollection();
                            reqparm.Add("q", q);
                            byte[] responseBytes = wclient.UploadValues("http://labpp.org/get/new/sd/", "POST", reqparm);
                            responseBody = Encoding.UTF8.GetString(responseBytes);
                            responseBody = DecryptRJ256(sKy, sIV, responseBody);
                        }
                        List<serialdataResponse> allContents = new JavaScriptSerializer().Deserialize<List<serialdataResponse>>(responseBody);
                        string suffix = "\n";
                        string serialdatastr = " ";
                        foreach (var item in allContents)
                        {
                            if (item.first != "")
                            {
                                sdDate = item.first;
                                sdTime = item.second;
                                if (serialdatastr.IndexOf(suffix, serialdatastr.Length - suffix.Length) != -1)
                                {
                                    serialdatastr += item.first + " (" + item.second + "):\r\n" + Base64ToString(item.third);
                                    AppendText(richTextBoxSerial, Color.Yellow, item.first + " (" + item.second + "):\r\n");
                                    AppendText(richTextBoxSerial, Color.Green, Base64ToString(item.third));
                                }
                                else
                                {
                                    AppendText(richTextBoxSerial, Color.Green, Base64ToString(item.third));
                                    serialdatastr += Base64ToString(item.third);
                                }
                            }
                        }

                        if (richTextBoxSerial.InvokeRequired)
                        {
                            richTextBoxSerial.BeginInvoke((MethodInvoker)delegate
                            {
                                if (richTextBoxSerial.Text.Length > 2000)
                                {
                                    richTextBoxSerial.Text = "";
                                    //richTextBoxSerial.Select(1, 500); richTextBoxSerial.SelectedText = "";
                                }
                                //richTextBoxSerial.Text = serialdatastr;
                            });
                        }
                        else
                        {
                            if (richTextBoxSerial.Text.Length > 2000)
                            {
                                richTextBoxSerial.Text = "";
                                //richTextBoxSerial.Select(1, 500); richTextBoxSerial.SelectedText = "";
                            }
                            //richTextBoxSerial.Text = serialdatastr;
                        }
                    }
                }
            }).Start();
        }

        #endregion

        #region Events

        private void toolStripButtonOpenSerial_Click(object sender, EventArgs e)
        {
            if (buttonSendSerialCommand.Enabled)
            {
                isStreamSerialData = false;
                timerRetreiveSerialData.Stop();
                timerRetreiveSerialData.Elapsed -= timerRetreiveSerialData_Tick;
                buttonSendSerialCommand.Enabled = false;
                textBoxSerialCommand.Enabled = false;
                return;
            }
            toolStripButtonCompile.Enabled = true;
            buttonSendSerialCommand.Enabled = true;
            textBoxSerialCommand.Enabled = true;
            tabControl.SelectedTab = tabPageSerial;

            timerRetreiveSerialData = new System.Timers.Timer(1000);
            timerRetreiveSerialData.Elapsed += new ElapsedEventHandler(timerRetreiveSerialData_Tick);
            timerRetreiveSerialData.Start();

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
            this.Text = string.Format("{0} - {1}", lastLab, "Lab++ IDE");

            //startArduino();

        }

        private void timerGetUserList_Tick(object sender, EventArgs e)
        {
            GetArduinoUserList(loginName);

            stopProgressBar();
            timerGetUserList.Start();
        }

        private void formArduino_Shown(object sender, EventArgs e)
        {
            timerStart.Enabled = true;
        }

        private void formArduino_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (timerRetreiveSerialData != null)
                {
                    timerRetreiveSerialData.Close();
                    timerRetreiveSerialData.Dispose();
                }
                if (!(client == null))
                {
                    client.stop_arduino(loginName);
                }

                System.Threading.Thread.Sleep(1000);
                if (!(client == null))
                {
                    client.Close();
                }

                this.Dispose();
            }
            catch (Exception ex)
            {
                if (timerRetreiveSerialData != null)
                {
                    timerRetreiveSerialData.Close();
                    timerRetreiveSerialData.Dispose();
                }
                if (!(client == null))
                {
                    client.Abort();
                }
            }
        }

        private void buttonSendSerialCommand_Click(object sender, EventArgs e)
        {
            if (textBoxSerialCommand.Text.Trim() == "")
                return;

            DateTime dt = DateTime.Now;
            if (!(client == null))
            {
                client.sendSerialData(loginName, textBoxSerialCommand.Text, ref dt);
            }
            //AppendText(richTextBoxSerial, Color.White, string.Format("Sent on {0} : ", dt.ToLongTimeString()));
            AppendText(richTextBoxSerial, Color.Red, textBoxSerialCommand.Text + Environment.NewLine);
            textBoxSerialCommand.Text = "";
        }

        private void timerRetreiveSerialData_Tick(object sender, EventArgs e)
        {
            timerRetreiveSerialData.Stop();
            getDBSerialData();
            return;

            if (client == null)
                return;
            try
            {
                Application.DoEvents();
                var t3 = Task.Factory.StartNew(() =>
                {
                    string newline = client.getNewSerialData(loginName, ref lastDateTime);
                    if (newline == "Null" || newline == null)
                        return;

                    newline = newline.Replace("§§§", "‰");
                    string[] lines = newline.Split("‰".ToCharArray());
                    for (int i = 0; i < lines.Length; i++)
                    {
                        if (lines[i] != "")
                        {
                            newline = lines[i].Replace("@@@", "‰");
                            string[] parts = newline.Split("‰".ToCharArray());
                            //AppendText(richTextBoxSerial, Color.White, string.Format("{0} : {1}", (DateTime.Parse((parts[0].Replace("[", "")).Replace("]", ""))).ToLongTimeString(), Environment.NewLine));

                            if (richTextBoxSerial.InvokeRequired)
                            {
                                richTextBoxSerial.BeginInvoke((MethodInvoker)delegate
                                {
                                    if (richTextBoxSerial.Text.Length > 2000)
                                    {
                                        richTextBoxSerial.Text = "";
                                        //richTextBoxSerial.Select(1, 500); richTextBoxSerial.SelectedText = "";
                                    }
                                });
                            }
                            else
                            {
                                if (richTextBoxSerial.Text.Length > 2000)
                                {
                                    richTextBoxSerial.Text = "";
                                    //richTextBoxSerial.Select(1, 500); richTextBoxSerial.SelectedText = "";
                                }
                            }
                            AppendText(richTextBoxSerial, Color.Green, parts[2]);
                            //richTextBoxSerial.Text = (DateTime.Parse((parts[0].Replace("[", "")).Replace("]", ""))).ToLongTimeString() + 
                            //    " : " + Environment.NewLine + parts[2] + 
                            //    Environment.NewLine + richTextBoxSerial.Text;

                        }
                    }
                });
                t3.Wait();
            }
            catch
            {
                return;
            }
        }
        
        private void textBoxSerialCommand_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) //Enter key ---- (char)10: Ctrl+Enter key
            {
                if (textBoxSerialCommand.Enabled)
                    buttonSendSerialCommand_Click(null, null);
            }
        }

        private void toolStripButtonCompile_Click(object sender, EventArgs e)
        {
            startProgressBar();
            EnableToolStripButton(toolStripButtonCompile, false);
            EnableToolStripButton(toolStripButtonUpload, false);
            tabControl.SelectedTab = tabPageOutputCompile;
            richTextBoxOutputCompile.Text = "Please wait . . .";

            /*new Thread(delegate()
            {
                try
                {
                    if (richTextBoxOutputCompile.InvokeRequired)
                    {
                        richTextBoxOutputCompile.BeginInvoke((MethodInvoker)delegate
                        {
                            richTextBoxOutputCompile.Text = client.arduinoCompiling(syntaxHighlightingTextBoxSourceCode.Text);
                        });
                    }
                    else
                    {
                        richTextBoxOutputCompile.Text = client.arduinoCompiling(syntaxHighlightingTextBoxSourceCode.Text);
                    }
                }
                catch (System.ServiceModel.CommunicationException ex)
                {
                    stopProgressBar();
                    EnableToolStripButton(toolStripButtonCompile, true);
                    return;

                }

                stopProgressBar();
                EnableToolStripButton(toolStripButtonCompile, true);
            }).Start();*/

            /*ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    if (richTextBoxOutputCompile.InvokeRequired)
                    {
                        richTextBoxOutputCompile.BeginInvoke((MethodInvoker)delegate
                        {
                            richTextBoxOutputCompile.Text = client.arduinoCompiling(syntaxHighlightingTextBoxSourceCode.Text);
                        });
                    }
                    else
                    {
                        richTextBoxOutputCompile.Text = client.arduinoCompiling(syntaxHighlightingTextBoxSourceCode.Text);
                    }
                }
                catch (System.ServiceModel.CommunicationException ex)
                {
                    stopProgressBar();
                    EnableToolStripButton(toolStripButtonCompile, true);
                    return;

                }

                stopProgressBar();
                EnableToolStripButton(toolStripButtonCompile, true);
            }, null);
            richTextBoxOutputCompile.ScrollToCaret();
            */

            this.StartBackgroundWorkCompiling()
                    .ContinueWith((t) => this.setResultCompiling(t.Result), TaskScheduler.FromCurrentSynchronizationContext()); ;

        }

        private void toolStripButtonUpload_Click(object sender, EventArgs e)
        {
            startProgressBar();
            if (timerRetreiveSerialData != null)
            {
                timerRetreiveSerialData.Stop();
                timerRetreiveSerialData.Elapsed -= timerRetreiveSerialData_Tick;
            }
            buttonSendSerialCommand.Enabled = false;
            textBoxSerialCommand.Enabled = false;
            EnableToolStripButton(toolStripButtonUpload, false);
            tabControl.SelectedTab = tabPageOutputUpload;
            richTextBoxOutputUpload.Text = "Please wait . . .";

            /*new Thread(delegate()
            {
                try
                {
                    if (richTextBoxOutputUpload.InvokeRequired)
                    {
                        richTextBoxOutputUpload.BeginInvoke((MethodInvoker)delegate
                        {
                            richTextBoxOutputUpload.Text = client.arduinoUploading();
                        });
                    }
                    else
                    {
                        richTextBoxOutputUpload.Text = client.arduinoUploading();
                    }
                }
                catch (System.ServiceModel.CommunicationException ex)
                {
                    timerRetreiveSerialData.Start();
                    stopProgressBar();
                    EnableToolStripButton(toolStripButtonUpload, true);
                    return;
                }

                stopProgressBar();
                EnableToolStripButton(toolStripButtonUpload, true);
                timerRetreiveSerialData.Start();
            }).Start();*/

            /*ThreadPool.QueueUserWorkItem(delegate
            {
                try
                {
                    if (richTextBoxOutputUpload.InvokeRequired)
                    {
                        richTextBoxOutputUpload.BeginInvoke((MethodInvoker)delegate
                        {
                            richTextBoxOutputUpload.Text = client.arduinoUploading();
                        });
                    }
                    else
                    {
                        richTextBoxOutputUpload.Text = client.arduinoUploading();
                    }
                }
                catch (System.ServiceModel.CommunicationException ex)
                {
                    //timerRetreiveSerialData.Start();
                    stopProgressBar();
                    EnableToolStripButton(toolStripButtonUpload, true);
                    return;
                }

                stopProgressBar();
                EnableToolStripButton(toolStripButtonUpload, true);
                //timerRetreiveSerialData.Start();
            }, null);
            */

            this.StartBackgroundWorkUploading()
                    .ContinueWith((t) => this.setResultUploading(t.Result), TaskScheduler.FromCurrentSynchronizationContext());

            this.StartBackgroundWorkSqlQuery()
                    .ContinueWith((t) => this.setResultSqlQuery(t.Result), TaskScheduler.FromCurrentSynchronizationContext());

        }

        private void formArduino_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.F12)
            //    splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
            if (e.Control && e.KeyCode == Keys.D1)
            {
                buttonUserList_Click(null, null);
            }
            else if (e.Control && e.KeyCode == Keys.D2)
            {
                buttonHelp_Click(null, null);
            }
            else if (e.Control && e.KeyCode == Keys.D3)
            {
                buttonTabControl_Click(null, null);
            }
            else if (e.Control && e.KeyCode == Keys.Up)
            {
                toolStripButtonZoomIn_Click(null, null);
            }
            else if (e.Control && e.KeyCode == Keys.Down)
            {
                toolStripButtonZoomOut_Click(null, null);
            }
        }

        private void syntaxHighlightingTextBoxSourceCode_MouseUp(object sender, MouseEventArgs e)
        {
            /*if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {   
                ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();
                MenuItem menuItem = new MenuItem("Cut           Ctrl+X");
                menuItem.Click += new EventHandler(CutAction);
                contextMenu.MenuItems.Add(menuItem);
                menuItem = new MenuItem("Copy         Ctrl+C");
                menuItem.Click += new EventHandler(CopyAction);
                contextMenu.MenuItems.Add(menuItem);
                menuItem = new MenuItem("Paste        Ctrl+P");
                menuItem.Click += new EventHandler(PasteAction);
                contextMenu.MenuItems.Add(menuItem);
                menuItem = new MenuItem("Delete       Del");
                menuItem.Click += new EventHandler(DeleteAction);
                contextMenu.MenuItems.Add(menuItem);
                menuItem = new MenuItem("-");
                contextMenu.MenuItems.Add(menuItem);
                menuItem = new MenuItem("Select All   Ctrl+A");
                menuItem.Click += new EventHandler(SelectAllAction);
                contextMenu.MenuItems.Add(menuItem);

                syntaxHighlightingTextBoxSourceCode.ContextMenu = contextMenu;
            }*/
        }

        void UndoAction(object sender, EventArgs e)
        {
            if (syntaxHighlightingTextBoxSourceCode.CanUndo)
            {
                syntaxHighlightingTextBoxSourceCode.Undo();
            }
        }

        void RedoAction(object sender, EventArgs e)
        {
            if (syntaxHighlightingTextBoxSourceCode.CanRedo)
            {
                syntaxHighlightingTextBoxSourceCode.Redo();
            }
        }

        void CutAction(object sender, EventArgs e)
        {
            syntaxHighlightingTextBoxSourceCode.Cut();
        }

        void CopyAction(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, syntaxHighlightingTextBoxSourceCode.SelectedText);
        }

        void PasteAction(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText(TextDataFormat.Text))
            {
                syntaxHighlightingTextBoxSourceCode.Paste();
            }
        }

        void DeleteAction(object sender, EventArgs e)
        {
            syntaxHighlightingTextBoxSourceCode.SelectedText = "";
        }

        void SelectAllAction(object sender, EventArgs e)
        {
            syntaxHighlightingTextBoxSourceCode.SelectAll();
        }

        private void buttonUserList_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            startProgressBar();
            splitContainer4.Panel2Collapsed = !splitContainer4.Panel2Collapsed;
            setDelay(null, 2000);
            if (splitContainer4.Panel2Collapsed)
                webControl1.Source = null;
            else
            {
                webControl1.Source = new Uri("http://labpp.org/bootstrap/7/");
                //this.StartBackgroundWorkWebControlSetSource("http://labpp.org/bootstrap/7/")
                //        .ContinueWith((t) => this.setResultWebControlSetSource(t.Result), TaskScheduler.FromCurrentSynchronizationContext());
                setDelay(null, 2000);
            }
        }

        private void buttonTabControl_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel2Collapsed = !splitContainer2.Panel2Collapsed;
        }

        private void verticalLabelUserList_Click(object sender, EventArgs e)
        {
            splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
        }

        private void verticalLabelHelp_Click(object sender, EventArgs e)
        {
            splitContainer4.Panel2Collapsed = !splitContainer4.Panel2Collapsed;
        }

        private void verticalLabelOutputs_Click(object sender, EventArgs e)
        {
            splitContainer2.Panel2Collapsed = !splitContainer2.Panel2Collapsed;
        }

        private void buttonUserList_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(Brushes.Gainsboro, ((Button)sender).ClientRectangle);
            string txt = buttonUserList.Text;
            Font f = new Font("Lucida Console", 8, FontStyle.Regular);
            SizeF szF = g.MeasureString(txt, f);
            g.TranslateTransform((float)((Button)sender).ClientRectangle.Width / (float)2 + szF.Height / (float)2, (float)((Button)sender).ClientRectangle.Height / (float)2 - (float)szF.Width / (float)2);
            g.RotateTransform(90);
            g.DrawString(txt, f, Brushes.Black, 0, 0);
            f.Dispose();
        }

        private void buttonHelp_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(Brushes.Gainsboro, ((Button)sender).ClientRectangle);
            string txt = buttonHelp.Text;
            Font f = new Font("Lucida Console", 8);
            SizeF szF = g.MeasureString(txt, f);
            g.TranslateTransform((float)((Button)sender).ClientRectangle.Width / (float)2 + szF.Height / (float)2, (float)((Button)sender).ClientRectangle.Height / (float)2 - (float)szF.Width / (float)2);
            g.RotateTransform(90);
            g.DrawString(txt, f, Brushes.Black, 0, 0);
            f.Dispose();
        }

        private void buttonTabControl_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(Brushes.Gainsboro, ((Button)sender).ClientRectangle);
            string txt = buttonTabControl.Text;
            Font f = new Font("Lucida Console", 8);
            SizeF szF = g.MeasureString(txt, f);
            g.TranslateTransform((float)((Button)sender).ClientRectangle.Width / (float)2 + szF.Height / (float)2, (float)((Button)sender).ClientRectangle.Height / (float)2 - (float)szF.Width / (float)2);
            g.RotateTransform(90);
            g.DrawString(txt, f, Brushes.Black, 0, 0);
            f.Dispose();
        }

        private void toolStripButtonZoomOut_Click(object sender, EventArgs e)
        {
            if (syntaxHighlightingTextBoxSourceCode.ZoomFactor <= 0.3f)
                return;

            syntaxHighlightingTextBoxSourceCode.ZoomFactor -= 0.1f;
        }

        private void toolStripButtonZoomIn_Click(object sender, EventArgs e)
        {
            if (syntaxHighlightingTextBoxSourceCode.ZoomFactor >= 3.0f)
                return;

            syntaxHighlightingTextBoxSourceCode.ZoomFactor += 0.1f;
        }

        private void syntaxHighlightingTextBoxSourceCode_TextChanged(object sender, EventArgs e)
        {
            toolStripButtonUndo.Enabled = true;
            toolStripButtonRedo.Enabled = true;
        }

        private void toolStripButtonUndo_Click(object sender, EventArgs e)
        {
            UndoAction(null, null);
        }

        private void toolStripButtonRedo_Click(object sender, EventArgs e)
        {
            RedoAction(null, null);
        }

        private void Awesomium_Windows_Forms_WebControl_DocumentReady(object sender, Awesomium.Core.DocumentReadyEventArgs e)
        {
            stopProgressBar();

        }

        private void toolStripButtonSaveSource_Click(object sender, EventArgs e)
        {
            startProgressBar(); 
            
            this.StartBackgroundWorkSqlQueryUser()
                     .ContinueWith((t) => this.setResultSqlQueryUser(t.Result), TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void toolStripButtonOpenCurrentSourceCode_Click(object sender, EventArgs e)
        {
            this.StartBackgroundWorkSqlQueryGet()
                    .ContinueWith((t) => this.setResultSqlGetQuery(t.Result), TaskScheduler.FromCurrentSynchronizationContext());

        }

        private void toolStripButtonOpenSourceCode_Click(object sender, EventArgs e)
        {
            this.StartBackgroundWorkSqlQueryGetUser()
                    .ContinueWith((t) => this.setResultSqlGetUserQuery(t.Result), TaskScheduler.FromCurrentSynchronizationContext());

        }

        #endregion

    }

    #region Classes


    class serialdataResponse
    {
        public String first;
        public String second;
        public String third;
    }

    /// <summary>
    /// This item appears when any part of snippet text is typed
    /// </summary>
    class DeclarationSnippet : SnippetAutocompleteItem
    {
        public static string RegexSpecSymbolsPattern = @"[\^\$\[\]\(\)\.\\\*\+\|\?\{\}]";

        public DeclarationSnippet(string snippet)
            : base(snippet)
        {
        }

        public override CompareResult Compare(string fragmentText)
        {
            var pattern = Regex.Replace(fragmentText, RegexSpecSymbolsPattern, "\\$0");
            if (Regex.IsMatch(Text, "\\b" + pattern, RegexOptions.IgnoreCase))
                return CompareResult.Visible;
            return CompareResult.Hidden;
        }
    }

    /// <summary>
    /// Divides numbers and words: "123AND456" -> "123 AND 456"
    /// Or "i=2" -> "i = 2"
    /// </summary>
    class InsertSpaceSnippet : AutocompleteItem
    {
        string pattern;

        public InsertSpaceSnippet(string pattern)
            : base("")
        {
            this.pattern = pattern;
        }

        public InsertSpaceSnippet()
            : this(@"^(\d+)([a-zA-Z_]+)(\d*)$")
        {
        }

        public override CompareResult Compare(string fragmentText)
        {
            if (Regex.IsMatch(fragmentText, pattern))
            {
                Text = InsertSpaces(fragmentText);
                if (Text != fragmentText)
                    return CompareResult.Visible;
            }
            return CompareResult.Hidden;
        }

        public string InsertSpaces(string fragment)
        {
            var m = Regex.Match(fragment, pattern);
            if (m.Groups[1].Value == "" && m.Groups[3].Value == "")
                return fragment;
            return (m.Groups[1].Value + " " + m.Groups[2].Value + " " + m.Groups[3].Value).Trim();
        }

        public override string ToolTipTitle
        {
            get
            {
                return Text;
            }
        }
    }

    /// <summary>
    /// Inerts line break after '}'
    /// </summary>
    class InsertEnterSnippet : AutocompleteItem
    {
        int enterPlace = 0;

        public InsertEnterSnippet()
            : base("[Line break]")
        {
        }

        public override CompareResult Compare(string fragmentText)
        {
            var tb = Parent.TargetControlWrapper;

            var text = tb.Text;
            for (int i = Parent.Fragment.Start - 1; i >= 0; i--)
            {
                if (text[i] == '\n')
                    break;
                if (text[i] == '}')
                {
                    enterPlace = i;
                    return CompareResult.Visible;
                }
            }

            return CompareResult.Hidden;
        }

        public override string GetTextForReplace()
        {
            var tb = Parent.TargetControlWrapper;

            //insert line break
            tb.SelectionStart = enterPlace + 1;
            tb.SelectedText = "\n";
            Parent.Fragment.Start += 1;
            Parent.Fragment.End += 1;
            return Parent.Fragment.Text;
        }

        public override string ToolTipTitle
        {
            get
            {
                return "Insert line break after '}'";
            }
        }
    }

    #endregion
}

namespace WebCamWindowsClient
{
    partial class formControlpanel
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formControlpanel));
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.comboBoxCameras = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.comboBoxLabs = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonAddCamera = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuTray = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemShow = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemExitApp = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.exListBoxLabs = new WebCamWindowsClient.exListBox();
            this.exListBoxCameras = new WebCamWindowsClient.exListBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabelDate = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripStatusLabelTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabelLogin = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.timerDateTime = new System.Windows.Forms.Timer(this.components);
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem6 = new System.Windows.Forms.MenuItem();
            this.menuItemConnectToServer = new System.Windows.Forms.MenuItem();
            this.menuItem7 = new System.Windows.Forms.MenuItem();
            this.menuItemClose = new System.Windows.Forms.MenuItem();
            this.menuItemCloseAll = new System.Windows.Forms.MenuItem();
            this.menuItem10 = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuItemOptions = new System.Windows.Forms.MenuItem();
            this.menuItemTutorials = new System.Windows.Forms.MenuItem();
            this.menuItemWindow = new System.Windows.Forms.MenuItem();
            this.menuItemShowLists = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.menuItemArrangeIcons = new System.Windows.Forms.MenuItem();
            this.menuItemCascade = new System.Windows.Forms.MenuItem();
            this.menuItemArrangeHorizontal = new System.Windows.Forms.MenuItem();
            this.menuItemArrangeVertical = new System.Windows.Forms.MenuItem();
            this.menuItemMaximizeAll = new System.Windows.Forms.MenuItem();
            this.menuItemMinimizeAll = new System.Windows.Forms.MenuItem();
            this.menuItemHelp = new System.Windows.Forms.MenuItem();
            this.menuItemHelpOnWebsite = new System.Windows.Forms.MenuItem();
            this.menuItemWelcomePanel = new System.Windows.Forms.MenuItem();
            this.menuItem4 = new System.Windows.Forms.MenuItem();
            this.menuItemCheckForUpdate = new System.Windows.Forms.MenuItem();
            this.menuItemContactUs = new System.Windows.Forms.MenuItem();
            this.menuItemSupportLiveChat = new System.Windows.Forms.MenuItem();
            this.menuItem9 = new System.Windows.Forms.MenuItem();
            this.menuItemAbout = new System.Windows.Forms.MenuItem();
            this.listPanelCameras = new WebCamWindowsClient.ListPanel();
            this.listPanelLabs = new WebCamWindowsClient.ListPanel();
            this.contextMenuTray.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(332, 114);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(72, 20);
            this.textBoxPort.TabIndex = 10;
            this.textBoxPort.Text = "5469";
            this.textBoxPort.Visible = false;
            this.textBoxPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPort_KeyPress);
            // 
            // comboBoxCameras
            // 
            this.comboBoxCameras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxCameras.FormattingEnabled = true;
            this.comboBoxCameras.Location = new System.Drawing.Point(332, 87);
            this.comboBoxCameras.Name = "comboBoxCameras";
            this.comboBoxCameras.Size = new System.Drawing.Size(274, 21);
            this.comboBoxCameras.TabIndex = 3;
            this.toolTip1.SetToolTip(this.comboBoxCameras, "Live Cameras");
            this.comboBoxCameras.Visible = false;
            this.comboBoxCameras.SelectedIndexChanged += new System.EventHandler(this.comboBoxCameras_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(298, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Lab :";
            this.label1.Visible = false;
            // 
            // buttonConnect
            // 
            this.buttonConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonConnect.Image = ((System.Drawing.Image)(resources.GetObject("buttonConnect.Image")));
            this.buttonConnect.Location = new System.Drawing.Point(879, 47);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(78, 61);
            this.buttonConnect.TabIndex = 9;
            this.toolTip1.SetToolTip(this.buttonConnect, "Get onLine \r\nLabs & Cameras");
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Visible = false;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // comboBoxLabs
            // 
            this.comboBoxLabs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLabs.FormattingEnabled = true;
            this.comboBoxLabs.Location = new System.Drawing.Point(332, 47);
            this.comboBoxLabs.Name = "comboBoxLabs";
            this.comboBoxLabs.Size = new System.Drawing.Size(274, 21);
            this.comboBoxLabs.TabIndex = 10;
            this.toolTip1.SetToolTip(this.comboBoxLabs, "Live Labs");
            this.comboBoxLabs.Visible = false;
            this.comboBoxLabs.SelectedIndexChanged += new System.EventHandler(this.comboBoxLabs_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(280, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Camera :";
            this.label2.Visible = false;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "avatar_female.png");
            this.imageList1.Images.SetKeyName(1, "avatar_male.png");
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Lab++.org Client Studio 2015";
            // 
            // buttonAddCamera
            // 
            this.buttonAddCamera.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAddCamera.Enabled = false;
            this.buttonAddCamera.Image = ((System.Drawing.Image)(resources.GetObject("buttonAddCamera.Image")));
            this.buttonAddCamera.Location = new System.Drawing.Point(781, 47);
            this.buttonAddCamera.Name = "buttonAddCamera";
            this.buttonAddCamera.Size = new System.Drawing.Size(78, 61);
            this.buttonAddCamera.TabIndex = 13;
            this.toolTip1.SetToolTip(this.buttonAddCamera, "Add Camera");
            this.buttonAddCamera.UseVisualStyleBackColor = true;
            this.buttonAddCamera.Visible = false;
            this.buttonAddCamera.Click += new System.EventHandler(this.buttonAddCamera_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "I am here!!!\r\nFor more info, please visit:\r\nhttp://labpp.org";
            this.notifyIcon.BalloonTipTitle = "Lab++.org Client Studio 2015";
            this.notifyIcon.ContextMenuStrip = this.contextMenuTray;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Lab++.org Client Studio 2015";
            this.notifyIcon.Visible = true;
            // 
            // contextMenuTray
            // 
            this.contextMenuTray.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemConnect,
            this.toolStripMenuItemShow,
            this.toolStripMenuItem1,
            this.toolStripMenuItemExitApp});
            this.contextMenuTray.Name = "contextMenuTray";
            this.contextMenuTray.Size = new System.Drawing.Size(163, 76);
            // 
            // toolStripMenuItemConnect
            // 
            this.toolStripMenuItemConnect.Name = "toolStripMenuItemConnect";
            this.toolStripMenuItemConnect.Size = new System.Drawing.Size(162, 22);
            this.toolStripMenuItemConnect.Text = "Connect to Server";
            this.toolStripMenuItemConnect.Click += new System.EventHandler(this.toolStripMenuItemConnect_Click);
            // 
            // toolStripMenuItemShow
            // 
            this.toolStripMenuItemShow.Name = "toolStripMenuItemShow";
            this.toolStripMenuItemShow.Size = new System.Drawing.Size(162, 22);
            this.toolStripMenuItemShow.Text = "Show";
            this.toolStripMenuItemShow.Click += new System.EventHandler(this.toolStripMenuItemShow_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(159, 6);
            // 
            // toolStripMenuItemExitApp
            // 
            this.toolStripMenuItemExitApp.Name = "toolStripMenuItemExitApp";
            this.toolStripMenuItemExitApp.Size = new System.Drawing.Size(162, 22);
            this.toolStripMenuItemExitApp.Text = "Exit";
            this.toolStripMenuItemExitApp.Click += new System.EventHandler(this.ExitAppToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.InsetDouble;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.exListBoxLabs, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.exListBoxCameras, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 58.39416F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 41.60584F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(274, 630);
            this.tableLayoutPanel1.TabIndex = 15;
            this.tableLayoutPanel1.Visible = false;
            // 
            // exListBoxLabs
            // 
            this.exListBoxLabs.BackColor = System.Drawing.Color.White;
            this.exListBoxLabs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.exListBoxLabs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exListBoxLabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exListBoxLabs.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.exListBoxLabs.FormattingEnabled = true;
            this.exListBoxLabs.ItemHeight = 66;
            this.exListBoxLabs.Location = new System.Drawing.Point(6, 6);
            this.exListBoxLabs.Name = "exListBoxLabs";
            this.exListBoxLabs.Size = new System.Drawing.Size(262, 356);
            this.exListBoxLabs.TabIndex = 2;
            this.exListBoxLabs.SelectedIndexChanged += new System.EventHandler(this.exListBoxLabs_SelectedIndexChanged);
            // 
            // exListBoxCameras
            // 
            this.exListBoxCameras.BackColor = System.Drawing.Color.White;
            this.exListBoxCameras.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.exListBoxCameras.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exListBoxCameras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exListBoxCameras.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.exListBoxCameras.FormattingEnabled = true;
            this.exListBoxCameras.ItemHeight = 66;
            this.exListBoxCameras.Location = new System.Drawing.Point(6, 371);
            this.exListBoxCameras.Name = "exListBoxCameras";
            this.exListBoxCameras.Size = new System.Drawing.Size(262, 253);
            this.exListBoxCameras.TabIndex = 3;
            this.exListBoxCameras.SelectedIndexChanged += new System.EventHandler(this.exListBoxCameras_SelectedIndexChanged);
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Transparent;
            this.statusStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelDate,
            this.toolStripStatusLabelTime,
            this.toolStripStatusLabelLogin,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(274, 606);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(690, 24);
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabelDate
            // 
            this.toolStripStatusLabelDate.Name = "toolStripStatusLabelDate";
            this.toolStripStatusLabelDate.Size = new System.Drawing.Size(46, 22);
            this.toolStripStatusLabelDate.Text = "Date";
            this.toolStripStatusLabelDate.ButtonClick += new System.EventHandler(this.toolStripStatusLabelDate_ButtonClick);
            // 
            // toolStripStatusLabelTime
            // 
            this.toolStripStatusLabelTime.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabelTime.Name = "toolStripStatusLabelTime";
            this.toolStripStatusLabelTime.Size = new System.Drawing.Size(33, 19);
            this.toolStripStatusLabelTime.Text = "Time";
            // 
            // toolStripStatusLabelLogin
            // 
            this.toolStripStatusLabelLogin.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.toolStripStatusLabelLogin.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedInner;
            this.toolStripStatusLabelLogin.Name = "toolStripStatusLabelLogin";
            this.toolStripStatusLabelLogin.Size = new System.Drawing.Size(71, 19);
            this.toolStripStatusLabelLogin.Text = "Login as . . .";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(209)))), ((int)(((byte)(220)))));
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(200, 18);
            this.toolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // timerDateTime
            // 
            this.timerDateTime.Enabled = true;
            this.timerDateTime.Interval = 1;
            this.timerDateTime.Tick += new System.EventHandler(this.timerDateTime_Tick);
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem6,
            this.menuItem1,
            this.menuItemWindow,
            this.menuItemHelp});
            this.mainMenu1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // menuItem6
            // 
            this.menuItem6.Index = 0;
            this.menuItem6.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemConnectToServer,
            this.menuItem7,
            this.menuItemClose,
            this.menuItemCloseAll,
            this.menuItem10,
            this.menuItemExit});
            this.menuItem6.Text = "File";
            // 
            // menuItemConnectToServer
            // 
            this.menuItemConnectToServer.Index = 0;
            this.menuItemConnectToServer.Shortcut = System.Windows.Forms.Shortcut.F2;
            this.menuItemConnectToServer.Text = "Connect to Server";
            this.menuItemConnectToServer.Click += new System.EventHandler(this.menuItemConnectToServer_Click);
            // 
            // menuItem7
            // 
            this.menuItem7.Index = 1;
            this.menuItem7.Text = "-";
            // 
            // menuItemClose
            // 
            this.menuItemClose.Index = 2;
            this.menuItemClose.Shortcut = System.Windows.Forms.Shortcut.CtrlF4;
            this.menuItemClose.Text = "Close";
            this.menuItemClose.Click += new System.EventHandler(this.menuItemClose_Click);
            // 
            // menuItemCloseAll
            // 
            this.menuItemCloseAll.Index = 3;
            this.menuItemCloseAll.Shortcut = System.Windows.Forms.Shortcut.CtrlShiftF4;
            this.menuItemCloseAll.Text = "Close All";
            this.menuItemCloseAll.Click += new System.EventHandler(this.menuItemCloseAll_Click);
            // 
            // menuItem10
            // 
            this.menuItem10.Index = 4;
            this.menuItem10.Text = "-";
            // 
            // menuItemExit
            // 
            this.menuItemExit.Index = 5;
            this.menuItemExit.Shortcut = System.Windows.Forms.Shortcut.AltF4;
            this.menuItemExit.Text = "Exit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 1;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemOptions,
            this.menuItemTutorials});
            this.menuItem1.Text = "Tools";
            // 
            // menuItemOptions
            // 
            this.menuItemOptions.Index = 0;
            this.menuItemOptions.Shortcut = System.Windows.Forms.Shortcut.F4;
            this.menuItemOptions.Text = "Options";
            this.menuItemOptions.Click += new System.EventHandler(this.menuItemOptions_Click);
            // 
            // menuItemTutorials
            // 
            this.menuItemTutorials.Index = 1;
            this.menuItemTutorials.Shortcut = System.Windows.Forms.Shortcut.F12;
            this.menuItemTutorials.Text = "Tutorials";
            this.menuItemTutorials.Click += new System.EventHandler(this.menuItemTutorials_Click);
            // 
            // menuItemWindow
            // 
            this.menuItemWindow.Index = 2;
            this.menuItemWindow.MdiList = true;
            this.menuItemWindow.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemShowLists,
            this.menuItem2,
            this.menuItemArrangeIcons,
            this.menuItemCascade,
            this.menuItemArrangeHorizontal,
            this.menuItemArrangeVertical,
            this.menuItemMaximizeAll,
            this.menuItemMinimizeAll});
            this.menuItemWindow.Text = "&Window";
            // 
            // menuItemShowLists
            // 
            this.menuItemShowLists.Index = 0;
            this.menuItemShowLists.Shortcut = System.Windows.Forms.Shortcut.F3;
            this.menuItemShowLists.Text = "Show/Hide Lists";
            this.menuItemShowLists.Click += new System.EventHandler(this.menuItemShowLists_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Index = 1;
            this.menuItem2.Text = "-";
            // 
            // menuItemArrangeIcons
            // 
            this.menuItemArrangeIcons.Index = 2;
            this.menuItemArrangeIcons.Text = "Arrange&Icons";
            this.menuItemArrangeIcons.Click += new System.EventHandler(this.menuItemArrangeIcons_Click);
            // 
            // menuItemCascade
            // 
            this.menuItemCascade.Index = 3;
            this.menuItemCascade.Text = "&Cascade";
            this.menuItemCascade.Click += new System.EventHandler(this.menuItemCascade_Click);
            // 
            // menuItemArrangeHorizontal
            // 
            this.menuItemArrangeHorizontal.Index = 4;
            this.menuItemArrangeHorizontal.Text = "Arrange &Horizontal";
            this.menuItemArrangeHorizontal.Click += new System.EventHandler(this.menuItemArrangeHorizontal_Click);
            // 
            // menuItemArrangeVertical
            // 
            this.menuItemArrangeVertical.Index = 5;
            this.menuItemArrangeVertical.Text = "Arrange &Vertical";
            this.menuItemArrangeVertical.Click += new System.EventHandler(this.menuItemArrangeVertical_Click);
            // 
            // menuItemMaximizeAll
            // 
            this.menuItemMaximizeAll.Index = 6;
            this.menuItemMaximizeAll.Text = "Ma&ximize all";
            this.menuItemMaximizeAll.Click += new System.EventHandler(this.menuItemMaximizeAll_Click);
            // 
            // menuItemMinimizeAll
            // 
            this.menuItemMinimizeAll.Index = 7;
            this.menuItemMinimizeAll.Text = "Mi&nimize all";
            this.menuItemMinimizeAll.Click += new System.EventHandler(this.menuItemMinimizeAll_Click);
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.Index = 3;
            this.menuItemHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemHelpOnWebsite,
            this.menuItemWelcomePanel,
            this.menuItem4,
            this.menuItemCheckForUpdate,
            this.menuItemContactUs,
            this.menuItemSupportLiveChat,
            this.menuItem9,
            this.menuItemAbout});
            this.menuItemHelp.Text = "Help";
            // 
            // menuItemHelpOnWebsite
            // 
            this.menuItemHelpOnWebsite.Index = 0;
            this.menuItemHelpOnWebsite.Shortcut = System.Windows.Forms.Shortcut.F1;
            this.menuItemHelpOnWebsite.Text = "Help on Website";
            this.menuItemHelpOnWebsite.Click += new System.EventHandler(this.menuItemHelpOnWebsite_Click);
            // 
            // menuItemWelcomePanel
            // 
            this.menuItemWelcomePanel.Index = 1;
            this.menuItemWelcomePanel.Text = "Welcome Panel";
            this.menuItemWelcomePanel.Click += new System.EventHandler(this.menuItemWelcomePanel_Click);
            // 
            // menuItem4
            // 
            this.menuItem4.Index = 2;
            this.menuItem4.Text = "-";
            // 
            // menuItemCheckForUpdate
            // 
            this.menuItemCheckForUpdate.Index = 3;
            this.menuItemCheckForUpdate.Text = "Check for Update";
            this.menuItemCheckForUpdate.Click += new System.EventHandler(this.menuItemCheckForUpdate_Click);
            // 
            // menuItemContactUs
            // 
            this.menuItemContactUs.Index = 4;
            this.menuItemContactUs.Text = "Contact Us";
            this.menuItemContactUs.Click += new System.EventHandler(this.menuItemContactUs_Click);
            // 
            // menuItemSupportLiveChat
            // 
            this.menuItemSupportLiveChat.Index = 5;
            this.menuItemSupportLiveChat.Text = "Support Live Chat";
            this.menuItemSupportLiveChat.Click += new System.EventHandler(this.menuItemSupportLiveChat_Click);
            // 
            // menuItem9
            // 
            this.menuItem9.Index = 6;
            this.menuItem9.Text = "-";
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Index = 7;
            this.menuItemAbout.Text = "About ...";
            this.menuItemAbout.Click += new System.EventHandler(this.menuItemAbout_Click);
            // 
            // listPanelCameras
            // 
            this.listPanelCameras.AutoScroll = true;
            this.listPanelCameras.BackColor = System.Drawing.Color.Transparent;
            this.listPanelCameras.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listPanelCameras.Location = new System.Drawing.Point(654, 160);
            this.listPanelCameras.Name = "listPanelCameras";
            this.listPanelCameras.SelectedIndex = 0;
            this.listPanelCameras.SelectedItem = null;
            this.listPanelCameras.Size = new System.Drawing.Size(262, 224);
            this.listPanelCameras.TabIndex = 19;
            this.listPanelCameras.Visible = false;
            // 
            // listPanelLabs
            // 
            this.listPanelLabs.AutoScroll = true;
            this.listPanelLabs.BackColor = System.Drawing.Color.Transparent;
            this.listPanelLabs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listPanelLabs.Location = new System.Drawing.Point(314, 160);
            this.listPanelLabs.Name = "listPanelLabs";
            this.listPanelLabs.SelectedIndex = 0;
            this.listPanelLabs.SelectedItem = null;
            this.listPanelLabs.Size = new System.Drawing.Size(262, 222);
            this.listPanelLabs.TabIndex = 18;
            this.listPanelLabs.Visible = false;
            // 
            // formControlpanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(964, 630);
            this.Controls.Add(this.listPanelCameras);
            this.Controls.Add(this.listPanelLabs);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxLabs);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.comboBoxCameras);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.buttonAddCamera);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Menu = this.mainMenu1;
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "formControlpanel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lab++ Client Studio 2015";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.formControlpanel_Shown);
            this.contextMenuTray.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxCameras;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.ComboBox comboBoxLabs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonAddCamera;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuTray;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemConnect;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExitApp;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelTime;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelLogin;
        private System.Windows.Forms.Timer timerDateTime;
        private System.Windows.Forms.ToolStripSplitButton toolStripStatusLabelDate;
        private WebCamWindowsClient.exListBox exListBoxLabs;
        private exListBox exListBoxCameras;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemShow;
        private ListPanel listPanelLabs;
        private ListPanel listPanelCameras;
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItemWindow;
        private System.Windows.Forms.MenuItem menuItemHelp;
        private System.Windows.Forms.MenuItem menuItemHelpOnWebsite;
        private System.Windows.Forms.MenuItem menuItemWelcomePanel;
        private System.Windows.Forms.MenuItem menuItem4;
        private System.Windows.Forms.MenuItem menuItemCheckForUpdate;
        private System.Windows.Forms.MenuItem menuItemContactUs;
        private System.Windows.Forms.MenuItem menuItemSupportLiveChat;
        private System.Windows.Forms.MenuItem menuItem9;
        private System.Windows.Forms.MenuItem menuItemAbout;
        private System.Windows.Forms.MenuItem menuItemShowLists;
        private System.Windows.Forms.MenuItem menuItem2;
        private System.Windows.Forms.MenuItem menuItemArrangeIcons;
        private System.Windows.Forms.MenuItem menuItemCascade;
        private System.Windows.Forms.MenuItem menuItemArrangeHorizontal;
        private System.Windows.Forms.MenuItem menuItemArrangeVertical;
        private System.Windows.Forms.MenuItem menuItemMaximizeAll;
        private System.Windows.Forms.MenuItem menuItemMinimizeAll;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem menuItemOptions;
        private System.Windows.Forms.MenuItem menuItemTutorials;
        private System.Windows.Forms.MenuItem menuItem6;
        private System.Windows.Forms.MenuItem menuItemConnectToServer;
        private System.Windows.Forms.MenuItem menuItem7;
        private System.Windows.Forms.MenuItem menuItemClose;
        private System.Windows.Forms.MenuItem menuItemCloseAll;
        private System.Windows.Forms.MenuItem menuItem10;
        private System.Windows.Forms.MenuItem menuItemExit;
    }
}


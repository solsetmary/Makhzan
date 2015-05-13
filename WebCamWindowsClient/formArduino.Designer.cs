namespace WebCamWindowsClient
{
    partial class formArduino
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formArduino));
            this.syntaxHighlightingTextBoxSourceCode = new UrielGuy.SyntaxHighlightingTextBox.SyntaxHighlightingTextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageSerial = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonSendSerialCommand = new System.Windows.Forms.Button();
            this.textBoxSerialCommand = new System.Windows.Forms.TextBox();
            this.richTextBoxSerial = new System.Windows.Forms.RichTextBox();
            this.tabPageOutputCompile = new System.Windows.Forms.TabPage();
            this.richTextBoxOutputCompile = new System.Windows.Forms.RichTextBox();
            this.tabPageOutputUpload = new System.Windows.Forms.TabPage();
            this.richTextBoxOutputUpload = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonOpenSerial = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonCompile = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonUpload = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonUndo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonZoomOut = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonZoomIn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonSaveSource = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOpenSourceCode = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOpenCurrentSourceCode = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSaveUploadOutput = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.timerStart = new System.Windows.Forms.Timer(this.components);
            this.timerGetUserList = new System.Windows.Forms.Timer(this.components);
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.exListBoxUsers = new WebCamWindowsClient.exListBox();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode = new WebCamWindowsClient.richTextBoxLineNumber();
            this.webControl1 = new Awesomium.Windows.Forms.WebControl(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonTabControl = new System.Windows.Forms.Button();
            this.buttonHelp = new System.Windows.Forms.Button();
            this.buttonUserList = new System.Windows.Forms.Button();
            this.webSessionProvider1 = new Awesomium.Windows.Forms.WebSessionProvider(this.components);
            this.autocompleteMenu1 = new AutocompleteMenuNS.AutocompleteMenu();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.tabControl.SuspendLayout();
            this.tabPageSerial.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tabPageOutputCompile.SuspendLayout();
            this.tabPageOutputUpload.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.SuspendLayout();
            // 
            // syntaxHighlightingTextBoxSourceCode
            // 
            this.syntaxHighlightingTextBoxSourceCode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.autocompleteMenu1.SetAutocompleteMenu(this.syntaxHighlightingTextBoxSourceCode, this.autocompleteMenu1);
            this.syntaxHighlightingTextBoxSourceCode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.syntaxHighlightingTextBoxSourceCode.CaseSensitive = false;
            this.syntaxHighlightingTextBoxSourceCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.syntaxHighlightingTextBoxSourceCode.EnableAutoDragDrop = true;
            this.syntaxHighlightingTextBoxSourceCode.FilterAutoComplete = false;
            this.syntaxHighlightingTextBoxSourceCode.Location = new System.Drawing.Point(19, 0);
            this.syntaxHighlightingTextBoxSourceCode.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.syntaxHighlightingTextBoxSourceCode.MaxUndoRedoSteps = 50;
            this.syntaxHighlightingTextBoxSourceCode.Name = "syntaxHighlightingTextBoxSourceCode";
            this.syntaxHighlightingTextBoxSourceCode.ShowSelectionMargin = true;
            this.syntaxHighlightingTextBoxSourceCode.Size = new System.Drawing.Size(590, 289);
            this.syntaxHighlightingTextBoxSourceCode.TabIndex = 0;
            this.syntaxHighlightingTextBoxSourceCode.Text = "";
            this.syntaxHighlightingTextBoxSourceCode.TextChanged += new System.EventHandler(this.syntaxHighlightingTextBoxSourceCode_TextChanged);
            this.syntaxHighlightingTextBoxSourceCode.MouseUp += new System.Windows.Forms.MouseEventHandler(this.syntaxHighlightingTextBoxSourceCode_MouseUp);
            // 
            // tabControl
            // 
            this.tabControl.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl.Controls.Add(this.tabPageSerial);
            this.tabControl.Controls.Add(this.tabPageOutputCompile);
            this.tabControl.Controls.Add(this.tabPageOutputUpload);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(808, 268);
            this.tabControl.TabIndex = 0;
            // 
            // tabPageSerial
            // 
            this.tabPageSerial.Controls.Add(this.splitContainer3);
            this.tabPageSerial.Location = new System.Drawing.Point(4, 4);
            this.tabPageSerial.Name = "tabPageSerial";
            this.tabPageSerial.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSerial.Size = new System.Drawing.Size(800, 242);
            this.tabPageSerial.TabIndex = 1;
            this.tabPageSerial.Text = "Serial";
            this.tabPageSerial.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.tableLayoutPanel4);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.richTextBoxSerial);
            this.splitContainer3.Size = new System.Drawing.Size(794, 236);
            this.splitContainer3.SplitterDistance = 31;
            this.splitContainer3.TabIndex = 1;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 83F));
            this.tableLayoutPanel4.Controls.Add(this.buttonSendSerialCommand, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.textBoxSerialCommand, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(794, 31);
            this.tableLayoutPanel4.TabIndex = 3;
            // 
            // buttonSendSerialCommand
            // 
            this.buttonSendSerialCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonSendSerialCommand.Enabled = false;
            this.buttonSendSerialCommand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSendSerialCommand.Location = new System.Drawing.Point(714, 3);
            this.buttonSendSerialCommand.Name = "buttonSendSerialCommand";
            this.buttonSendSerialCommand.Size = new System.Drawing.Size(77, 25);
            this.buttonSendSerialCommand.TabIndex = 1;
            this.buttonSendSerialCommand.Text = "&Send";
            this.buttonSendSerialCommand.UseVisualStyleBackColor = true;
            this.buttonSendSerialCommand.Click += new System.EventHandler(this.buttonSendSerialCommand_Click);
            // 
            // textBoxSerialCommand
            // 
            this.textBoxSerialCommand.AcceptsReturn = true;
            this.autocompleteMenu1.SetAutocompleteMenu(this.textBoxSerialCommand, null);
            this.textBoxSerialCommand.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxSerialCommand.Enabled = false;
            this.textBoxSerialCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSerialCommand.Location = new System.Drawing.Point(3, 3);
            this.textBoxSerialCommand.Multiline = true;
            this.textBoxSerialCommand.Name = "textBoxSerialCommand";
            this.textBoxSerialCommand.Size = new System.Drawing.Size(705, 25);
            this.textBoxSerialCommand.TabIndex = 0;
            this.textBoxSerialCommand.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxSerialCommand_KeyPress);
            // 
            // richTextBoxSerial
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.richTextBoxSerial, null);
            this.richTextBoxSerial.BackColor = System.Drawing.Color.Black;
            this.richTextBoxSerial.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxSerial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxSerial.ForeColor = System.Drawing.Color.Lime;
            this.richTextBoxSerial.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxSerial.Name = "richTextBoxSerial";
            this.richTextBoxSerial.ReadOnly = true;
            this.richTextBoxSerial.Size = new System.Drawing.Size(794, 201);
            this.richTextBoxSerial.TabIndex = 1;
            this.richTextBoxSerial.Text = "";
            // 
            // tabPageOutputCompile
            // 
            this.tabPageOutputCompile.Controls.Add(this.richTextBoxOutputCompile);
            this.tabPageOutputCompile.Location = new System.Drawing.Point(4, 4);
            this.tabPageOutputCompile.Name = "tabPageOutputCompile";
            this.tabPageOutputCompile.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOutputCompile.Size = new System.Drawing.Size(800, 242);
            this.tabPageOutputCompile.TabIndex = 0;
            this.tabPageOutputCompile.Text = "Compile";
            this.tabPageOutputCompile.UseVisualStyleBackColor = true;
            // 
            // richTextBoxOutputCompile
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.richTextBoxOutputCompile, null);
            this.richTextBoxOutputCompile.BackColor = System.Drawing.Color.Black;
            this.richTextBoxOutputCompile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxOutputCompile.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxOutputCompile.ForeColor = System.Drawing.Color.White;
            this.richTextBoxOutputCompile.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxOutputCompile.Name = "richTextBoxOutputCompile";
            this.richTextBoxOutputCompile.ReadOnly = true;
            this.richTextBoxOutputCompile.Size = new System.Drawing.Size(794, 236);
            this.richTextBoxOutputCompile.TabIndex = 0;
            this.richTextBoxOutputCompile.Text = "";
            // 
            // tabPageOutputUpload
            // 
            this.tabPageOutputUpload.Controls.Add(this.richTextBoxOutputUpload);
            this.tabPageOutputUpload.Location = new System.Drawing.Point(4, 4);
            this.tabPageOutputUpload.Name = "tabPageOutputUpload";
            this.tabPageOutputUpload.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageOutputUpload.Size = new System.Drawing.Size(800, 242);
            this.tabPageOutputUpload.TabIndex = 2;
            this.tabPageOutputUpload.Text = "Upload";
            this.tabPageOutputUpload.UseVisualStyleBackColor = true;
            // 
            // richTextBoxOutputUpload
            // 
            this.autocompleteMenu1.SetAutocompleteMenu(this.richTextBoxOutputUpload, null);
            this.richTextBoxOutputUpload.BackColor = System.Drawing.Color.Black;
            this.richTextBoxOutputUpload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxOutputUpload.Font = new System.Drawing.Font("Lucida Console", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBoxOutputUpload.ForeColor = System.Drawing.Color.White;
            this.richTextBoxOutputUpload.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxOutputUpload.Name = "richTextBoxOutputUpload";
            this.richTextBoxOutputUpload.ReadOnly = true;
            this.richTextBoxOutputUpload.Size = new System.Drawing.Size(794, 236);
            this.richTextBoxOutputUpload.TabIndex = 1;
            this.richTextBoxOutputUpload.Text = "";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOpenSerial,
            this.toolStripSeparator1,
            this.toolStripButtonCompile,
            this.toolStripButtonUpload,
            this.toolStripSeparator2,
            this.toolStripProgressBar,
            this.toolStripSeparator5,
            this.toolStripButtonUndo,
            this.toolStripButtonRedo,
            this.toolStripSeparator4,
            this.toolStripButtonZoomOut,
            this.toolStripButtonZoomIn,
            this.toolStripSeparator6,
            this.toolStripButtonSaveSource,
            this.toolStripButtonOpenSourceCode,
            this.toolStripButtonOpenCurrentSourceCode,
            this.toolStripButtonSaveUploadOutput,
            this.toolStripSeparator3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(808, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonOpenSerial
            // 
            this.toolStripButtonOpenSerial.Enabled = false;
            this.toolStripButtonOpenSerial.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpenSerial.Image")));
            this.toolStripButtonOpenSerial.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpenSerial.Name = "toolStripButtonOpenSerial";
            this.toolStripButtonOpenSerial.Size = new System.Drawing.Size(82, 22);
            this.toolStripButtonOpenSerial.Text = "Open Serial";
            this.toolStripButtonOpenSerial.Click += new System.EventHandler(this.toolStripButtonOpenSerial_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonCompile
            // 
            this.toolStripButtonCompile.Enabled = false;
            this.toolStripButtonCompile.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCompile.Image")));
            this.toolStripButtonCompile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCompile.Name = "toolStripButtonCompile";
            this.toolStripButtonCompile.Size = new System.Drawing.Size(64, 22);
            this.toolStripButtonCompile.Text = "Compile";
            this.toolStripButtonCompile.Click += new System.EventHandler(this.toolStripButtonCompile_Click);
            // 
            // toolStripButtonUpload
            // 
            this.toolStripButtonUpload.Enabled = false;
            this.toolStripButtonUpload.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUpload.Image")));
            this.toolStripButtonUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUpload.Name = "toolStripButtonUpload";
            this.toolStripButtonUpload.Size = new System.Drawing.Size(60, 22);
            this.toolStripButtonUpload.Text = "Upload";
            this.toolStripButtonUpload.Click += new System.EventHandler(this.toolStripButtonUpload_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripProgressBar
            // 
            this.toolStripProgressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.toolStripProgressBar.Name = "toolStripProgressBar";
            this.toolStripProgressBar.Size = new System.Drawing.Size(150, 22);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonUndo
            // 
            this.toolStripButtonUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonUndo.Enabled = false;
            this.toolStripButtonUndo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonUndo.Image")));
            this.toolStripButtonUndo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonUndo.Name = "toolStripButtonUndo";
            this.toolStripButtonUndo.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonUndo.Text = "Undo (Ctrl + Z)";
            this.toolStripButtonUndo.Click += new System.EventHandler(this.toolStripButtonUndo_Click);
            // 
            // toolStripButtonRedo
            // 
            this.toolStripButtonRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonRedo.Enabled = false;
            this.toolStripButtonRedo.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRedo.Image")));
            this.toolStripButtonRedo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonRedo.Name = "toolStripButtonRedo";
            this.toolStripButtonRedo.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonRedo.Text = "Redo (Ctrl + Y)";
            this.toolStripButtonRedo.Click += new System.EventHandler(this.toolStripButtonRedo_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonZoomOut
            // 
            this.toolStripButtonZoomOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonZoomOut.Enabled = false;
            this.toolStripButtonZoomOut.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonZoomOut.Image")));
            this.toolStripButtonZoomOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonZoomOut.Name = "toolStripButtonZoomOut";
            this.toolStripButtonZoomOut.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonZoomOut.Text = "Zoom Out (Ctrl + Down)";
            this.toolStripButtonZoomOut.Click += new System.EventHandler(this.toolStripButtonZoomOut_Click);
            // 
            // toolStripButtonZoomIn
            // 
            this.toolStripButtonZoomIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonZoomIn.Enabled = false;
            this.toolStripButtonZoomIn.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonZoomIn.Image")));
            this.toolStripButtonZoomIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonZoomIn.Name = "toolStripButtonZoomIn";
            this.toolStripButtonZoomIn.Size = new System.Drawing.Size(23, 22);
            this.toolStripButtonZoomIn.Text = "Zoom In (Ctrl + Up)";
            this.toolStripButtonZoomIn.Click += new System.EventHandler(this.toolStripButtonZoomIn_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButtonSaveSource
            // 
            this.toolStripButtonSaveSource.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSaveSource.Image")));
            this.toolStripButtonSaveSource.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveSource.Name = "toolStripButtonSaveSource";
            this.toolStripButtonSaveSource.Size = new System.Drawing.Size(51, 22);
            this.toolStripButtonSaveSource.Text = "Save";
            this.toolStripButtonSaveSource.ToolTipText = "Save my source code";
            this.toolStripButtonSaveSource.Click += new System.EventHandler(this.toolStripButtonSaveSource_Click);
            // 
            // toolStripButtonOpenSourceCode
            // 
            this.toolStripButtonOpenSourceCode.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpenSourceCode.Image")));
            this.toolStripButtonOpenSourceCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpenSourceCode.Name = "toolStripButtonOpenSourceCode";
            this.toolStripButtonOpenSourceCode.Size = new System.Drawing.Size(53, 22);
            this.toolStripButtonOpenSourceCode.Text = "Open";
            this.toolStripButtonOpenSourceCode.ToolTipText = "Open my source code";
            this.toolStripButtonOpenSourceCode.Click += new System.EventHandler(this.toolStripButtonOpenSourceCode_Click);
            // 
            // toolStripButtonOpenCurrentSourceCode
            // 
            this.toolStripButtonOpenCurrentSourceCode.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonOpenCurrentSourceCode.Image")));
            this.toolStripButtonOpenCurrentSourceCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonOpenCurrentSourceCode.Name = "toolStripButtonOpenCurrentSourceCode";
            this.toolStripButtonOpenCurrentSourceCode.Size = new System.Drawing.Size(93, 22);
            this.toolStripButtonOpenCurrentSourceCode.Text = "Open Current";
            this.toolStripButtonOpenCurrentSourceCode.ToolTipText = "Open current source code that is loaded on Arduino";
            this.toolStripButtonOpenCurrentSourceCode.Click += new System.EventHandler(this.toolStripButtonOpenCurrentSourceCode_Click);
            // 
            // toolStripButtonSaveUploadOutput
            // 
            this.toolStripButtonSaveUploadOutput.Enabled = false;
            this.toolStripButtonSaveUploadOutput.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonSaveUploadOutput.Image")));
            this.toolStripButtonSaveUploadOutput.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonSaveUploadOutput.Name = "toolStripButtonSaveUploadOutput";
            this.toolStripButtonSaveUploadOutput.Size = new System.Drawing.Size(124, 20);
            this.toolStripButtonSaveUploadOutput.Text = "Save Upload Output";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // timerStart
            // 
            this.timerStart.Tick += new System.EventHandler(this.timerStart_Tick);
            // 
            // timerGetUserList
            // 
            this.timerGetUserList.Interval = 5000;
            this.timerGetUserList.Tick += new System.EventHandler(this.timerGetUserList_Tick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "avatar_female.png");
            this.imageList1.Images.SetKeyName(1, "avatar_male.png");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(50);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.exListBoxUsers);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer1.Size = new System.Drawing.Size(779, 289);
            this.splitContainer1.SplitterDistance = 163;
            this.splitContainer1.TabIndex = 3;
            // 
            // exListBoxUsers
            // 
            this.exListBoxUsers.BackColor = System.Drawing.Color.White;
            this.exListBoxUsers.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.exListBoxUsers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exListBoxUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.exListBoxUsers.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.exListBoxUsers.FormattingEnabled = true;
            this.exListBoxUsers.ItemHeight = 66;
            this.exListBoxUsers.Location = new System.Drawing.Point(0, 0);
            this.exListBoxUsers.Name = "exListBoxUsers";
            this.exListBoxUsers.Size = new System.Drawing.Size(163, 289);
            this.exListBoxUsers.TabIndex = 2;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.syntaxHighlightingTextBoxSourceCode);
            this.splitContainer4.Panel1.Controls.Add(this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode);
            this.splitContainer4.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.webControl1);
            this.splitContainer4.Panel2Collapsed = true;
            this.splitContainer4.Size = new System.Drawing.Size(612, 289);
            this.splitContainer4.SplitterDistance = 476;
            this.splitContainer4.TabIndex = 1;
            // 
            // lineNumbers_For_syntaxHighlightingTextBoxSourceCode
            // 
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode._SeeThroughMode_ = false;
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.AutoSizing = true;
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.BackgroundGradient_AlphaColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.BackgroundGradient_BetaColor = System.Drawing.Color.LightSteelBlue;
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.BackgroundGradient_Direction = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.BorderLines_Color = System.Drawing.Color.SlateGray;
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.BorderLines_Style = System.Drawing.Drawing2D.DashStyle.Dot;
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.BorderLines_Thickness = 1F;
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.DockSide = WebCamWindowsClient.richTextBoxLineNumber.LineNumberDockSide.Left;
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.GridLines_Color = System.Drawing.Color.SlateGray;
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.GridLines_Style = System.Drawing.Drawing2D.DashStyle.Dot;
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.GridLines_Thickness = 1F;
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.LineNrs_Alignment = System.Drawing.ContentAlignment.TopRight;
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.LineNrs_AntiAlias = true;
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.LineNrs_AsHexadecimal = false;
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.LineNrs_ClippedByItemRectangle = true;
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.LineNrs_LeadingZeroes = true;
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.LineNrs_Offset = new System.Drawing.Size(0, 0);
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.Location = new System.Drawing.Point(1, 0);
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.Margin = new System.Windows.Forms.Padding(0);
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.MarginLines_Color = System.Drawing.Color.SlateGray;
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.MarginLines_Side = WebCamWindowsClient.richTextBoxLineNumber.LineNumberDockSide.Right;
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.MarginLines_Style = System.Drawing.Drawing2D.DashStyle.Solid;
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.MarginLines_Thickness = 1F;
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.Name = "lineNumbers_For_syntaxHighlightingTextBoxSourceCode";
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.Padding = new System.Windows.Forms.Padding(0, 0, 2, 0);
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.ParentRichTextBox = this.syntaxHighlightingTextBoxSourceCode;
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.Show_BackgroundGradient = true;
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.Show_BorderLines = true;
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.Show_GridLines = true;
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.Show_LineNrs = true;
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.Show_MarginLines = true;
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.Size = new System.Drawing.Size(17, 289);
            this.lineNumbers_For_syntaxHighlightingTextBoxSourceCode.TabIndex = 0;
            // 
            // webControl1
            // 
            this.webControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webControl1.Location = new System.Drawing.Point(0, 0);
            this.webControl1.Size = new System.Drawing.Size(96, 100);
            this.webControl1.TabIndex = 0;
            this.webControl1.ViewType = Awesomium.Core.WebViewType.Offscreen;
            this.webControl1.DocumentReady += new Awesomium.Core.DocumentReadyEventHandler(this.Awesomium_Windows_Forms_WebControl_DocumentReady);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 25);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer5);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl);
            this.splitContainer2.Size = new System.Drawing.Size(808, 561);
            this.splitContainer2.SplitterDistance = 289;
            this.splitContainer2.TabIndex = 4;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.label3);
            this.splitContainer5.Panel1.Controls.Add(this.label2);
            this.splitContainer5.Panel1.Controls.Add(this.label1);
            this.splitContainer5.Panel1.Controls.Add(this.buttonTabControl);
            this.splitContainer5.Panel1.Controls.Add(this.buttonHelp);
            this.splitContainer5.Panel1.Controls.Add(this.buttonUserList);
            this.splitContainer5.Panel1MinSize = 5;
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.splitContainer1);
            this.splitContainer5.Panel2MinSize = 5;
            this.splitContainer5.Size = new System.Drawing.Size(808, 289);
            this.splitContainer5.SplitterDistance = 25;
            this.splitContainer5.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label3.Location = new System.Drawing.Point(2, 210);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(5, 67);
            this.label3.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(2, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(5, 114);
            this.label2.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.Location = new System.Drawing.Point(2, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(5, 81);
            this.label1.TabIndex = 3;
            // 
            // buttonTabControl
            // 
            this.buttonTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonTabControl.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonTabControl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonTabControl.FlatAppearance.BorderSize = 0;
            this.buttonTabControl.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.buttonTabControl.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.buttonTabControl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTabControl.Location = new System.Drawing.Point(7, 210);
            this.buttonTabControl.Name = "buttonTabControl";
            this.buttonTabControl.Size = new System.Drawing.Size(16, 67);
            this.buttonTabControl.TabIndex = 2;
            this.buttonTabControl.Text = "Outputs";
            this.buttonTabControl.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.buttonTabControl.UseVisualStyleBackColor = false;
            this.buttonTabControl.Click += new System.EventHandler(this.buttonTabControl_Click);
            this.buttonTabControl.Paint += new System.Windows.Forms.PaintEventHandler(this.buttonTabControl_Paint);
            // 
            // buttonHelp
            // 
            this.buttonHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonHelp.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonHelp.FlatAppearance.BorderSize = 0;
            this.buttonHelp.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.buttonHelp.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.buttonHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonHelp.Location = new System.Drawing.Point(7, 90);
            this.buttonHelp.Name = "buttonHelp";
            this.buttonHelp.Size = new System.Drawing.Size(16, 114);
            this.buttonHelp.TabIndex = 1;
            this.buttonHelp.Text = "Help on coding";
            this.buttonHelp.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.buttonHelp.UseVisualStyleBackColor = false;
            this.buttonHelp.Click += new System.EventHandler(this.buttonHelp_Click);
            this.buttonHelp.Paint += new System.Windows.Forms.PaintEventHandler(this.buttonHelp_Paint);
            // 
            // buttonUserList
            // 
            this.buttonUserList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonUserList.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonUserList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonUserList.FlatAppearance.BorderSize = 0;
            this.buttonUserList.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Silver;
            this.buttonUserList.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Gray;
            this.buttonUserList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonUserList.Location = new System.Drawing.Point(7, 3);
            this.buttonUserList.Name = "buttonUserList";
            this.buttonUserList.Size = new System.Drawing.Size(16, 81);
            this.buttonUserList.TabIndex = 0;
            this.buttonUserList.Text = "User List";
            this.buttonUserList.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.buttonUserList.UseVisualStyleBackColor = false;
            this.buttonUserList.Click += new System.EventHandler(this.buttonUserList_Click);
            this.buttonUserList.Paint += new System.Windows.Forms.PaintEventHandler(this.buttonUserList_Paint);
            // 
            // webSessionProvider1
            // 
            this.webSessionProvider1.Views.Add(this.webControl1);
            // 
            // autocompleteMenu1
            // 
            this.autocompleteMenu1.Colors = ((AutocompleteMenuNS.Colors)(resources.GetObject("autocompleteMenu1.Colors")));
            this.autocompleteMenu1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.autocompleteMenu1.ImageList = this.imageList2;
            this.autocompleteMenu1.Items = new string[0];
            this.autocompleteMenu1.TargetControlWrapper = null;
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "0182-power.png");
            this.imageList2.Images.SetKeyName(1, "0047-stack.png");
            this.imageList2.Images.SetKeyName(2, "0035-file-text.png");
            this.imageList2.Images.SetKeyName(3, "0384-embed.png");
            // 
            // formArduino
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 586);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.toolStrip1);
            this.KeyPreview = true;
            this.Name = "formArduino";
            this.Text = "Lab++ IDE";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formArduino_FormClosing);
            this.Shown += new System.EventHandler(this.formArduino_Shown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.formArduino_KeyUp);
            this.tabControl.ResumeLayout(false);
            this.tabPageSerial.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.tabPageOutputCompile.ResumeLayout(false);
            this.tabPageOutputUpload.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private richTextBoxLineNumber lineNumbers_For_syntaxHighlightingTextBoxSourceCode;
        private System.Windows.Forms.RichTextBox richTextBoxOutputCompile;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonCompile;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpenSerial;
        private System.Windows.Forms.Timer timerStart;
        private System.Windows.Forms.Timer timerGetUserList;
        private exListBox exListBoxUsers;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TextBox textBoxSerialCommand;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageOutputCompile;
        private System.Windows.Forms.TabPage tabPageSerial;
        private System.Windows.Forms.RichTextBox richTextBoxSerial;
        private System.Windows.Forms.Button buttonSendSerialCommand;
        private System.Windows.Forms.ToolStripButton toolStripButtonUpload;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveSource;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpenCurrentSourceCode;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpenSourceCode;
        private System.Windows.Forms.TabPage tabPageOutputUpload;
        private System.Windows.Forms.RichTextBox richTextBoxOutputUpload;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private UrielGuy.SyntaxHighlightingTextBox.SyntaxHighlightingTextBox syntaxHighlightingTextBoxSourceCode;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveUploadOutput;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.Button buttonUserList;
        private System.Windows.Forms.Button buttonTabControl;
        private System.Windows.Forms.Button buttonHelp;
        private System.Windows.Forms.ToolStripButton toolStripButtonZoomOut;
        private System.Windows.Forms.ToolStripButton toolStripButtonZoomIn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Awesomium.Windows.Forms.WebControl webControl1;
        private Awesomium.Windows.Forms.WebSessionProvider webSessionProvider1;
        private System.Windows.Forms.ToolStripButton toolStripButtonUndo;
        private System.Windows.Forms.ToolStripButton toolStripButtonRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private AutocompleteMenuNS.AutocompleteMenu autocompleteMenu1;
        private System.Windows.Forms.ImageList imageList2;
    }
}
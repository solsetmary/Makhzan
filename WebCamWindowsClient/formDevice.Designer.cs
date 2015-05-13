namespace WebCamWindowsClient
{
    partial class formDevice
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formDevice));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonIncreaseTime = new System.Windows.Forms.Button();
            this.buttonRotate = new System.Windows.Forms.Button();
            this.buttonDecreaseTime = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelRotation = new System.Windows.Forms.Label();
            this.buttonMic = new System.Windows.Forms.Button();
            this.buttonCaptureImage = new System.Windows.Forms.Button();
            this.comboBoxRotate = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblCamerasNr = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.timerMic = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuCamera = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemMic = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItemCapture = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.contextMenuCamera.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.comboBoxRotate);
            this.groupBox2.Controls.Add(this.lblCamerasNr);
            this.groupBox2.Location = new System.Drawing.Point(3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(339, 272);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.buttonIncreaseTime);
            this.groupBox1.Controls.Add(this.buttonRotate);
            this.groupBox1.Controls.Add(this.buttonDecreaseTime);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.labelRotation);
            this.groupBox1.Controls.Add(this.buttonMic);
            this.groupBox1.Controls.Add(this.buttonCaptureImage);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(322, 64);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Visible = false;
            // 
            // buttonIncreaseTime
            // 
            this.buttonIncreaseTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonIncreaseTime.Image = ((System.Drawing.Image)(resources.GetObject("buttonIncreaseTime.Image")));
            this.buttonIncreaseTime.Location = new System.Drawing.Point(7, 13);
            this.buttonIncreaseTime.Name = "buttonIncreaseTime";
            this.buttonIncreaseTime.Size = new System.Drawing.Size(34, 34);
            this.buttonIncreaseTime.TabIndex = 5;
            this.toolTip1.SetToolTip(this.buttonIncreaseTime, "Slower frame rate");
            this.buttonIncreaseTime.UseVisualStyleBackColor = true;
            this.buttonIncreaseTime.Click += new System.EventHandler(this.buttonIncreaseTime_Click);
            // 
            // buttonRotate
            // 
            this.buttonRotate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRotate.Image = ((System.Drawing.Image)(resources.GetObject("buttonRotate.Image")));
            this.buttonRotate.Location = new System.Drawing.Point(91, 13);
            this.buttonRotate.Name = "buttonRotate";
            this.buttonRotate.Size = new System.Drawing.Size(34, 34);
            this.buttonRotate.TabIndex = 15;
            this.toolTip1.SetToolTip(this.buttonRotate, "Rotate");
            this.buttonRotate.UseVisualStyleBackColor = true;
            this.buttonRotate.Click += new System.EventHandler(this.buttonRotate_Click);
            // 
            // buttonDecreaseTime
            // 
            this.buttonDecreaseTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDecreaseTime.Image = ((System.Drawing.Image)(resources.GetObject("buttonDecreaseTime.Image")));
            this.buttonDecreaseTime.Location = new System.Drawing.Point(47, 13);
            this.buttonDecreaseTime.Name = "buttonDecreaseTime";
            this.buttonDecreaseTime.Size = new System.Drawing.Size(34, 34);
            this.buttonDecreaseTime.TabIndex = 6;
            this.toolTip1.SetToolTip(this.buttonDecreaseTime, "Faster frame rate");
            this.buttonDecreaseTime.UseVisualStyleBackColor = true;
            this.buttonDecreaseTime.Click += new System.EventHandler(this.buttonDecreaseTime_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(264, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Capture";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Speed";
            this.label3.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(215, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Voice";
            this.label1.Visible = false;
            // 
            // labelRotation
            // 
            this.labelRotation.AutoSize = true;
            this.labelRotation.Location = new System.Drawing.Point(126, 34);
            this.labelRotation.Name = "labelRotation";
            this.labelRotation.Size = new System.Drawing.Size(39, 13);
            this.labelRotation.TabIndex = 11;
            this.labelRotation.Text = "Rotate";
            // 
            // buttonMic
            // 
            this.buttonMic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMic.Enabled = false;
            this.buttonMic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMic.Image = ((System.Drawing.Image)(resources.GetObject("buttonMic.Image")));
            this.buttonMic.Location = new System.Drawing.Point(237, 13);
            this.buttonMic.Name = "buttonMic";
            this.buttonMic.Size = new System.Drawing.Size(34, 34);
            this.buttonMic.TabIndex = 14;
            this.toolTip1.SetToolTip(this.buttonMic, "Get voice");
            this.buttonMic.UseVisualStyleBackColor = true;
            this.buttonMic.Click += new System.EventHandler(this.buttonMic_Click);
            // 
            // buttonCaptureImage
            // 
            this.buttonCaptureImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCaptureImage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCaptureImage.Image = ((System.Drawing.Image)(resources.GetObject("buttonCaptureImage.Image")));
            this.buttonCaptureImage.Location = new System.Drawing.Point(279, 13);
            this.buttonCaptureImage.Name = "buttonCaptureImage";
            this.buttonCaptureImage.Size = new System.Drawing.Size(34, 34);
            this.buttonCaptureImage.TabIndex = 13;
            this.toolTip1.SetToolTip(this.buttonCaptureImage, "Capture Image");
            this.buttonCaptureImage.UseVisualStyleBackColor = true;
            this.buttonCaptureImage.Click += new System.EventHandler(this.buttonCaptureImage_Click);
            // 
            // comboBoxRotate
            // 
            this.comboBoxRotate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxRotate.FormattingEnabled = true;
            this.comboBoxRotate.Items.AddRange(new object[] {
            "None",
            "XY Filip"});
            this.comboBoxRotate.Location = new System.Drawing.Point(-3, 0);
            this.comboBoxRotate.Name = "comboBoxRotate";
            this.comboBoxRotate.Size = new System.Drawing.Size(19, 21);
            this.comboBoxRotate.TabIndex = 12;
            this.comboBoxRotate.Visible = false;
            this.comboBoxRotate.SelectedIndexChanged += new System.EventHandler(this.comboBoxRotate_SelectedIndexChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.ContextMenuStrip = this.contextMenuCamera;
            this.pictureBox1.Location = new System.Drawing.Point(9, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(320, 240);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblCamerasNr
            // 
            this.lblCamerasNr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCamerasNr.AutoSize = true;
            this.lblCamerasNr.ForeColor = System.Drawing.Color.Red;
            this.lblCamerasNr.Location = new System.Drawing.Point(6, 254);
            this.lblCamerasNr.Name = "lblCamerasNr";
            this.lblCamerasNr.Size = new System.Drawing.Size(79, 13);
            this.lblCamerasNr.TabIndex = 2;
            this.lblCamerasNr.Text = "Please wait . . .";
            // 
            // timer1
            // 
            this.timer1.Interval = 1500;
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "JPEG files|*.jpg";
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Info";
            // 
            // contextMenuCamera
            // 
            this.contextMenuCamera.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemMic,
            this.toolStripMenuItem1,
            this.toolStripMenuItemCapture});
            this.contextMenuCamera.Name = "contextMenuConversation";
            this.contextMenuCamera.Size = new System.Drawing.Size(120, 54);
            // 
            // toolStripMenuItemMic
            // 
            this.toolStripMenuItemMic.Enabled = false;
            this.toolStripMenuItemMic.Name = "toolStripMenuItemMic";
            this.toolStripMenuItemMic.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemMic.Text = "Get Voice";
            this.toolStripMenuItemMic.Click += new System.EventHandler(this.toolStripMenuItemMic_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(116, 6);
            // 
            // toolStripMenuItemCapture
            // 
            this.toolStripMenuItemCapture.Name = "toolStripMenuItemCapture";
            this.toolStripMenuItemCapture.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItemCapture.Text = "Capture";
            this.toolStripMenuItemCapture.Click += new System.EventHandler(this.toolStripMenuItemCapture_Click);
            // 
            // formDevice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(200)))));
            this.ClientSize = new System.Drawing.Size(345, 279);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "formDevice";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.cameraForm_FormClosing);
            this.Load += new System.EventHandler(this.cameraForm_Load);
            this.Shown += new System.EventHandler(this.cameraForm_Shown);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.contextMenuCamera.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonDecreaseTime;
        private System.Windows.Forms.Button buttonIncreaseTime;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblCamerasNr;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label labelRotation;
        private System.Windows.Forms.ComboBox comboBoxRotate;
        private System.Windows.Forms.Button buttonCaptureImage;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Timer timerMic;
        private System.Windows.Forms.Button buttonMic;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonRotate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuCamera;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMic;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCapture;
    }
}
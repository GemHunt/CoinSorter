namespace ImageClassifier
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.cmdWebCam = new System.Windows.Forms.Button();
            this.cmdRead = new System.Windows.Forms.Button();
            this.trackBarSensorDelay = new System.Windows.Forms.TrackBar();
            this.lblIRSensorCount = new System.Windows.Forms.Label();
            this.lblCamCount = new System.Windows.Forms.Label();
            this.cmdToggle = new System.Windows.Forms.Button();
            this.checkLive = new System.Windows.Forms.CheckBox();
            this.timerWebcam = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.trackBarToggleDelay = new System.Windows.Forms.TrackBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmdTestFindCoinCenter = new System.Windows.Forms.Button();
            this.cmdTestFindCoinCenterBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSensorDelay)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarToggleDelay)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(133, 137);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmdWebCam
            // 
            this.cmdWebCam.Location = new System.Drawing.Point(273, 57);
            this.cmdWebCam.Name = "cmdWebCam";
            this.cmdWebCam.Size = new System.Drawing.Size(96, 23);
            this.cmdWebCam.TabIndex = 1;
            this.cmdWebCam.Text = "Web Cam";
            this.cmdWebCam.UseVisualStyleBackColor = true;
            this.cmdWebCam.Click += new System.EventHandler(this.cmdWebCam_Click);
            // 
            // cmdRead
            // 
            this.cmdRead.Location = new System.Drawing.Point(273, 106);
            this.cmdRead.Name = "cmdRead";
            this.cmdRead.Size = new System.Drawing.Size(96, 67);
            this.cmdRead.TabIndex = 2;
            this.cmdRead.Text = "Read Port";
            this.cmdRead.UseVisualStyleBackColor = true;
            this.cmdRead.Click += new System.EventHandler(this.cmdRead_Click);
            // 
            // trackBarSensorDelay
            // 
            this.trackBarSensorDelay.Location = new System.Drawing.Point(6, 31);
            this.trackBarSensorDelay.Maximum = 1500;
            this.trackBarSensorDelay.Minimum = 1;
            this.trackBarSensorDelay.Name = "trackBarSensorDelay";
            this.trackBarSensorDelay.Size = new System.Drawing.Size(941, 45);
            this.trackBarSensorDelay.TabIndex = 3;
            this.trackBarSensorDelay.TickFrequency = 100;
            this.trackBarSensorDelay.Value = 570;
            // 
            // lblIRSensorCount
            // 
            this.lblIRSensorCount.AutoSize = true;
            this.lblIRSensorCount.Location = new System.Drawing.Point(515, 66);
            this.lblIRSensorCount.Name = "lblIRSensorCount";
            this.lblIRSensorCount.Size = new System.Drawing.Size(85, 13);
            this.lblIRSensorCount.TabIndex = 4;
            this.lblIRSensorCount.Text = "IR Sensor Count";
            // 
            // lblCamCount
            // 
            this.lblCamCount.AutoSize = true;
            this.lblCamCount.Location = new System.Drawing.Point(515, 88);
            this.lblCamCount.Name = "lblCamCount";
            this.lblCamCount.Size = new System.Drawing.Size(59, 13);
            this.lblCamCount.TabIndex = 5;
            this.lblCamCount.Text = "Cam Count";
            // 
            // cmdToggle
            // 
            this.cmdToggle.Location = new System.Drawing.Point(487, 121);
            this.cmdToggle.Name = "cmdToggle";
            this.cmdToggle.Size = new System.Drawing.Size(87, 52);
            this.cmdToggle.TabIndex = 6;
            this.cmdToggle.Text = "Toggle ";
            this.cmdToggle.UseVisualStyleBackColor = true;
            this.cmdToggle.Click += new System.EventHandler(this.cmdToggle_Click);
            // 
            // checkLive
            // 
            this.checkLive.AutoSize = true;
            this.checkLive.Location = new System.Drawing.Point(273, 12);
            this.checkLive.Name = "checkLive";
            this.checkLive.Size = new System.Drawing.Size(92, 17);
            this.checkLive.TabIndex = 7;
            this.checkLive.Text = "Live Webcam";
            this.checkLive.UseVisualStyleBackColor = true;
            this.checkLive.CheckedChanged += new System.EventHandler(this.checkLive_CheckedChanged);
            // 
            // timerWebcam
            // 
            this.timerWebcam.Interval = 250;
            this.timerWebcam.Tick += new System.EventHandler(this.timerWebcam_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.trackBarSensorDelay);
            this.groupBox1.Location = new System.Drawing.Point(4, 181);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(958, 96);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sensor Delay";
            // 
            // trackBarToggleDelay
            // 
            this.trackBarToggleDelay.Location = new System.Drawing.Point(6, 31);
            this.trackBarToggleDelay.Maximum = 1500;
            this.trackBarToggleDelay.Minimum = 1;
            this.trackBarToggleDelay.Name = "trackBarToggleDelay";
            this.trackBarToggleDelay.Size = new System.Drawing.Size(941, 45);
            this.trackBarToggleDelay.TabIndex = 3;
            this.trackBarToggleDelay.TickFrequency = 100;
            this.trackBarToggleDelay.Value = 300;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.trackBarToggleDelay);
            this.groupBox2.Location = new System.Drawing.Point(4, 283);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(958, 96);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Toggle Delay";
            // 
            // cmdTestFindCoinCenter
            // 
            this.cmdTestFindCoinCenter.Location = new System.Drawing.Point(791, 79);
            this.cmdTestFindCoinCenter.Name = "cmdTestFindCoinCenter";
            this.cmdTestFindCoinCenter.Size = new System.Drawing.Size(119, 94);
            this.cmdTestFindCoinCenter.TabIndex = 11;
            this.cmdTestFindCoinCenter.Text = "Test Find Coin Center";
            this.cmdTestFindCoinCenter.UseVisualStyleBackColor = true;
            this.cmdTestFindCoinCenter.Click += new System.EventHandler(this.cmdTestFindCoinCenter_Click);
            // 
            // cmdTestFindCoinCenterBack
            // 
            this.cmdTestFindCoinCenterBack.Location = new System.Drawing.Point(744, 79);
            this.cmdTestFindCoinCenterBack.Name = "cmdTestFindCoinCenterBack";
            this.cmdTestFindCoinCenterBack.Size = new System.Drawing.Size(41, 94);
            this.cmdTestFindCoinCenterBack.TabIndex = 12;
            this.cmdTestFindCoinCenterBack.Text = "Back";
            this.cmdTestFindCoinCenterBack.UseVisualStyleBackColor = true;
            this.cmdTestFindCoinCenterBack.Click += new System.EventHandler(this.cmdTestFindCoinCenterBack_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(975, 402);
            this.Controls.Add(this.cmdTestFindCoinCenterBack);
            this.Controls.Add(this.cmdTestFindCoinCenter);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkLive);
            this.Controls.Add(this.cmdToggle);
            this.Controls.Add(this.lblCamCount);
            this.Controls.Add(this.lblIRSensorCount);
            this.Controls.Add(this.cmdRead);
            this.Controls.Add(this.cmdWebCam);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSensorDelay)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarToggleDelay)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button cmdWebCam;
        private System.Windows.Forms.Button cmdRead;
        private System.Windows.Forms.TrackBar trackBarSensorDelay;
        private System.Windows.Forms.Label lblIRSensorCount;
        private System.Windows.Forms.Label lblCamCount;
        private System.Windows.Forms.Button cmdToggle;
        private System.Windows.Forms.CheckBox checkLive;
        private System.Windows.Forms.Timer timerWebcam;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar trackBarToggleDelay;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button cmdTestFindCoinCenter;
        private System.Windows.Forms.Button cmdTestFindCoinCenterBack;
    }
}


namespace ImageClassifier
{
    partial class frmMain
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
            this.cmdStartWebCam = new System.Windows.Forms.Button();
            this.cmdOpenSerialPort = new System.Windows.Forms.Button();
            this.trackBarSensorDelay = new System.Windows.Forms.TrackBar();
            this.lblIRSensorCount = new System.Windows.Forms.Label();
            this.cmdManualSolenoidToggle = new System.Windows.Forms.Button();
            this.checkShowLiveWebcamView = new System.Windows.Forms.CheckBox();
            this.timerWebcam = new System.Windows.Forms.Timer(this.components);
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.trackBarToggleDelay = new System.Windows.Forms.TrackBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBoxLiveCapture = new System.Windows.Forms.GroupBox();
            this.cmdTestFindCoinCenter = new System.Windows.Forms.Button();
            this.cmdTestFindCoinCenterBack = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmdImageLabelingWizard = new System.Windows.Forms.Button();
            this.cmdClassifyImages = new System.Windows.Forms.Button();
            this.cmdAugmentImages = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSensorDelay)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarToggleDelay)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBoxLiveCapture.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdStartWebCam
            // 
            this.cmdStartWebCam.Location = new System.Drawing.Point(270, 83);
            this.cmdStartWebCam.Name = "cmdStartWebCam";
            this.cmdStartWebCam.Size = new System.Drawing.Size(153, 35);
            this.cmdStartWebCam.TabIndex = 1;
            this.cmdStartWebCam.Text = "Manually Capture Once";
            this.cmdStartWebCam.UseVisualStyleBackColor = true;
            this.cmdStartWebCam.Click += new System.EventHandler(this.cmdWebCam_Click);
            // 
            // cmdOpenSerialPort
            // 
            this.cmdOpenSerialPort.Location = new System.Drawing.Point(21, 47);
            this.cmdOpenSerialPort.Name = "cmdOpenSerialPort";
            this.cmdOpenSerialPort.Size = new System.Drawing.Size(152, 59);
            this.cmdOpenSerialPort.TabIndex = 2;
            this.cmdOpenSerialPort.Text = "Start Automatic Capture From IR Sensor";
            this.cmdOpenSerialPort.UseVisualStyleBackColor = true;
            this.cmdOpenSerialPort.Click += new System.EventHandler(this.cmdOpenSerialPort_Click);
            // 
            // trackBarSensorDelay
            // 
            this.trackBarSensorDelay.Location = new System.Drawing.Point(6, 16);
            this.trackBarSensorDelay.Maximum = 1500;
            this.trackBarSensorDelay.Minimum = 1;
            this.trackBarSensorDelay.Name = "trackBarSensorDelay";
            this.trackBarSensorDelay.Size = new System.Drawing.Size(458, 45);
            this.trackBarSensorDelay.TabIndex = 3;
            this.trackBarSensorDelay.TickFrequency = 100;
            this.trackBarSensorDelay.Value = 555;
            // 
            // lblIRSensorCount
            // 
            this.lblIRSensorCount.AutoSize = true;
            this.lblIRSensorCount.Location = new System.Drawing.Point(18, 28);
            this.lblIRSensorCount.Name = "lblIRSensorCount";
            this.lblIRSensorCount.Size = new System.Drawing.Size(85, 13);
            this.lblIRSensorCount.TabIndex = 4;
            this.lblIRSensorCount.Text = "IR Sensor Count";
            // 
            // cmdManualSolenoidToggle
            // 
            this.cmdManualSolenoidToggle.Location = new System.Drawing.Point(270, 128);
            this.cmdManualSolenoidToggle.Name = "cmdManualSolenoidToggle";
            this.cmdManualSolenoidToggle.Size = new System.Drawing.Size(153, 35);
            this.cmdManualSolenoidToggle.TabIndex = 6;
            this.cmdManualSolenoidToggle.Text = "Manually Solenoid Toggle ";
            this.cmdManualSolenoidToggle.UseVisualStyleBackColor = true;
            this.cmdManualSolenoidToggle.Click += new System.EventHandler(this.cmdToggle_Click);
            // 
            // checkShowLiveWebcamView
            // 
            this.checkShowLiveWebcamView.AutoSize = true;
            this.checkShowLiveWebcamView.Location = new System.Drawing.Point(251, 40);
            this.checkShowLiveWebcamView.Name = "checkShowLiveWebcamView";
            this.checkShowLiveWebcamView.Size = new System.Drawing.Size(217, 17);
            this.checkShowLiveWebcamView.TabIndex = 7;
            this.checkShowLiveWebcamView.Text = "Automatically Capture 4 Times a Second";
            this.checkShowLiveWebcamView.UseVisualStyleBackColor = true;
            this.checkShowLiveWebcamView.CheckedChanged += new System.EventHandler(this.checkLive_CheckedChanged);
            // 
            // timerWebcam
            // 
            this.timerWebcam.Interval = 250;
            this.timerWebcam.Tick += new System.EventHandler(this.timerWebcam_Tick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.trackBarSensorDelay);
            this.groupBox1.Location = new System.Drawing.Point(4, 239);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 67);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Sensor Delay";
            // 
            // trackBarToggleDelay
            // 
            this.trackBarToggleDelay.Location = new System.Drawing.Point(6, 16);
            this.trackBarToggleDelay.Maximum = 1500;
            this.trackBarToggleDelay.Minimum = 1;
            this.trackBarToggleDelay.Name = "trackBarToggleDelay";
            this.trackBarToggleDelay.Size = new System.Drawing.Size(458, 45);
            this.trackBarToggleDelay.TabIndex = 3;
            this.trackBarToggleDelay.TickFrequency = 100;
            this.trackBarToggleDelay.Value = 300;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.trackBarToggleDelay);
            this.groupBox2.Location = new System.Drawing.Point(4, 312);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(483, 67);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Toggle Delay";
            // 
            // groupBoxLiveCapture
            // 
            this.groupBoxLiveCapture.Controls.Add(this.cmdOpenSerialPort);
            this.groupBoxLiveCapture.Controls.Add(this.lblIRSensorCount);
            this.groupBoxLiveCapture.Location = new System.Drawing.Point(12, 12);
            this.groupBoxLiveCapture.Name = "groupBoxLiveCapture";
            this.groupBoxLiveCapture.Size = new System.Drawing.Size(214, 163);
            this.groupBoxLiveCapture.TabIndex = 14;
            this.groupBoxLiveCapture.TabStop = false;
            this.groupBoxLiveCapture.Text = "Live Capture";
            // 
            // cmdTestFindCoinCenter
            // 
            this.cmdTestFindCoinCenter.Location = new System.Drawing.Point(68, 27);
            this.cmdTestFindCoinCenter.Name = "cmdTestFindCoinCenter";
            this.cmdTestFindCoinCenter.Size = new System.Drawing.Size(119, 94);
            this.cmdTestFindCoinCenter.TabIndex = 11;
            this.cmdTestFindCoinCenter.Text = "Test Find Coin Center";
            this.cmdTestFindCoinCenter.UseVisualStyleBackColor = true;
            this.cmdTestFindCoinCenter.Click += new System.EventHandler(this.cmdTestFindCoinCenter_Click);
            // 
            // cmdTestFindCoinCenterBack
            // 
            this.cmdTestFindCoinCenterBack.Location = new System.Drawing.Point(21, 27);
            this.cmdTestFindCoinCenterBack.Name = "cmdTestFindCoinCenterBack";
            this.cmdTestFindCoinCenterBack.Size = new System.Drawing.Size(41, 94);
            this.cmdTestFindCoinCenterBack.TabIndex = 12;
            this.cmdTestFindCoinCenterBack.Text = "Back";
            this.cmdTestFindCoinCenterBack.UseVisualStyleBackColor = true;
            this.cmdTestFindCoinCenterBack.Click += new System.EventHandler(this.cmdTestFindCoinCenterBack_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmdTestFindCoinCenterBack);
            this.groupBox3.Controls.Add(this.cmdTestFindCoinCenter);
            this.groupBox3.Location = new System.Drawing.Point(234, 43);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(214, 163);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Test Image Files";
            // 
            // cmdImageLabelingWizard
            // 
            this.cmdImageLabelingWizard.Location = new System.Drawing.Point(56, 71);
            this.cmdImageLabelingWizard.Name = "cmdImageLabelingWizard";
            this.cmdImageLabelingWizard.Size = new System.Drawing.Size(153, 35);
            this.cmdImageLabelingWizard.TabIndex = 14;
            this.cmdImageLabelingWizard.Text = "Image Labeling Wizard";
            this.cmdImageLabelingWizard.UseVisualStyleBackColor = true;
            this.cmdImageLabelingWizard.Click += new System.EventHandler(this.cmdImageLabelingWizard_Click);
            // 
            // cmdClassifyImages
            // 
            this.cmdClassifyImages.Location = new System.Drawing.Point(56, 153);
            this.cmdClassifyImages.Name = "cmdClassifyImages";
            this.cmdClassifyImages.Size = new System.Drawing.Size(153, 35);
            this.cmdClassifyImages.TabIndex = 15;
            this.cmdClassifyImages.Text = "Classify Images";
            this.cmdClassifyImages.UseVisualStyleBackColor = true;
            this.cmdClassifyImages.Click += new System.EventHandler(this.cmdClassifyImages_Click);
            // 
            // cmdAugmentImages
            // 
            this.cmdAugmentImages.Location = new System.Drawing.Point(56, 112);
            this.cmdAugmentImages.Name = "cmdAugmentImages";
            this.cmdAugmentImages.Size = new System.Drawing.Size(153, 35);
            this.cmdAugmentImages.TabIndex = 16;
            this.cmdAugmentImages.Text = "Augment Images";
            this.cmdAugmentImages.UseVisualStyleBackColor = true;
            this.cmdAugmentImages.Click += new System.EventHandler(this.cmdAugmentImages_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cmdAugmentImages);
            this.groupBox5.Controls.Add(this.cmdClassifyImages);
            this.groupBox5.Controls.Add(this.cmdImageLabelingWizard);
            this.groupBox5.Controls.Add(this.groupBox3);
            this.groupBox5.Location = new System.Drawing.Point(502, 16);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(488, 363);
            this.groupBox5.TabIndex = 17;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Image File Processes";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 393);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBoxLiveCapture);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkShowLiveWebcamView);
            this.Controls.Add(this.cmdManualSolenoidToggle);
            this.Controls.Add(this.cmdStartWebCam);
            this.Name = "frmMain";
            this.Text = "Coin Classifier";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSensorDelay)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarToggleDelay)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBoxLiveCapture.ResumeLayout(false);
            this.groupBoxLiveCapture.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdStartWebCam;
        private System.Windows.Forms.Button cmdOpenSerialPort;
        private System.Windows.Forms.TrackBar trackBarSensorDelay;
        private System.Windows.Forms.Label lblIRSensorCount;
        private System.Windows.Forms.Button cmdManualSolenoidToggle;
        private System.Windows.Forms.CheckBox checkShowLiveWebcamView;
        private System.Windows.Forms.Timer timerWebcam;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar trackBarToggleDelay;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBoxLiveCapture;
        private System.Windows.Forms.Button cmdTestFindCoinCenter;
        private System.Windows.Forms.Button cmdTestFindCoinCenterBack;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button cmdImageLabelingWizard;
        private System.Windows.Forms.Button cmdClassifyImages;
        private System.Windows.Forms.Button cmdAugmentImages;
        private System.Windows.Forms.GroupBox groupBox5;
    }
}


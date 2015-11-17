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
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkFakeCamera = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.chkInsertToDatabase = new System.Windows.Forms.CheckBox();
            this.cmdTestFindCoinCenter = new System.Windows.Forms.Button();
            this.cmdTestFindCoinCenterBack = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmdImageLabelingWizard = new System.Windows.Forms.Button();
            this.cmdClassifyImages = new System.Windows.Forms.Button();
            this.cmdAugmentImages = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.cmdCropForDates = new System.Windows.Forms.Button();
            this.cmdLabelRotation = new System.Windows.Forms.Button();
            this.txtTargetDate = new System.Windows.Forms.TextBox();
            this.lblTargetDate = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblDesign = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.chkDeskew = new System.Windows.Forms.CheckBox();
            this.chkAutoRotate = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSensorDelay)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarToggleDelay)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBoxLiveCapture.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdStartWebCam
            // 
            this.cmdStartWebCam.Location = new System.Drawing.Point(787, 296);
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
            this.lblIRSensorCount.Location = new System.Drawing.Point(3, 16);
            this.lblIRSensorCount.Name = "lblIRSensorCount";
            this.lblIRSensorCount.Size = new System.Drawing.Size(85, 13);
            this.lblIRSensorCount.TabIndex = 4;
            this.lblIRSensorCount.Text = "IR Sensor Count";
            // 
            // cmdManualSolenoidToggle
            // 
            this.cmdManualSolenoidToggle.Location = new System.Drawing.Point(787, 346);
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
            this.checkShowLiveWebcamView.Location = new System.Drawing.Point(542, 296);
            this.checkShowLiveWebcamView.Name = "checkShowLiveWebcamView";
            this.checkShowLiveWebcamView.Size = new System.Drawing.Size(217, 17);
            this.checkShowLiveWebcamView.TabIndex = 7;
            this.checkShowLiveWebcamView.Text = "Automatically Capture 2 Times a Second";
            this.checkShowLiveWebcamView.UseVisualStyleBackColor = true;
            this.checkShowLiveWebcamView.CheckedChanged += new System.EventHandler(this.checkLive_CheckedChanged);
            // 
            // timerWebcam
            // 
            this.timerWebcam.Interval = 500;
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
            this.groupBoxLiveCapture.Controls.Add(this.lblTargetDate);
            this.groupBoxLiveCapture.Controls.Add(this.txtTargetDate);
            this.groupBoxLiveCapture.Controls.Add(this.groupBox4);
            this.groupBoxLiveCapture.Controls.Add(this.chkInsertToDatabase);
            this.groupBoxLiveCapture.Controls.Add(this.cmdOpenSerialPort);
            this.groupBoxLiveCapture.Location = new System.Drawing.Point(12, 12);
            this.groupBoxLiveCapture.Name = "groupBoxLiveCapture";
            this.groupBoxLiveCapture.Size = new System.Drawing.Size(263, 221);
            this.groupBoxLiveCapture.TabIndex = 14;
            this.groupBoxLiveCapture.TabStop = false;
            this.groupBoxLiveCapture.Text = "Auto Capture";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkFakeCamera);
            this.groupBox4.Controls.Add(this.textBox1);
            this.groupBox4.Enabled = false;
            this.groupBox4.Location = new System.Drawing.Point(6, 143);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(238, 67);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            // 
            // chkFakeCamera
            // 
            this.chkFakeCamera.AutoSize = true;
            this.chkFakeCamera.Location = new System.Drawing.Point(15, 14);
            this.chkFakeCamera.Name = "chkFakeCamera";
            this.chkFakeCamera.Size = new System.Drawing.Size(212, 17);
            this.chkFakeCamera.TabIndex = 6;
            this.chkFakeCamera.Text = "Fake Camera With Files From Directory:";
            this.chkFakeCamera.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(15, 37);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(212, 20);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "F:/OpenCV/preCaptured";
            // 
            // chkInsertToDatabase
            // 
            this.chkInsertToDatabase.AutoSize = true;
            this.chkInsertToDatabase.Location = new System.Drawing.Point(21, 112);
            this.chkInsertToDatabase.Name = "chkInsertToDatabase";
            this.chkInsertToDatabase.Size = new System.Drawing.Size(117, 17);
            this.chkInsertToDatabase.TabIndex = 5;
            this.chkInsertToDatabase.Text = "Insert To Database";
            this.chkInsertToDatabase.UseVisualStyleBackColor = true;
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
            this.cmdImageLabelingWizard.Location = new System.Drawing.Point(44, 43);
            this.cmdImageLabelingWizard.Name = "cmdImageLabelingWizard";
            this.cmdImageLabelingWizard.Size = new System.Drawing.Size(153, 35);
            this.cmdImageLabelingWizard.TabIndex = 14;
            this.cmdImageLabelingWizard.Text = "Image Labeling Wizard";
            this.cmdImageLabelingWizard.UseVisualStyleBackColor = true;
            this.cmdImageLabelingWizard.Click += new System.EventHandler(this.cmdImageLabelingWizard_Click);
            // 
            // cmdClassifyImages
            // 
            this.cmdClassifyImages.Location = new System.Drawing.Point(44, 207);
            this.cmdClassifyImages.Name = "cmdClassifyImages";
            this.cmdClassifyImages.Size = new System.Drawing.Size(153, 35);
            this.cmdClassifyImages.TabIndex = 15;
            this.cmdClassifyImages.Text = "Classify Images";
            this.cmdClassifyImages.UseVisualStyleBackColor = true;
            this.cmdClassifyImages.Click += new System.EventHandler(this.cmdClassifyImages_Click);
            // 
            // cmdAugmentImages
            // 
            this.cmdAugmentImages.Location = new System.Drawing.Point(44, 125);
            this.cmdAugmentImages.Name = "cmdAugmentImages";
            this.cmdAugmentImages.Size = new System.Drawing.Size(153, 35);
            this.cmdAugmentImages.TabIndex = 16;
            this.cmdAugmentImages.Text = "Augment Images";
            this.cmdAugmentImages.UseVisualStyleBackColor = true;
            this.cmdAugmentImages.Click += new System.EventHandler(this.cmdAugmentImages_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.cmdCropForDates);
            this.groupBox5.Controls.Add(this.cmdLabelRotation);
            this.groupBox5.Controls.Add(this.cmdAugmentImages);
            this.groupBox5.Controls.Add(this.cmdClassifyImages);
            this.groupBox5.Controls.Add(this.cmdImageLabelingWizard);
            this.groupBox5.Controls.Add(this.groupBox3);
            this.groupBox5.Location = new System.Drawing.Point(542, 16);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(448, 256);
            this.groupBox5.TabIndex = 17;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Image File Processes";
            // 
            // cmdCropForDates
            // 
            this.cmdCropForDates.Location = new System.Drawing.Point(44, 166);
            this.cmdCropForDates.Name = "cmdCropForDates";
            this.cmdCropForDates.Size = new System.Drawing.Size(153, 35);
            this.cmdCropForDates.TabIndex = 18;
            this.cmdCropForDates.Text = "Crop For Dates";
            this.cmdCropForDates.UseVisualStyleBackColor = true;
            this.cmdCropForDates.Click += new System.EventHandler(this.cmdCropForDates_Click);
            // 
            // cmdLabelRotation
            // 
            this.cmdLabelRotation.Location = new System.Drawing.Point(44, 84);
            this.cmdLabelRotation.Name = "cmdLabelRotation";
            this.cmdLabelRotation.Size = new System.Drawing.Size(153, 35);
            this.cmdLabelRotation.TabIndex = 17;
            this.cmdLabelRotation.Text = "Label Rotation";
            this.cmdLabelRotation.UseVisualStyleBackColor = true;
            this.cmdLabelRotation.Click += new System.EventHandler(this.cmdLabelRotation_Click);
            // 
            // txtTargetDate
            // 
            this.txtTargetDate.Location = new System.Drawing.Point(180, 62);
            this.txtTargetDate.Name = "txtTargetDate";
            this.txtTargetDate.Size = new System.Drawing.Size(64, 20);
            this.txtTargetDate.TabIndex = 9;
            this.txtTargetDate.Text = "1981";
            // 
            // lblTargetDate
            // 
            this.lblTargetDate.AutoSize = true;
            this.lblTargetDate.Location = new System.Drawing.Point(179, 47);
            this.lblTargetDate.Name = "lblTargetDate";
            this.lblTargetDate.Size = new System.Drawing.Size(33, 13);
            this.lblTargetDate.TabIndex = 10;
            this.lblTargetDate.Text = "Date:";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(0, 76);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(79, 33);
            this.lblDate.TabIndex = 11;
            this.lblDate.Text = "0000";
            // 
            // lblDesign
            // 
            this.lblDesign.AutoSize = true;
            this.lblDesign.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesign.Location = new System.Drawing.Point(0, 43);
            this.lblDesign.Name = "lblDesign";
            this.lblDesign.Size = new System.Drawing.Size(174, 33);
            this.lblDesign.TabIndex = 12;
            this.lblDesign.Text = "Coin Design";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.lblDate);
            this.groupBox6.Controls.Add(this.lblDesign);
            this.groupBox6.Controls.Add(this.lblIRSensorCount);
            this.groupBox6.Location = new System.Drawing.Point(281, 16);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(206, 217);
            this.groupBox6.TabIndex = 18;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Capture Output";
            // 
            // chkDeskew
            // 
            this.chkDeskew.AutoSize = true;
            this.chkDeskew.Checked = true;
            this.chkDeskew.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDeskew.Location = new System.Drawing.Point(542, 320);
            this.chkDeskew.Name = "chkDeskew";
            this.chkDeskew.Size = new System.Drawing.Size(65, 17);
            this.chkDeskew.TabIndex = 19;
            this.chkDeskew.Text = "Deskew";
            this.chkDeskew.UseVisualStyleBackColor = true;
            // 
            // chkAutoRotate
            // 
            this.chkAutoRotate.AutoSize = true;
            this.chkAutoRotate.Location = new System.Drawing.Point(542, 346);
            this.chkAutoRotate.Name = "chkAutoRotate";
            this.chkAutoRotate.Size = new System.Drawing.Size(83, 17);
            this.chkAutoRotate.TabIndex = 20;
            this.chkAutoRotate.Text = "Auto Rotate";
            this.chkAutoRotate.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 393);
            this.Controls.Add(this.chkAutoRotate);
            this.Controls.Add(this.chkDeskew);
            this.Controls.Add(this.groupBox6);
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
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
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
        private System.Windows.Forms.Button cmdLabelRotation;
        private System.Windows.Forms.Button cmdCropForDates;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkFakeCamera;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox chkInsertToDatabase;
        private System.Windows.Forms.Label lblTargetDate;
        private System.Windows.Forms.TextBox txtTargetDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblDesign;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.CheckBox chkDeskew;
        private System.Windows.Forms.CheckBox chkAutoRotate;
    }
}


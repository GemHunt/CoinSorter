using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;

namespace ImageClassifier
{
    public partial class frmMain : Form
    {
        [DllImport("E:\\build\\Caffe-prefix\\src\\Caffe-build\\examples\\cpp_classification\\Debug\\classification-d.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FindCoinCenter(int imageID, bool show);

        SerialPortManager _spManager;
        int coinCenterImageID = 10098;
        int IRSensorCount = 0;
        bool AutomaticCaptureOn = false;
        
        public frmMain()
        {
            InitializeComponent();
            _spManager = new SerialPortManager();
            _spManager.NewSerialDataRecieved += new EventHandler<SerialDataEventArgs>(_spManager_NewSerialDataRecieved);

            var di = new DirectoryInfo("F:/OpenCV/Raw/");
            var lastFileName = di.GetFiles()
             .OrderByDescending(f => f.Name)
             .First();

            IRSensorCount = ImagesDB.GetImageIDFromFileName(lastFileName.Name);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        void _spManager_NewSerialDataRecieved(object sender, SerialDataEventArgs e)
        {
            if (this.InvokeRequired)
            {
                // Using this.Invoke causes deadlock when closing serial port, and BeginInvoke is good practice anyway.
                this.BeginInvoke(new EventHandler<SerialDataEventArgs>(_spManager_NewSerialDataRecieved), new object[] { sender, e });
                return;
            }
            Console.WriteLine(Encoding.ASCII.GetString(e.Data));
            Timer timerIRSensorDelay = new Timer();
            timerIRSensorDelay.Interval = trackBarSensorDelay.Value;
            timerIRSensorDelay.Enabled = true;
            timerIRSensorDelay.Tick += new EventHandler(timerIRSensorDelay_Tick);
            IRSensorCount += 1;
            lblIRSensorCount.Text = "IR Sensor Count: " + IRSensorCount;

            //Toggle every other coin:
            // toggleNextCoin = !toggleNextCoin;
            // if (toggleNextCoin) {
            //Timer timerToggleDelay = new Timer();
            //timerToggleDelay.Interval = trackBarSensorDelay.Value + trackBarToggleDelay.Value;
            //timerToggleDelay.Enabled = true;
            //timerToggleDelay.Tick += new EventHandler(timerToggleDelay_Tick);
            // }
        }
        
        private void cmdWebCam_Click(object sender, EventArgs e)
        {
            captureFromWebCam();
            //Application.Exit();
        }

        // Handles the "Start Listening"-buttom click event
        private void cmdOpenSerialPort_Click(object sender, EventArgs e)
        {
            int date;
            bool goodDate = int.TryParse(txtTargetDate.Text, out date);            
            if (!goodDate) {
                MessageBox.Show("Bad Date");
                return;
            }
            
            if (AutomaticCaptureOn) {
                cmdOpenSerialPort.BackColor = SystemColors.ButtonFace;
                cmdOpenSerialPort.Text = "Start Automatic Capture From IR Sensor";
                AutomaticCaptureOn = false;
                _spManager.StopListening();
            } else {
                cmdOpenSerialPort.BackColor = Color.LightGreen;
                cmdOpenSerialPort.Text = "Stop Automatic Capture From IR Sensor";
                AutomaticCaptureOn = true;
                _spManager.StartListening();
            }
        }

        private void timerIRSensorDelay_Tick(object sender, EventArgs e)
        {
            Timer timerIRSensorDelay = (Timer)sender;
            timerIRSensorDelay.Enabled = false;
            System.Diagnostics.Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            bool result = captureFromWebCam();
            stopWatch.Stop();
            Debug.WriteLine("cc took" + stopWatch.ElapsedMilliseconds);
            if (result)
            {
                Timer timerToggleDelay = new Timer();
                int totalDelay = trackBarToggleDelay.Value - (int)stopWatch.ElapsedMilliseconds;
                if (totalDelay > 0){
                    timerToggleDelay.Interval = totalDelay;
                    timerToggleDelay.Enabled = true;
                    timerToggleDelay.Tick += new EventHandler(timerToggleDelay_Tick);   
                }
            }
        }

        private void timerToggleDelay_Tick(object sender, EventArgs e)
        {
            Timer timerToggleDelay = (Timer)sender;
            timerToggleDelay.Enabled = false;
            _spManager.Toggle();
        }



        private void cmdToggle_Click(object sender, EventArgs e)
        {
            _spManager.Toggle();
        }

        private void checkLive_CheckedChanged(object sender, EventArgs e)
        {
            timerWebcam.Enabled = checkShowLiveWebcamView.Checked;
        }

        private void timerWebcam_Tick(object sender, EventArgs e)
        {
            IRSensorCount++;
            captureFromWebCam();
        }

        private bool captureFromWebCam()
        {
            int targetDate;
            bool goodDate = int.TryParse(txtTargetDate.Text, out targetDate);
            if (goodDate)
            {
                int date = 0;
                String design = "null";
                Camera.ClassifyFromWebCam(IRSensorCount + 10000000, true, true,ref date,ref design);
                lblDate.Text = date.ToString();
                lblDesign.Text = design;
                //if (date > 1979){
                    return true;
                //}
            }
            return false;
        }

        private void cmdTestFindCoinCenter_Click(object sender, EventArgs e)
        {
            coinCenterImageID++;
            TestFindCoinCenter();
        }

        private void cmdTestFindCoinCenterBack_Click(object sender, EventArgs e)
        {
            coinCenterImageID--;
            TestFindCoinCenter();
        }

        private void TestFindCoinCenter()
        {
            if (File.Exists("F:/OpenCV/" + coinCenterImageID + "raw.jpg"))
            {
                FindCoinCenter(coinCenterImageID, true);
            }
        }

        private void cmdImageLabelingWizard_Click(object sender, EventArgs e)
        {
            frmLabel frm = new frmLabel();
            frm.Show();
        }

        private void cmdClassifyImages_Click(object sender, EventArgs e)
        {
            frmClassifyImages frm = new frmClassifyImages();
            frm.Show();
        }

        private void cmdAugmentImages_Click(object sender, EventArgs e)
        {

            frmAugmentImages frm = new frmAugmentImages();
            frm.Show();
        }

        private void cmdLabelRotation_Click(object sender, EventArgs e)
        {
            frmLabelRotation frm = new frmLabelRotation();
            frm.Show();
        }

        private void cmdCropForDates_Click(object sender, EventArgs e)
        {

            frmCropDates frm = new frmCropDates();
            frm.Show();
        }

       
    }
}

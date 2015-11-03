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

namespace ImageClassifier
{
    public partial class frmMain : Form
    {
        [DllImport("E:\\build\\Caffe-prefix\\src\\Caffe-build\\examples\\cpp_classification\\Debug\\classification-d.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int captureFromWebCam(int imageID,int show);

        [DllImport("E:\\build\\Caffe-prefix\\src\\Caffe-build\\examples\\cpp_classification\\Debug\\classification-d.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int FindCoinCenter(int imageID, int show);

        SerialPortManager _spManager;
        int coinCenterImageID = 10098;
        int IRSensorCount = 5134;
        
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
            Timer timerToggleDelay = new Timer();
            timerToggleDelay.Interval = trackBarSensorDelay.Value + trackBarToggleDelay.Value;
            timerToggleDelay.Enabled = true;
            timerToggleDelay.Tick += new EventHandler(timerToggleDelay_Tick);
            // }
        }



      

       
        
        private void cmdWebCam_Click(object sender, EventArgs e)
        {
            captureFromWebCam();
            //Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //captureFromWebCam();

        }

        // Handles the "Start Listening"-buttom click event
        private void cmdRead_Click(object sender, EventArgs e)
        {
            _spManager.StartListening();
        }

        private void timerIRSensorDelay_Tick(object sender, EventArgs e)
        {
            Timer timerIRSensorDelay = (Timer)sender;
            timerIRSensorDelay.Enabled = false;
            captureFromWebCam();
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


        private void captureFromWebCam()
        {
            captureFromWebCam(IRSensorCount + 10000000,1);
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
                FindCoinCenter(coinCenterImageID, 1);
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

    }
}

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
    public partial class Form1 : Form
    {
        [DllImport("E:\\build\\Caffe-prefix\\src\\Caffe-build\\examples\\cpp_classification\\Debug\\classification-d.dll")]
        public static extern IntPtr ClassifyImage(String model_file, String trained_file, String mean_file, String label_file, String image_file);

        [DllImport("E:\\build\\Caffe-prefix\\src\\Caffe-build\\examples\\cpp_classification\\Debug\\classification-d.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ReleaseMemory(IntPtr ptr);

        [DllImport("E:\\build\\Caffe-prefix\\src\\Caffe-build\\examples\\cpp_classification\\Debug\\classification-d.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int captureFromWebCam();

        SerialPortManager _spManager;
        int IRSensorCount = 0;

        public Form1()
        {
            InitializeComponent();
            _spManager = new SerialPortManager();
            _spManager.NewSerialDataRecieved += new EventHandler<SerialDataEventArgs>(_spManager_NewSerialDataRecieved);
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
            timerIRSensorDelay.Interval = trackBar1.Value; 
            timerIRSensorDelay.Enabled = true;
            timerIRSensorDelay.Tick += new EventHandler(timerIRSensorDelay_Tick);
            IRSensorCount += 1;
            lblIRSensorCount.Text = "IR Sensor Count: " + IRSensorCount;

        }


        private void button1_Click(object sender, EventArgs e)
        {
            String dir = "F:\\20150924-184701-f9a5_epoch_3.0\\";
            //String dir = "F:\\model\\";
            
            String model_file = dir + "deploy.prototxt";
            String trained_file = dir + "snapshot.caffemodel";
            String mean_file = dir + "mean.binaryproto";
            String label_file = dir + "labels.txt";
            String imageDir = "F:\\Rotated\\HeadsWithRotation3600\\000\\";

            string[] files = Directory.GetFiles(imageDir);

            int counter = 0;
            List<double> standardDeviations = new List<double>();

            foreach (string image_file in files)
            {
                List<double> predictions = new List<double>();
                
                for (int i = 0; i < 359; i += 360)
                {
                    string fileName = image_file.Replace("\\000\\", "\\" + i.ToString("D3") + "\\");
                    IntPtr ptr = ClassifyImage(model_file, trained_file, mean_file, label_file, fileName);

                    double[] result = new double[3];
                    Marshal.Copy(ptr, result, 0, 3);
                    ReleaseMemory(ptr);

                    if (result[2] < 4)
                    {
                        double prediction = result[0] - i;
                        if (prediction < 0)
                        {
                            prediction += 360;
                        }

                        predictions.Add(prediction);

                        Console.WriteLine("Prediction: " + prediction + "   Confidence: " + result[1] + "   Standard Deviation: " + result[2]);
                        Console.WriteLine();
                        Console.WriteLine();
                    }
                }

                if (predictions.Count != 0)
                {
                    double average = predictions.Average();
                    double sumOfSquaresOfDifferences = predictions.Select(val => (val - average) * (val - average)).Sum();
                    double sd = Math.Sqrt(sumOfSquaresOfDifferences / predictions.Count);
                    if (sd < 4)
                    {
                        standardDeviations.Add(sd);
                    }

                    Console.WriteLine("Seq:" + counter + "   Prediction Mean: " + average + "  Prediction SD: " + sd);
                    Console.WriteLine();
                    Console.WriteLine();

                    FileInfo fi = new FileInfo(image_file);
                    String fullImage = "D:\\CoinImages\\Rotated\\heads\\" + fi.FullName.Substring(36, 16);
                    //int angle = Convert.ToInt32(average * 10);
                    //File.Move(fullImage + ".jpg", fullImage + angle.ToString("D4") + ".jpg");

                    counter++;
                    if (counter == 1) { break; }
                }
            }
            //Console.WriteLine("Mean Standard Deviations: " + standardDeviations.Average());
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

      private void cmdToggle_Click(object sender, EventArgs e)
      {
          _spManager.Toggle();
      }
       
    }
}

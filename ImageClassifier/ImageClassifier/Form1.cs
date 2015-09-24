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
        [DllImport("D:\\GitHub\\build\\Caffe-prefix\\src\\Caffe-build\\examples\\cpp_classification\\Debug\\classification-d.dll")]
        public static extern int SetupNetwork(String model_file, String trained_file, String mean_file, String label_file, String image_file);

        //[DllImport("D:\\GitHub\\build\\Caffe-prefix\\src\\Caffe-build\\examples\\cpp_classification\\Debug\\classification-d.dll")]
        //public static extern int ClassifyImage(String image_file);


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //String dir = "F:\\20150923-070243-795f_epoch_3.0\\";
            String dir = "F:\\model\\";

            
            String model_file = dir + "deploy.prototxt";
            String trained_file = dir + "snapshot.caffemodel";
            String mean_file = dir + "mean.binaryproto";
            String label_file = dir + "labels.txt";
            String imageDir = "F:\\Rotated\\HeadsWithRotation360\\000\\";

            string[] files = Directory.GetFiles(imageDir);

            foreach (string image_file in files)
            {
                int result = SetupNetwork(model_file, trained_file, mean_file, label_file, image_file);
                Console.WriteLine(result);
            }


        }
    }
}

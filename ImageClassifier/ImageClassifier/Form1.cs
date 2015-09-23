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

namespace ImageClassifier
{
    public partial class Form1 : Form
    {
        [DllImport("D:\\GitHub\\build\\Caffe-prefix\\src\\Caffe-build\\examples\\cpp_classification\\Debug\\classification-d.dll")]
        public static extern int ClassifyNetwork(String model_file, String trained_file, String mean_file, String label_file, String image_file);

        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           String dir = "F:\\model\\";

           String model_file = dir + "deploy.prototxt";
           String trained_file = dir + "snapshot_iter_11836.caffemodel";
           String mean_file = dir + "mean.binaryproto";
           String label_file = dir + "labels.txt";
            String image_file = "F:\\Rotated\\HeadsWithRotation360\\000\\1000000251020612.png";

            int result = ClassifyNetwork(model_file, trained_file, mean_file, label_file, image_file);
            Console.WriteLine(result);
        }
    }
}

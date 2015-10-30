using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Runtime.InteropServices;
using System.IO;

namespace ImageClassifier
{
    public partial class frmClassifyImages : Form
    {
        [DllImport("E:\\build\\Caffe-prefix\\src\\Caffe-build\\examples\\cpp_classification\\Debug\\classification-d.dll")]
        public static extern IntPtr ClassifyImage(String model_file, String trained_file, String mean_file, String label_file, String image_file);
        
        [DllImport("E:\\build\\Caffe-prefix\\src\\Caffe-build\\examples\\cpp_classification\\Debug\\classification-d.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int ReleaseMemory(IntPtr ptr);
        
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;

        public frmClassifyImages()
        {
            InitializeComponent();
            SetConnection();
        }

        private void SetConnection()
        {
            sql_con = new SQLiteConnection
                ("Data Source=C:/Users/pkrush/Documents/GemHunt/CoinSorter/networks/classify.db;Version=3;New=False;Compress=True;");
            sql_con.Open();
            
        }

        private void ExecuteQuery(string txtQuery)
        {
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();

        }

        private void LoadData()
        {
            SetConnection();
            sql_con.Open();
            sql_cmd = sql_con.CreateCommand();
            string CommandText = "select id, desc from mains";
            DB = new SQLiteDataAdapter(CommandText, sql_con);
            sql_con.Close();
        }


        private void AddResult(int modelID, int imageID, int labelID, double score)
        {
            StringBuilder SQL = new StringBuilder();

            SQL.AppendLine("BEGIN;");
            SQL.Append("Insert into Results (ModelID,ImageID,LabelID,Score) values (");
            SQL.Append(modelID + ",");
            SQL.Append(imageID + ",");
            SQL.Append(labelID + ",");
            SQL.AppendLine(score + ");");
            SQL.AppendLine("COMMIT;");
            ExecuteQuery(SQL.ToString());
            //sql_con.Close();
        }

        private void AddImage(int imageID, int labelID)
        {
            StringBuilder SQL = new StringBuilder();

            SQL.AppendLine("BEGIN;");
            SQL.Append("Insert into Images (ImageID,LabelID) values (");
            SQL.Append(imageID + ",");
            SQL.AppendLine(labelID + ");");
            SQL.AppendLine("COMMIT;");
            ExecuteQuery(SQL.ToString());
            //sql_con.Close();
        }
       
        
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void cmdClassify_Click_1(object sender, EventArgs e)
        {
            String dir = "F:/models/20151029-163033-683c_epoch_12.0/";

            String model_file = dir + "deploy.prototxt";
            String trained_file = dir + "snapshot.caffemodel";
            String mean_file = dir + "mean.binaryproto";
            String label_file = dir + "labels.txt";
            String imageDir = "F:/new4/tails/";

            string[] files = Directory.GetFiles(imageDir);

            foreach (string image_file in files)
            {
                IntPtr ptr = ClassifyImage(model_file, trained_file, mean_file, label_file, image_file);
                double[] result = new double[4];
                Marshal.Copy(ptr, result, 0, 4);
                ReleaseMemory(ptr);
                AddResult(1, Convert.ToInt32(image_file.Substring(14, 8)),(int)result[0], result[2]);
                AddResult(1, Convert.ToInt32(image_file.Substring(14, 8)),(int)result[1],result[3]);
                //AddImage(Convert.ToInt32(image_file.Substring(14, 8)), 0);

            }
        }
    }
}

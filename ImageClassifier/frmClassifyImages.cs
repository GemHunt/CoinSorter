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
        CaffeModel caffe = new CaffeModel();
        
        public frmClassifyImages()
        {
            InitializeComponent();
        }
        
        private void cmdClassify_Click_1(object sender, EventArgs e)
        {
            if (!Directory.Exists(txtOldImageDirectory.Text))
            {
                txtOldImageDirectory.Focus();
                MessageBox.Show("Old Image Directory Does Not Exist");
                return;
            }

            if (!Directory.Exists(txtNewImageDirectory.Text))
            {
                txtNewImageDirectory.Focus();
                MessageBox.Show("New mage Directory Does Not Exist");
                return;
            }

            if (!Directory.Exists(txtRootModelDirectory.Text))
            {
                txtRootModelDirectory.Focus();
                MessageBox.Show("Root Model Directory Does Not Exist");
                return;
            }

            DirectoryInfo imageDirectory = new DirectoryInfo(txtOldImageDirectory.Text);
            DirectoryInfo rootModelDirectory = new DirectoryInfo(txtRootModelDirectory.Text);
            String modelDirectory = rootModelDirectory + txtModel.Text;

            if (!Directory.Exists(modelDirectory))
            {
                txtModel.Focus();
                MessageBox.Show(modelDirectory + " Does Not Exist");
                return;
            }

            if (chkClassify.Checked == false && chkAddImagesToDataBase.Checked == false)
            {
                txtModel.Focus();
                MessageBox.Show("Nothing is checked");
                return;
            }

            caffe.Classify(txtOldImageDirectory.Text, txtNewImageDirectory.Text, txtModel.Text, modelDirectory, chkClassify.Checked, chkAddImagesToDataBase.Checked,chkMoveImages.Checked, chkIncludeSubDirectories.Checked);
        }

    }
}

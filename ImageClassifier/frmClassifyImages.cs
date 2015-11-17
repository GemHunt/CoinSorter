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

            if (chkMoveImages.Checked && !Directory.Exists(txtNewImageDirectory.Text))
            {
                txtNewImageDirectory.Focus();
                MessageBox.Show("New image Directory Does Not Exist");
                return;
            }

            if (!Directory.Exists(txtModelsDir.Text))
            {
                txtModelsDir.Focus();
                MessageBox.Show("Root Model Directory Does Not Exist");
                return;
            }

            DirectoryInfo imageDirectory = new DirectoryInfo(txtOldImageDirectory.Text);
            DirectoryInfo modelDir = new DirectoryInfo(txtModelsDir.Text);

            if (chkClassify.Checked == false && chkAddImagesToDataBase.Checked == false)
            {
                txtModelsDir.Focus();
                MessageBox.Show("Nothing is checked");
                return;
            }
            caffe.ClassifyFiles(txtOldImageDirectory.Text, txtNewImageDirectory.Text, txtModelsDir.Text, chkClassify.Checked, chkAddImagesToDataBase.Checked, chkMoveImages.Checked, chkIncludeSubDirectories.Checked);
        }

    }
}

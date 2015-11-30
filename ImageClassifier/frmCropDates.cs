using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageClassifier
{
    public partial class frmCropDates : Form
    {
        public frmCropDates()
        {
            InitializeComponent();
        }

        private void cmdCropDates_Click(object sender, EventArgs e)
        {
            ImagesDB.CropForDates(txtCropDirectory.Text ,txtDateDirectory.Text, chkAugment.Checked);
        }
    }
}

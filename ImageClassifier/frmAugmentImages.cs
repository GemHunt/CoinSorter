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
    public partial class frmAugmentImages : Form
    {
        public frmAugmentImages()
        {
            InitializeComponent();
        }

        private void cmdAugment_Click(object sender, EventArgs e)
        {
            ImagesDB.AugmentImages(txtImageDirectory.Text);
        }
    }
}

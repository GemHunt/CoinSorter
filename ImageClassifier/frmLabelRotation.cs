using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.IO;

namespace ImageClassifier
{
    public partial class frmLabelRotation : Form
    {
        public String SingleImageFileName;
        private string CoinFileName;
        private Int32 NextCoinIndex = 0;
        private FileInfo[] imageFiles;
       
        public frmLabelRotation()
        {
            InitializeComponent();
        }

        private void frmLabelRotation_Shown(object sender, EventArgs e)
        {
            if (SingleImageFileName == null)
            {
                const string ImageDir = "F:\\NewRot\\heads";
                DirectoryInfo dirInfo = new DirectoryInfo(ImageDir);
                dirInfo = new DirectoryInfo(ImageDir);
                imageFiles = dirInfo.GetFiles();
            }
            else
            {
                imageFiles = new FileInfo[1];
                imageFiles[0] = new FileInfo(SingleImageFileName);
            }
            LoadNextPicture();         
        }

        private void LoadNextPicture()
        {
            if (NextCoinIndex == imageFiles.Length)
            {
                this.Close();
                return;
            }
            
            FileInfo fi = imageFiles[NextCoinIndex];
            CoinFileName = fi.FullName;
            NextCoinIndex += 1;
            Image coinImage = Bitmap.FromFile(CoinFileName);
            PictureBoxCoin.Image = coinImage;
        }

        private void PictureBoxCoin_Click(object sender, EventArgs e)
        {
            MouseEventArgs clickArg = (MouseEventArgs)e;
            float offsetX = Convert.ToInt32(PictureBoxCoin.Width / 2);
            float offsetY = Convert.ToInt32(PictureBoxCoin.Height / 2);
            float clickX = clickArg.X - offsetX;
            float clickY = (PictureBoxCoin.Height - clickArg.Y) - offsetY;
            float coinAngle;
        
            coinAngle = (Convert.ToSingle(Math.Atan2(clickY, clickX) / Math.PI) * 180);

            if (clickArg.Button == System.Windows.Forms.MouseButtons.Left)
            {
                dynamic distanceFromCenter = Math.Sqrt(clickX * clickX + clickY * clickY);
                coinAngle = coinAngle - 3;
            }

            Console.WriteLine("angle: " + coinAngle + "  Y:  " + clickY);

            coinAngle = coinAngle + 180;

            if (coinAngle > 360)
            {
                coinAngle = coinAngle - 360;
            }

            if (coinAngle < 0)
            {
                coinAngle = coinAngle + 360;
            }

            int imageID = Convert.ToInt32(CoinFileName.Substring(CoinFileName.Length - 12, 8));
            ImagesDB.UpdateAngle(imageID, coinAngle);
            LoadNextPicture();
        }

        public static Bitmap RotateImage(Image image, PointF offset, float angle)
        {
            if (image == null)
            {
                throw new ArgumentNullException("image");
            }

            //create a new empty bitmap to hold rotated image
            Bitmap rotatedBmp = new Bitmap(image.Width, image.Height);
            rotatedBmp.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            //make a graphics object from the empty bitmap
            Graphics g = Graphics.FromImage(rotatedBmp);

            //Put the rotation point in the center of the image
            g.TranslateTransform(offset.X, offset.Y);

            //rotate the image
            g.RotateTransform(angle);

            //move the image back
            g.TranslateTransform(-offset.X, -offset.Y);

            //draw passed in image onto graphics object
            g.DrawImage(image, new PointF(0, 0));

            return rotatedBmp;
        }

        //private void saveAngle()
        //{
            //dynamic coinfileinfo = new FileInfo(CoinFileName);
            //Int32 coinAngleInt = Convert.ToInt32(Math.Round(coinAngle));
            //if (coinAngleInt < 0)
            //{
            //    coinAngleInt = coinAngleInt + 360;
            //}
            //if (coinAngleInt >= 360)
            //{
            //    coinAngleInt = coinAngleInt - 360;
            //}
            //File.Copy(CoinFileName, "F:\\NewRot\\HeadsWithRotation\\" + coinfileinfo.Name.Replace(".jpg", coinAngleInt.ToString().PadLeft(3,'0') + ".jpg"), true);
            //LoadNextPicture();
        //}



    }
}

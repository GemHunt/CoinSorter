using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ImageClassifier
{
    public partial class frmLabel : Form
    {
        Dictionary<int, double> images = new Dictionary<int, double>();
        int LastListBoxWorkingLabelSelectedIndex = -1;
        public frmLabel()
        {
            InitializeComponent();
        }


        private void cmdGetImages_Click(object sender, EventArgs e)
        {
            listBoxWorkingLabel.Items.Clear();
            listBoxClickList.Items.Clear();
            listBoxLabelAllShown.Items.Clear();

            listBoxWorkingLabel.Items.Add("No Label");
            List<String> labels = new List<String>();

            if (radioDates.Checked)
            {
                labels = LabelsDB.GetDateLabels(radioDecades.Checked);
            }
            if (radioLabelDesigns.Checked)
            {
                labels = LabelsDB.GetDesignLabels();
            }

            foreach (String label in labels)
            {
                listBoxWorkingLabel.Items.Add(label);
            }
            listBoxClickList.Items.AddRange(listBoxWorkingLabel.Items);
            listBoxClickList.Items.Add("Delete");
            listBoxLabelAllShown.Items.AddRange(listBoxWorkingLabel.Items);
            listBoxLabelAllShown.Items.Add("Delete");

            listBoxWorkingLabel.SelectedIndex = 0;
            listBoxClickList.SelectedIndex = 1;
            listBoxLabelAllShown.SelectedIndex = 1;

            ImageRefresh(true);
        }

        private int GetDateFromLabel(String label)
        {
            if (label == "No Label")
            {
                return -1;
            }
            int date;
            int.TryParse(listBoxWorkingLabel.SelectedItem.ToString(), out date);
            return date;
        }

        private void ImageRefresh(bool newimageIDs)
        {
            int date = GetDateFromLabel(listBoxWorkingLabel.SelectedItem.ToString());

            if (newimageIDs)
            {
                images = ImagesDB.GetDateImages(date, chkLabeled.Checked, radioDecades.Checked);
            }



            groupBoxImages.Controls.Clear();
            int imageSize = 64;
            for (int y = 0; y < 800; y = y + imageSize + 10)
            {
                for (int x = 0; x < 800; x = x + imageSize + 10)
                {
                    if (images.Count == 0)
                    {
                        return;
                    }
                    int imageID = images.Keys.First();
                    double angle = images.Values.First();
                    String fileName = "F:/OpenCV/Crops/good/" + imageID + ".jpg";
                    images.Remove(imageID);
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
                    pictureBox.Tag = fileName;
                    Bitmap rotated = CloneImage(fileName, angle);
                    Rectangle cropRect = new Rectangle(307, 250, imageSize, imageSize);
                    rotated = rotated.Clone(cropRect, rotated.PixelFormat);
                    pictureBox.BackgroundImage = rotated;
                    pictureBox.Height = imageSize;
                    pictureBox.Width = imageSize;
                    pictureBox.Left = x;
                    pictureBox.Top = y + 10;
                    pictureBox.Click += new EventHandler(pictureBox_Click);
                    pictureBox.Paint += new PaintEventHandler((sender, e) =>
                    {
                        e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                        e.Graphics.DrawString(imageID.ToString(), Font, Brushes.Blue, 3, 3);
                    });
                    groupBoxImages.Controls.Add(pictureBox);


                }
            }
        }


        private Bitmap CloneImage(string aImagePath, double angle)
        {
            // create original image
            Image originalImage = new Bitmap(aImagePath);

            // create an empty clone of the same size of original
            //Bitmap clone = new Bitmap(originalImage.Width, originalImage.Height);
            Bitmap clone = new Bitmap(originalImage.Width, originalImage.Height);

            // get the object representing clone's currently empty drawing surface
            Graphics g = Graphics.FromImage(clone);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighSpeed;

            PointF offset = new PointF(203, 203);
            g.TranslateTransform(offset.X, offset.Y);
            g.RotateTransform(Convert.ToSingle(angle));
            //move the image back
            g.TranslateTransform(-offset.X, -offset.Y);
            g.DrawImage(originalImage, 0, 0, originalImage.Width, originalImage.Height);
            // copy the original image onto this surface
            return clone;
        }

        private void HandlePictureBoxLeftClick(PictureBox pictureBox, bool allShown, bool skipCommand)
        {
            int imageID;
            String tag = pictureBox.Tag.ToString();
            int.TryParse(tag.Substring(tag.Length - 12, 8), out imageID);

            groupBoxImages.Controls.Remove(pictureBox);
            if (skipCommand)
            {
                return;
            }


            String selectedCommand;
            if (allShown)
            {
                selectedCommand = listBoxLabelAllShown.SelectedItem.ToString();
            }
            else
            {
                selectedCommand = listBoxClickList.SelectedItem.ToString();
            }

            if (selectedCommand == "Delete")
            {
                //File.Delete(label);
            }
            else
            {
                if (radioDates.Checked)
                {
                    int date;
                    int.TryParse(selectedCommand, out date);
                    if (radioDecades.Checked)
                    {
                        date = date - 1900;
                    }
                    if (selectedCommand == "No Label")
                    {
                        date = -1;
                    }
                    ImagesDB.UpdateDate(imageID, date);
                }


            }
        }


        private void HandlePictureBoxRightClick(PictureBox pictureBox)
        {
            //int imageID;
            //String tag = pictureBox.Tag.ToString();
            //int.TryParse(tag.Substring(tag.Length - 12, 8), out imageID);
            groupBoxImages.Controls.Remove(pictureBox);
            frmLabelRotation frm = new frmLabelRotation();
            frm.SingleImageFileName = pictureBox.Tag.ToString();
            frm.Show();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            MouseEventArgs me = (MouseEventArgs)e;
            if (me.Button == System.Windows.Forms.MouseButtons.Right)
            {
                HandlePictureBoxRightClick(pictureBox);
            }
            else
            {
                HandlePictureBoxLeftClick(pictureBox, false, false);
            }
        }

        private void listBoxWorkingLabel_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBoxLabelAllShown.SelectedIndex = listBoxWorkingLabel.SelectedIndex;
            if (listBoxWorkingLabel.SelectedIndex == LastListBoxWorkingLabelSelectedIndex)
            {
                OnlyGetMore();
            }
            else
            {
                ImageRefresh(true);
            }
            LastListBoxWorkingLabelSelectedIndex = listBoxWorkingLabel.SelectedIndex;
        }


        private void cmdLabelAll_Click(object sender, EventArgs e)
        {
            HandleAll(false);
        }

        private void HandleAll(bool skipCommand)
        {
            //A new list is needed because HandlePictureBoxClick is removing items from groupBoxImages.Controls. 
            List<PictureBox> pictureBoxes = new List<PictureBox>();

            foreach (PictureBox pictureBox in groupBoxImages.Controls)
            {
                pictureBoxes.Add(pictureBox);
            }

            foreach (PictureBox pictureBox in pictureBoxes)
            {
                HandlePictureBoxLeftClick(pictureBox, true, skipCommand);
            }
            ImageRefresh(false);
        }

        private void cmdGetMore_Click(object sender, EventArgs e)
        {
            OnlyGetMore();
        }

        private void OnlyGetMore()
        {
            if (images.Count == 0)
            {
                ImageRefresh(true);
            }
            else
            {
                HandleAll(true);
            }
        }

        private void cmdGetMore_KeyPress(object sender, KeyPressEventArgs e)
        {
            //I think this only makes sence to use in Decades mode. 
            if (radioYears.Checked)
            {
                return;
            }

            if (!radioDates.Checked)
            {
                return;
            }


            if (groupBoxImages.Controls.Count == 0)
            {
                return;
            }

            int numberKeyed;


            if (!int.TryParse(e.KeyChar.ToString(), out numberKeyed))
            {
                if (e.KeyChar.ToString() == ".")
                {
                    numberKeyed = -1;
                }
                else
                {
                    return;
                }

            }

            int date;
            if (listBoxWorkingLabel.SelectedItem.ToString() == "No Label")
            {
                date = numberKeyed * 10;
            }
            else
            {
                int selectedDate;
                int.TryParse(listBoxWorkingLabel.SelectedItem.ToString(), out selectedDate);
                date = selectedDate + numberKeyed;
            }

            if (numberKeyed == -1)
            {
                date = 0;
            }

            PictureBox pictureBox = (PictureBox)groupBoxImages.Controls[0];
            int imageID;
            String tag = pictureBox.Tag.ToString();
            int.TryParse(tag.Substring(tag.Length - 12, 8), out imageID);
            groupBoxImages.Controls.Remove(pictureBox);
            ImagesDB.UpdateDate(imageID, date);
        }
        public static Bitmap RotateImage(Image image, PointF offset, float angle)
        {
            if (image == null)
                throw new ArgumentNullException("image");

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
    }
}

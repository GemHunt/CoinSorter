using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using EDSDKLib;
using System.Threading;
using Microsoft.VisualBasic;
namespace WinFormsUI
{
    public partial class MainForm : Form
    {
        #region Variables

        SDKHandler CameraHandler;
        List<int> AvList;
        List<int> TvList;
        List<int> ISOList;
        List<Camera> CamList;
        Bitmap Evf_Bmp;
        int LVBw, LVBh, w, h, imageID = 100 * 1080000;
        DateTime lastImageTime = DateTime.Now;
        float LVBratio, LVration;
        Images images = new Images();


        #endregion

        public MainForm()
        {
            InitializeComponent();
            CameraHandler = new SDKHandler();
            CameraHandler.CameraAdded += new SDKHandler.CameraAddedHandler(SDK_CameraAdded);
            CameraHandler.LiveViewUpdated += new SDKHandler.StreamUpdate(SDK_LiveViewUpdated);
            CameraHandler.ProgressChanged += new SDKHandler.ProgressHandler(SDK_ProgressChanged);
            CameraHandler.CameraHasShutdown += SDK_CameraHasShutdown;
            LVBw = LiveViewPicBox.Width;
            LVBh = LiveViewPicBox.Height;
            RefreshCamera();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CameraHandler != null) CameraHandler.Dispose();
        }

        #region SDK Events

        private void SDK_ProgressChanged(int Progress)
        {
            if (Progress == 100) Progress = 0;
            this.Invoke(new Action(() => { MainProgressBar.Value = Progress; }));
        }

        private void SDK_LiveViewUpdated(Stream img)
        {
            Evf_Bmp = new Bitmap(img);
            
            if (radioShowLiveView.Checked == true)
            {
                using (Graphics g = LiveViewPicBox.CreateGraphics())
                {
                    LVBratio = LVBw / (float)LVBh;
                    LVration = Evf_Bmp.Width / (float)Evf_Bmp.Height;
                    if (LVBratio < LVration)
                    {
                        w = LVBw;
                        h = (int)(LVBw / LVration);
                    }
                    else
                    {
                        w = (int)(LVBh * LVration);
                        h = LVBh;
                    }
                    g.DrawImage(Evf_Bmp, 0, 0, w, h);
                }
            }
            else
            {
                long lastImageMilliSeconds = (DateTime.Now - lastImageTime).Milliseconds;
                lastImageMilliSeconds += (DateTime.Now - lastImageTime).Seconds * 1000;

                if (lastImageMilliSeconds > 650)
                {
                    lastImageTime = DateTime.Now;
                    imageID++;
                    string fullImageName = images.ImageDir + imageID + ".jpg";
                    Evf_Bmp.Save(fullImageName, System.Drawing.Imaging.ImageFormat.Jpeg);
                    images.CallMatlab(fullImageName);
                }
            }
            Evf_Bmp.Dispose();
        }

        private void SDK_CameraAdded()
        {
            RefreshCamera();
        }

        private void SDK_CameraHasShutdown(object sender, EventArgs e)
        {
            CloseSession();
        }

        #endregion

        #region Session

        private void SessionButton_Click(object sender, EventArgs e)
        {
            if (CameraHandler.CameraSessionOpen) CloseSession();
            else OpenSession();
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            RefreshCamera();
        }

        #endregion

        #region Live view

        private void LiveViewButton_Click(object sender, EventArgs e)
        {
            if (!CameraHandler.IsLiveViewOn) { CameraHandler.StartLiveView(); LiveViewButton.Text = "Stop LV"; }
            else { CameraHandler.StopLiveView(); LiveViewButton.Text = "Start LV"; }
        }

        private void LiveViewPicBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (CameraHandler.IsLiveViewOn && CameraHandler.IsCoordSystemSet)
            {
                ushort x = (ushort)((e.X / (double)LiveViewPicBox.Width) * CameraHandler.Evf_CoordinateSystem.width);
                ushort y = (ushort)((e.Y / (double)LiveViewPicBox.Height) * CameraHandler.Evf_CoordinateSystem.height);
                CameraHandler.SetManualWBEvf(x, y);
            }
        }
        
        private void LiveViewPicBox_SizeChanged(object sender, EventArgs e)
        {
            LVBw = LiveViewPicBox.Width;
            LVBh = LiveViewPicBox.Height;
        }

        private void FocusNear3Button_Click(object sender, EventArgs e)
        {
            CameraHandler.SetFocus(EDSDK.EvfDriveLens_Near3);
        }

        private void FocusNear2Button_Click(object sender, EventArgs e)
        {
            CameraHandler.SetFocus(EDSDK.EvfDriveLens_Near2);
        }

        private void FocusNear1Button_Click(object sender, EventArgs e)
        {
            CameraHandler.SetFocus(EDSDK.EvfDriveLens_Near1);
        }

        private void FocusFar1Button_Click(object sender, EventArgs e)
        {
            CameraHandler.SetFocus(EDSDK.EvfDriveLens_Far1);
        }

        private void FocusFar2Button_Click(object sender, EventArgs e)
        {
            CameraHandler.SetFocus(EDSDK.EvfDriveLens_Far2);
        }

        private void FocusFar3Button_Click(object sender, EventArgs e)
        {
            CameraHandler.SetFocus(EDSDK.EvfDriveLens_Far3);
        }

        #endregion

        #region Settings

        private void TakePhotoButton_Click(object sender, EventArgs e)
        {
            if ((string)TvCoBox.SelectedItem == "Bulb") CameraHandler.TakePhoto((uint)BulbUpDo.Value);
            else CameraHandler.TakePhoto();
        }

        
    
        private void AvCoBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CameraHandler.SetSetting(EDSDK.PropID_Av, CameraValues.AV((string)AvCoBox.SelectedItem));
        }

        private void TvCoBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CameraHandler.SetSetting(EDSDK.PropID_Tv, CameraValues.TV((string)TvCoBox.SelectedItem));
            if ((string)TvCoBox.SelectedItem == "Bulb") BulbUpDo.Enabled = true;
            else BulbUpDo.Enabled = false;
        }

        private void ISOCoBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            CameraHandler.SetSetting(EDSDK.PropID_ISOSpeed, CameraValues.ISO((string)ISOCoBox.SelectedItem));
        }

        private void WBCoBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (WBCoBox.SelectedIndex)
            {
                case 0: CameraHandler.SetSetting(EDSDK.PropID_WhiteBalance, EDSDK.WhiteBalance_Auto); break;
                case 1: CameraHandler.SetSetting(EDSDK.PropID_WhiteBalance, EDSDK.WhiteBalance_Daylight); break;
                case 2: CameraHandler.SetSetting(EDSDK.PropID_WhiteBalance, EDSDK.WhiteBalance_Cloudy); break;
                case 3: CameraHandler.SetSetting(EDSDK.PropID_WhiteBalance, EDSDK.WhiteBalance_Tangsten); break;
                case 4: CameraHandler.SetSetting(EDSDK.PropID_WhiteBalance, EDSDK.WhiteBalance_Fluorescent); break;
                case 5: CameraHandler.SetSetting(EDSDK.PropID_WhiteBalance, EDSDK.WhiteBalance_Strobe); break;
                case 6: CameraHandler.SetSetting(EDSDK.PropID_WhiteBalance, EDSDK.WhiteBalance_WhitePaper); break;
                case 7: CameraHandler.SetSetting(EDSDK.PropID_WhiteBalance, EDSDK.WhiteBalance_Shade); break;
            }
        }

      

        #endregion

        #region Subroutines

        private void CloseSession()
        {
            CameraHandler.CloseSession();
            AvCoBox.Items.Clear();
            TvCoBox.Items.Clear();
            ISOCoBox.Items.Clear();
            SettingsGroupBox.Enabled = false;
            LiveViewGroupBox.Enabled = false;
            SessionButton.Text = "Open Session";
            SessionLabel.Text = "No open session";
            RefreshCamera();//Closing the session invalidates the current camera pointer
        }

        private void RefreshCamera()
        {
            if (CameraHandler.CameraSessionOpen) CloseSession();
            CameraListBox.Items.Clear();
            CamList = CameraHandler.GetCameraList();
            foreach (Camera cam in CamList) CameraListBox.Items.Add(cam.Info.szDeviceDescription);
            if (CamList.Count > 0) CameraListBox.SelectedIndex = 0;
        }

        private void OpenSession()
        {
            if (CameraListBox.SelectedIndex >= 0)
            {
                CameraHandler.OpenSession(CamList[CameraListBox.SelectedIndex]);
                SessionButton.Text = "Close Session";
                string cameraname = CameraHandler.MainCamera.Info.szDeviceDescription;
                SessionLabel.Text = cameraname;
                if (CameraHandler.GetSetting(EDSDK.PropID_AEMode) != EDSDK.AEMode_Manual) MessageBox.Show("Camera is not in manual mode. Some features might not work!");
                AvList = CameraHandler.GetSettingsList((uint)EDSDK.PropID_Av);
                TvList = CameraHandler.GetSettingsList((uint)EDSDK.PropID_Tv);
                ISOList = CameraHandler.GetSettingsList((uint)EDSDK.PropID_ISOSpeed);
                foreach (int Av in AvList) AvCoBox.Items.Add(CameraValues.AV((uint)Av));
                foreach (int Tv in TvList) TvCoBox.Items.Add(CameraValues.TV((uint)Tv));
                foreach (int ISO in ISOList) ISOCoBox.Items.Add(CameraValues.ISO((uint)ISO));
                AvCoBox.SelectedIndex = AvCoBox.Items.IndexOf(CameraValues.AV((uint)CameraHandler.GetSetting((uint)EDSDK.PropID_Av)));
                TvCoBox.SelectedIndex = TvCoBox.Items.IndexOf(CameraValues.TV((uint)CameraHandler.GetSetting((uint)EDSDK.PropID_Tv)));
                ISOCoBox.SelectedIndex = ISOCoBox.Items.IndexOf(CameraValues.ISO((uint)CameraHandler.GetSetting((uint)EDSDK.PropID_ISOSpeed)));
                int wbidx = (int)CameraHandler.GetSetting((uint)EDSDK.PropID_WhiteBalance);
                switch (wbidx)
                {
                    case EDSDK.WhiteBalance_Auto: WBCoBox.SelectedIndex = 0; break;
                    case EDSDK.WhiteBalance_Daylight: WBCoBox.SelectedIndex = 1; break;
                    case EDSDK.WhiteBalance_Cloudy: WBCoBox.SelectedIndex = 2; break;
                    case EDSDK.WhiteBalance_Tangsten: WBCoBox.SelectedIndex = 3; break;
                    case EDSDK.WhiteBalance_Fluorescent: WBCoBox.SelectedIndex = 4; break;
                    case EDSDK.WhiteBalance_Strobe: WBCoBox.SelectedIndex = 5; break;
                    case EDSDK.WhiteBalance_WhitePaper: WBCoBox.SelectedIndex = 6; break;
                    case EDSDK.WhiteBalance_Shade: WBCoBox.SelectedIndex = 7; break;
                    default: WBCoBox.SelectedIndex = -1; break;
                }
                SettingsGroupBox.Enabled = true;
                LiveViewGroupBox.Enabled = true;
            }
        }

        #endregion

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}

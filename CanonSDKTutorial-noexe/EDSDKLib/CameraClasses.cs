using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Globalization;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace EDSDKLib
{
    public class SDKHandler : IDisposable
    {
        #region Variables

        /// <summary>
        /// The used camera
        /// </summary>
        public Camera MainCamera { get; private set; }
        /// <summary>
        /// States if a session with the MainCamera is opened
        /// </summary>
        public bool CameraSessionOpen { get; private set; }
        /// <summary>
        /// States if the live view is on or not
        /// </summary>
        public bool IsLiveViewOn { get; private set; }
        /// <summary>
        /// States if camera is recording or not
        /// </summary>
        public bool IsFilming { get; private set; }
        /// <summary>
        /// Directory to where photos will be saved
        /// </summary>
        public string ImageSaveDirectory { get; set; }
        /// <summary>
        /// The focus and zoom border rectangle for live view (set after first use of live view)
        /// </summary>
        public EDSDK.EdsRect Evf_ZoomRect { get; private set; }
        /// <summary>
        /// The focus and zoom border position of the live view (set after first use of live view)
        /// </summary>
        public EDSDK.EdsPoint Evf_ZoomPosition { get; private set; }
        /// <summary>
        /// The cropping position of the enlarged live view image (set after first use of live view)
        /// </summary>
        public EDSDK.EdsPoint Evf_ImagePosition { get; private set; }
        /// <summary>
        /// The live view coordinate system (set after first use of live view)
        /// </summary>
        public EDSDK.EdsSize Evf_CoordinateSystem { get; private set; }
        /// <summary>
        /// States if the Evf_CoordinateSystem is already set
        /// </summary>
        public bool IsCoordSystemSet = false;
        /// <summary>
        /// Handles errors that happen with the SDK
        /// </summary>
        public uint Error
        {
            get { return EDSDK.EDS_ERR_OK; }
            set { if (value != EDSDK.EDS_ERR_OK) throw new Exception("SDK Error: " + value); }
        }


        /// <summary>
        /// States if a finished video should be downloaded from the camera
        /// </summary>
        private bool DownloadVideo;
        /// <summary>
        /// For video recording, SaveTo has to be set to Camera. This is to store the previous setting until after the filming.
        /// </summary>
        private uint PrevSaveTo;
        /// <summary>
        /// The thread on which the live view images will get downloaded continuously
        /// </summary>
        private Thread LVThread;
        /// <summary>
        /// If true, the live view will be shut off completely. If false, live view will go back to the camera.
        /// </summary>
        private bool LVoff;

        #endregion

        #region Events

        #region SDK Events

        public event EDSDK.EdsCameraAddedHandler SDKCameraAddedEvent;
        public event EDSDK.EdsObjectEventHandler SDKObjectEvent;
        public event EDSDK.EdsProgressCallback SDKProgressCallbackEvent;
        public event EDSDK.EdsPropertyEventHandler SDKPropertyEvent;
        public event EDSDK.EdsStateEventHandler SDKStateEvent;

        #endregion

        #region Custom Events

        public delegate void CameraAddedHandler();
        public delegate void ProgressHandler(int Progress);
        public delegate void StreamUpdate(Stream img);
        public delegate void BitmapUpdate(Bitmap bmp);

        /// <summary>
        /// Fires if a camera is added
        /// </summary>
        public event CameraAddedHandler CameraAdded;
        /// <summary>
        /// Fires if any process reports progress
        /// </summary>
        public event ProgressHandler ProgressChanged;
        /// <summary>
        /// Fires if the live view image has been updated
        /// </summary>
        public event StreamUpdate LiveViewUpdated;
        /// <summary>
        /// If the camera is disconnected or shuts down, this event is fired
        /// </summary>
        public event EventHandler CameraHasShutdown;
        /// <summary>
        /// If an image is downloaded, this event fires with the downloaded image.
        /// </summary>
        public event BitmapUpdate ImageDownloaded;

        #endregion

        #endregion

        #region Basic SDK and Session handling

        /// <summary>
        /// Initializes the SDK and adds events
        /// </summary>
        public SDKHandler()
        {
            //initialize SDK
            Error = EDSDK.EdsInitializeSDK();
            STAThread.Init();
            //subscribe to camera added event (the C# event and the SDK event)
            SDKCameraAddedEvent += new EDSDK.EdsCameraAddedHandler(SDKHandler_CameraAddedEvent);
            EDSDK.EdsSetCameraAddedHandler(SDKCameraAddedEvent, IntPtr.Zero);

            //subscribe to the camera events (for the C# events)
            SDKStateEvent += new EDSDK.EdsStateEventHandler(Camera_SDKStateEvent);
            SDKPropertyEvent += new EDSDK.EdsPropertyEventHandler(Camera_SDKPropertyEvent);
            SDKProgressCallbackEvent += new EDSDK.EdsProgressCallback(Camera_SDKProgressCallbackEvent);
            SDKObjectEvent += new EDSDK.EdsObjectEventHandler(Camera_SDKObjectEvent);
        }
        
        /// <summary>
        /// Get a list of all connected cameras
        /// </summary>
        /// <returns>The camera list</returns>
        public List<Camera> GetCameraList()
        {
            IntPtr camlist;
            //get list of cameras
            Error = EDSDK.EdsGetCameraList(out camlist);

            //get each camera from camlist
            int c;
            //get amount of connected cameras
            Error = EDSDK.EdsGetChildCount(camlist, out c);
            List<Camera> OutCamList = new List<Camera>();
            for (int i = 0; i < c; i++)
            {
                IntPtr cptr;
                //get pointer to camera at index i
                Error = EDSDK.EdsGetChildAtIndex(camlist, i, out cptr);
                OutCamList.Add(new Camera(cptr));
            }
            return OutCamList;
        }

        /// <summary>
        /// Opens a session with given camera
        /// </summary>
        /// <param name="newCamera">The camera which will be used</param>
        public void OpenSession(Camera newCamera)
        {
            if (CameraSessionOpen) CloseSession();
            if (newCamera != null)
            {
                MainCamera = newCamera;
                //open a session
                SendSDKCommand(delegate { Error = EDSDK.EdsOpenSession(MainCamera.Ref); });
                //subscribe to the camera events (for the SDK)
                EDSDK.EdsSetCameraStateEventHandler(MainCamera.Ref, EDSDK.StateEvent_All, SDKStateEvent, IntPtr.Zero);
                EDSDK.EdsSetObjectEventHandler(MainCamera.Ref, EDSDK.ObjectEvent_All, SDKObjectEvent, IntPtr.Zero);
                EDSDK.EdsSetPropertyEventHandler(MainCamera.Ref, EDSDK.PropertyEvent_All, SDKPropertyEvent, IntPtr.Zero);
                CameraSessionOpen = true;
            }
        }

        /// <summary>
        /// Closes the session with the current camera
        /// </summary>
        public void CloseSession()
        {
            if (CameraSessionOpen)
            {
                //if live view is still on, stop it and wait till the thread has stopped
                if (IsLiveViewOn)
                {
                    StopLiveView();
                    LVThread.Join(1000);
                }

                //close session and release camera
                SendSDKCommand(delegate { Error = EDSDK.EdsCloseSession(MainCamera.Ref); });
                uint c = EDSDK.EdsRelease(MainCamera.Ref);
                CameraSessionOpen = false;
            }
        }

        /// <summary>
        /// Closes open session and terminates the SDK
        /// </summary>
        public void Dispose()
        {
            //close session
            CloseSession();
            //terminate SDK
            Error = EDSDK.EdsTerminateSDK();
            //stop command execution thread
            STAThread.Shutdown();
        }

        #endregion

        #region Eventhandling

        /// <summary>
        /// A new camera was plugged into the computer
        /// </summary>
        /// <param name="inContext">The pointer to the added camera</param>
        /// <returns>An EDSDK errorcode</returns>
        private uint SDKHandler_CameraAddedEvent(IntPtr inContext)
        {
            //Handle new camera here
            if (CameraAdded != null) CameraAdded();
            return EDSDK.EDS_ERR_OK;
        }

        /// <summary>
        /// An Objectevent fired
        /// </summary>
        /// <param name="inEvent">The ObjectEvent id</param>
        /// <param name="inRef">Pointer to the object</param>
        /// <param name="inContext"></param>
        /// <returns>An EDSDK errorcode</returns>
        private uint Camera_SDKObjectEvent(uint inEvent, IntPtr inRef, IntPtr inContext)
        {
            //handle object event here
            switch (inEvent)
            {
                case EDSDK.ObjectEvent_All:
                    break;
                case EDSDK.ObjectEvent_DirItemCancelTransferDT:
                    break;
                case EDSDK.ObjectEvent_DirItemContentChanged:
                    break;
                case EDSDK.ObjectEvent_DirItemCreated:
                    if (DownloadVideo) { DownloadImage(inRef, ImageSaveDirectory); DownloadVideo = false; }
                    break;
                case EDSDK.ObjectEvent_DirItemInfoChanged:
                    break;
                case EDSDK.ObjectEvent_DirItemRemoved:
                    break;
                case EDSDK.ObjectEvent_DirItemRequestTransfer:
                    DownloadImage(inRef, ImageSaveDirectory);
                    break;
                case EDSDK.ObjectEvent_DirItemRequestTransferDT:
                    break;
                case EDSDK.ObjectEvent_FolderUpdateItems:
                    break;
                case EDSDK.ObjectEvent_VolumeAdded:
                    break;
                case EDSDK.ObjectEvent_VolumeInfoChanged:
                    break;
                case EDSDK.ObjectEvent_VolumeRemoved:
                    break;
                case EDSDK.ObjectEvent_VolumeUpdateItems:
                    break;
            }

            return EDSDK.EDS_ERR_OK;
        }

        /// <summary>
        /// A progress was made
        /// </summary>
        /// <param name="inPercent">Percent of progress</param>
        /// <param name="inContext">...</param>
        /// <param name="outCancel">Set true to cancel event</param>
        /// <returns>An EDSDK errorcode</returns>
        private uint Camera_SDKProgressCallbackEvent(uint inPercent, IntPtr inContext, ref bool outCancel)
        {
            //Handle progress here
            if (ProgressChanged != null) ProgressChanged((int)inPercent);
            return EDSDK.EDS_ERR_OK;
        }

        /// <summary>
        /// A property changed
        /// </summary>
        /// <param name="inEvent">The PropetyEvent ID</param>
        /// <param name="inPropertyID">The Property ID</param>
        /// <param name="inParameter">Event Parameter</param>
        /// <param name="inContext">...</param>
        /// <returns>An EDSDK errorcode</returns>
        private uint Camera_SDKPropertyEvent(uint inEvent, uint inPropertyID, uint inParameter, IntPtr inContext)
        {
            //Handle property event here
            switch (inEvent)
            {
                case EDSDK.PropertyEvent_All:
                    break;
                case EDSDK.PropertyEvent_PropertyChanged:
                    break;
                case EDSDK.PropertyEvent_PropertyDescChanged:
                    break;
            }

            switch (inPropertyID)
            {
                case EDSDK.PropID_AEBracket:
                    break;
                case EDSDK.PropID_AEMode:
                    break;
                case EDSDK.PropID_AEModeSelect:
                    break;
                case EDSDK.PropID_AFMode:
                    break;
                case EDSDK.PropID_Artist:
                    break;
                case EDSDK.PropID_AtCapture_Flag:
                    break;
                case EDSDK.PropID_Av:
                    break;
                case EDSDK.PropID_AvailableShots:
                    break;
                case EDSDK.PropID_BatteryLevel:
                    break;
                case EDSDK.PropID_BatteryQuality:
                    break;
                case EDSDK.PropID_BodyIDEx:
                    break;
                case EDSDK.PropID_Bracket:
                    break;
                case EDSDK.PropID_CFn:
                    break;
                case EDSDK.PropID_ClickWBPoint:
                    break;
                case EDSDK.PropID_ColorMatrix:
                    break;
                case EDSDK.PropID_ColorSaturation:
                    break;
                case EDSDK.PropID_ColorSpace:
                    break;
                case EDSDK.PropID_ColorTemperature:
                    break;
                case EDSDK.PropID_ColorTone:
                    break;
                case EDSDK.PropID_Contrast:
                    break;
                case EDSDK.PropID_Copyright:
                    break;
                case EDSDK.PropID_DateTime:
                    break;
                case EDSDK.PropID_DepthOfField:
                    break;
                case EDSDK.PropID_DigitalExposure:
                    break;
                case EDSDK.PropID_DriveMode:
                    break;
                case EDSDK.PropID_EFCompensation:
                    break;
                case EDSDK.PropID_Evf_AFMode:
                    break;
                case EDSDK.PropID_Evf_ColorTemperature:
                    break;
                case EDSDK.PropID_Evf_DepthOfFieldPreview:
                    break;
                case EDSDK.PropID_Evf_FocusAid:
                    break;
                case EDSDK.PropID_Evf_Histogram:
                    break;
                case EDSDK.PropID_Evf_HistogramStatus:
                    break;
                case EDSDK.PropID_Evf_ImagePosition:
                    break;
                case EDSDK.PropID_Evf_Mode:
                    break;
                case EDSDK.PropID_Evf_OutputDevice:
                    if (IsLiveViewOn == true) DownloadEvf();
                    break;
                case EDSDK.PropID_Evf_WhiteBalance:
                    break;
                case EDSDK.PropID_Evf_Zoom:
                    break;
                case EDSDK.PropID_Evf_ZoomPosition:
                    break;
                case EDSDK.PropID_ExposureCompensation:
                    break;
                case EDSDK.PropID_FEBracket:
                    break;
                case EDSDK.PropID_FilterEffect:
                    break;
                case EDSDK.PropID_FirmwareVersion:
                    break;
                case EDSDK.PropID_FlashCompensation:
                    break;
                case EDSDK.PropID_FlashMode:
                    break;
                case EDSDK.PropID_FlashOn:
                    break;
                case EDSDK.PropID_FocalLength:
                    break;
                case EDSDK.PropID_FocusInfo:
                    break;
                case EDSDK.PropID_GPSAltitude:
                    break;
                case EDSDK.PropID_GPSAltitudeRef:
                    break;
                case EDSDK.PropID_GPSDateStamp:
                    break;
                case EDSDK.PropID_GPSLatitude:
                    break;
                case EDSDK.PropID_GPSLatitudeRef:
                    break;
                case EDSDK.PropID_GPSLongitude:
                    break;
                case EDSDK.PropID_GPSLongitudeRef:
                    break;
                case EDSDK.PropID_GPSMapDatum:
                    break;
                case EDSDK.PropID_GPSSatellites:
                    break;
                case EDSDK.PropID_GPSStatus:
                    break;
                case EDSDK.PropID_GPSTimeStamp:
                    break;
                case EDSDK.PropID_GPSVersionID:
                    break;
                case EDSDK.PropID_HDDirectoryStructure:
                    break;
                case EDSDK.PropID_ICCProfile:
                    break;
                case EDSDK.PropID_ImageQuality:
                    break;
                case EDSDK.PropID_ISOBracket:
                    break;
                case EDSDK.PropID_ISOSpeed:
                    break;
                case EDSDK.PropID_JpegQuality:
                    break;
                case EDSDK.PropID_LensName:
                    break;
                case EDSDK.PropID_LensStatus:
                    break;
                case EDSDK.PropID_Linear:
                    break;
                case EDSDK.PropID_MakerName:
                    break;
                case EDSDK.PropID_MeteringMode:
                    break;
                case EDSDK.PropID_NoiseReduction:
                    break;
                case EDSDK.PropID_Orientation:
                    break;
                case EDSDK.PropID_OwnerName:
                    break;
                case EDSDK.PropID_ParameterSet:
                    break;
                case EDSDK.PropID_PhotoEffect:
                    break;
                case EDSDK.PropID_PictureStyle:
                    break;
                case EDSDK.PropID_PictureStyleCaption:
                    break;
                case EDSDK.PropID_PictureStyleDesc:
                    break;
                case EDSDK.PropID_ProductName:
                    break;
                case EDSDK.PropID_Record:
                    break;
                case EDSDK.PropID_RedEye:
                    break;
                case EDSDK.PropID_SaveTo:
                    break;
                case EDSDK.PropID_Sharpness:
                    break;
                case EDSDK.PropID_ToneCurve:
                    break;
                case EDSDK.PropID_ToningEffect:
                    break;
                case EDSDK.PropID_Tv:
                    break;
                case EDSDK.PropID_Unknown:
                    break;
                case EDSDK.PropID_WBCoeffs:
                    break;
                case EDSDK.PropID_WhiteBalance:
                    break;
                case EDSDK.PropID_WhiteBalanceBracket:
                    break;
                case EDSDK.PropID_WhiteBalanceShift:
                    break;
            }
            return EDSDK.EDS_ERR_OK;
        }

        /// <summary>
        /// The camera state changed
        /// </summary>
        /// <param name="inEvent">The StateEvent ID</param>
        /// <param name="inParameter">Parameter from this event</param>
        /// <param name="inContext">...</param>
        /// <returns>An EDSDK errorcode</returns>
        private uint Camera_SDKStateEvent(uint inEvent, uint inParameter, IntPtr inContext)
        {
            //Handle state event here
            switch (inEvent)
            {
                case EDSDK.StateEvent_All:
                    break;
                case EDSDK.StateEvent_AfResult:
                    break;
                case EDSDK.StateEvent_BulbExposureTime:
                    break;
                case EDSDK.StateEvent_CaptureError:
                    break;
                case EDSDK.StateEvent_InternalError:
                    break;
                case EDSDK.StateEvent_JobStatusChanged:
                    break;
                case EDSDK.StateEvent_Shutdown:
                    CameraSessionOpen = false;
                    if (CameraHasShutdown != null) CameraHasShutdown(this, new EventArgs());
                    break;
                case EDSDK.StateEvent_ShutDownTimerUpdate:
                    break;
                case EDSDK.StateEvent_WillSoonShutDown:
                    break;
            }
            return EDSDK.EDS_ERR_OK;
        }

        #endregion

        #region Camera commands

        #region Download data

        /// <summary>
        /// Downloads an image to given directory
        /// </summary>
        /// <param name="ObjectPointer">Pointer to the object. Get it from the SDKObjectEvent.</param>
        /// <param name="directory">Path to where the image will be saved to</param>
        public void DownloadImage(IntPtr ObjectPointer, string directory)
        {
            EDSDK.EdsDirectoryItemInfo dirInfo;
            IntPtr streamRef;
            //get information about object
            Error = EDSDK.EdsGetDirectoryItemInfo(ObjectPointer, out dirInfo);
            string CurrentPhoto = Path.Combine(directory, dirInfo.szFileName);

            SendSDKCommand(delegate
            {
                //create filestream to data
                Error = EDSDK.EdsCreateFileStream(CurrentPhoto, EDSDK.EdsFileCreateDisposition.CreateAlways, EDSDK.EdsAccess.ReadWrite, out streamRef);
                //download file
                lock (STAThread.ExecLock) { DownloadData(ObjectPointer, streamRef); }
                //release stream
                Error = EDSDK.EdsRelease(streamRef);
            }, true);
        }

        /// <summary>
        /// Downloads a jpg image from the camera into a Bitmap. Fires the ImageDownloaded event when done.
        /// </summary>
        /// <param name="ObjectPointer">Pointer to the object. Get it from the SDKObjectEvent.</param>
        public void DownloadImage(IntPtr ObjectPointer)
        {
            //get information about image
            EDSDK.EdsDirectoryItemInfo dirInfo = new EDSDK.EdsDirectoryItemInfo();
            Error = EDSDK.EdsGetDirectoryItemInfo(ObjectPointer, out dirInfo);

            //check the extension. Raw data cannot be read by the bitmap class
            string ext = Path.GetExtension(dirInfo.szFileName).ToLower();
            if (ext == ".jpg" || ext == ".jpeg")
            {
                SendSDKCommand(delegate
                {
                    Bitmap bmp = null;
                    IntPtr streamRef, jpgPointer = IntPtr.Zero;
                    uint length = 0;

                    //create memory stream
                    Error = EDSDK.EdsCreateMemoryStream(dirInfo.Size, out streamRef);

                     //download data to the stream
                     lock (STAThread.ExecLock) { DownloadData(ObjectPointer, streamRef); }
                     Error = EDSDK.EdsGetPointer(streamRef, out jpgPointer);
                     Error = EDSDK.EdsGetLength(streamRef, out length);

                     unsafe
                     {
                         //create a System.IO.Stream from the pointer
                         using (UnmanagedMemoryStream ums = new UnmanagedMemoryStream((byte*)jpgPointer.ToPointer(), length, length, FileAccess.Read))
                         {
                             //create bitmap from stream (it's a normal jpeg image)
                             bmp = new Bitmap(ums);
                         }
                     }

                     //release data
                     Error = EDSDK.EdsRelease(streamRef);

                    //Fire the event with the image
                     if (ImageDownloaded != null) ImageDownloaded(bmp);
                 }, true);
            }
            else
            {
                //if it's a RAW image, cancel the download and release the image
                SendSDKCommand(delegate { Error = EDSDK.EdsDownloadCancel(ObjectPointer); });
                Error = EDSDK.EdsRelease(ObjectPointer);
            }
        }

        /// <summary>
        /// Gets the thumbnail of an image (can be raw or jpg)
        /// </summary>
        /// <param name="filepath">The filename of the image</param>
        /// <returns>The thumbnail of the image</returns>
        public Bitmap GetFileThumb(string filepath)
        {
            IntPtr stream;
            //create a filestream to given file
            Error = EDSDK.EdsCreateFileStream(filepath, EDSDK.EdsFileCreateDisposition.OpenExisting, EDSDK.EdsAccess.Read, out stream);
            return GetImage(stream, EDSDK.EdsImageSource.Thumbnail);
        }

        /// <summary>
        /// Downloads data from the camera
        /// </summary>
        /// <param name="ObjectPointer">Pointer to the object</param>
        /// <param name="stream">Pointer to the stream created in advance</param>
        private void DownloadData(IntPtr ObjectPointer, IntPtr stream)
        {
            //get information about the object
            EDSDK.EdsDirectoryItemInfo dirInfo;
            Error = EDSDK.EdsGetDirectoryItemInfo(ObjectPointer, out dirInfo);

            try
            {
                //set progress event
                Error = EDSDK.EdsSetProgressCallback(stream, SDKProgressCallbackEvent, EDSDK.EdsProgressOption.Periodically, ObjectPointer);
                //download the data
                Error = EDSDK.EdsDownload(ObjectPointer, dirInfo.Size, stream);
            }
            finally
            {
                //set the download as complete
                Error = EDSDK.EdsDownloadComplete(ObjectPointer);
                //release object
                Error = EDSDK.EdsRelease(ObjectPointer);
            }
        }

        /// <summary>
        /// Creates a Bitmap out of a stream
        /// </summary>
        /// <param name="img_stream">Image stream</param>
        /// <param name="imageSource">Type of image</param>
        /// <returns>The bitmap from the stream</returns>
        private Bitmap GetImage(IntPtr img_stream, EDSDK.EdsImageSource imageSource)
        {
            IntPtr stream = IntPtr.Zero;
            IntPtr img_ref = IntPtr.Zero;
            IntPtr streamPointer = IntPtr.Zero;
            EDSDK.EdsImageInfo imageInfo;

            try
            {
                //create reference and get image info
                Error = EDSDK.EdsCreateImageRef(img_stream, out img_ref);
                Error = EDSDK.EdsGetImageInfo(img_ref, imageSource, out imageInfo);

                EDSDK.EdsSize outputSize = new EDSDK.EdsSize();
                outputSize.width = imageInfo.EffectiveRect.width;
                outputSize.height = imageInfo.EffectiveRect.height;
                //calculate amount of data
                int datalength = outputSize.height * outputSize.width * 3;
                //create buffer that stores the image
                byte[] buffer = new byte[datalength];
                //create a stream to the buffer
                Error = EDSDK.EdsCreateMemoryStreamFromPointer(buffer, (uint)datalength, out stream);
                //load image into the buffer
                Error = EDSDK.EdsGetImage(img_ref, imageSource, EDSDK.EdsTargetImageType.RGB, imageInfo.EffectiveRect, outputSize, stream);

                //create output bitmap
                Bitmap bmp = new Bitmap(outputSize.width, outputSize.height, PixelFormat.Format24bppRgb);

                //assign values to bitmap and make BGR from RGB (System.Drawing (i.e. GDI+) uses BGR)
                unsafe
                {
                    BitmapData data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, bmp.PixelFormat);

                    byte* outPix = (byte*)data.Scan0;
                    fixed (byte* inPix = buffer)
                    {
                        for (int i = 0; i < datalength; i += 3)
                        {
                            outPix[i] = inPix[i + 2];//Set B value with R value
                            outPix[i + 1] = inPix[i + 1];//Set G value
                            outPix[i + 2] = inPix[i];//Set R value with B value
                        }
                    }
                    bmp.UnlockBits(data);
                }

                return bmp;
            }
            finally
            {
                //Release all data
                if (img_stream != IntPtr.Zero) EDSDK.EdsRelease(img_stream);
                if (img_ref != IntPtr.Zero) EDSDK.EdsRelease(img_ref);
                if (stream != IntPtr.Zero) EDSDK.EdsRelease(stream);
            }
        }

        #endregion

        #region Get Settings

        /// <summary>
        /// Gets the list of possible values for the current camera to set.
        /// Only the PropertyIDs "AEModeSelect", "ISO", "Av", "Tv", "MeteringMode" and "ExposureCompensation" are allowed.
        /// </summary>
        /// <param name="PropID">The property ID</param>
        /// <returns>A list of available values for the given property ID</returns>
        public List<int> GetSettingsList(uint PropID)
        {
            if (MainCamera.Ref != IntPtr.Zero)
            {
                //a list of settings can only be retrieved for following properties
                if (PropID == EDSDK.PropID_AEModeSelect || PropID == EDSDK.PropID_ISOSpeed || PropID == EDSDK.PropID_Av
                    || PropID == EDSDK.PropID_Tv || PropID == EDSDK.PropID_MeteringMode || PropID == EDSDK.PropID_ExposureCompensation)
                {
                    //get the list of possible values
                    EDSDK.EdsPropertyDesc des = new EDSDK.EdsPropertyDesc();
                    Error = EDSDK.EdsGetPropertyDesc(MainCamera.Ref, PropID, out des);
                    return des.PropDesc.Take(des.NumElements).ToList();
                }
                else throw new ArgumentException("Method cannot be used with this Property ID");
            }
            else { throw new ArgumentNullException("Camera or camera reference is null/zero"); }
        }

        /// <summary>
        /// Gets the current setting of given property ID as an uint
        /// </summary>
        /// <param name="PropID">The property ID</param>
        /// <returns>The current setting of the camera</returns>
        public uint GetSetting(uint PropID)
        {
            if (MainCamera.Ref != IntPtr.Zero)
            {
                uint property = 0;
                Error = EDSDK.EdsGetPropertyData(MainCamera.Ref, PropID, 0, out property);
                return property;
            }
            else { throw new ArgumentNullException("Camera or camera reference is null/zero"); }
        }

        /// <summary>
        /// Gets the current setting of given property ID as a string
        /// </summary>
        /// <param name="PropID">The property ID</param>
        /// <returns>The current setting of the camera</returns>
        public string GetStringSetting(uint PropID)
        {
            if (MainCamera.Ref != IntPtr.Zero)
            {
                string data = String.Empty;
                EDSDK.EdsGetPropertyData(MainCamera.Ref, PropID, 0, out data);
                return data;
            }
            else { throw new ArgumentNullException("Camera or camera reference is null/zero"); }
        }

        /// <summary>
        /// Gets the current setting of given property ID as a struct
        /// </summary>
        /// <param name="PropID">The property ID</param>
        /// <typeparam name="T">One of the EDSDK structs</typeparam>
        /// <returns>The current setting of the camera</returns>
        public T GetStructSetting<T>(uint PropID) where T : struct
        {
            if (MainCamera.Ref != IntPtr.Zero)
            {
                //get type and size of struct
                Type structureType = typeof(T);
                int bufferSize = Marshal.SizeOf(structureType);

                //allocate memory
                IntPtr ptr = Marshal.AllocHGlobal(bufferSize);
                //retrieve value
                Error = EDSDK.EdsGetPropertyData(MainCamera.Ref, PropID, 0, bufferSize, ptr);

                try
                {
                    //convert pointer to managed structure
                    T data = (T)Marshal.PtrToStructure(ptr, structureType);
                    return data;
                }
                finally
                {
                    if (ptr != IntPtr.Zero)
                    {
                        //free the allocated memory
                        Marshal.FreeHGlobal(ptr);
                        ptr = IntPtr.Zero;
                    }
                }
            }
            else { throw new ArgumentNullException("Camera or camera reference is null/zero"); }
        }

        #endregion

        #region Set Settings

        /// <summary>
        /// Sets an uint value for the given property ID
        /// </summary>
        /// <param name="PropID">The property ID</param>
        /// <param name="Value">The value which will be set</param>
        public void SetSetting(uint PropID, uint Value)
        {
            if (MainCamera.Ref != IntPtr.Zero)
            {
                SendSDKCommand(delegate
                {
                    int propsize;
                    EDSDK.EdsDataType proptype;
                    //get size of property
                    Error = EDSDK.EdsGetPropertySize(MainCamera.Ref, PropID, 0, out proptype, out propsize);
                    //set given property
                    Error = EDSDK.EdsSetPropertyData(MainCamera.Ref, PropID, 0, propsize, Value);
                });
            }
            else { throw new ArgumentNullException("Camera or camera reference is null/zero"); }
        }

        /// <summary>
        /// Sets a string value for the given property ID
        /// </summary>
        /// <param name="PropID">The property ID</param>
        /// <param name="Value">The value which will be set</param>
        public void SetStringSetting(uint PropID, string Value)
        {
            if (MainCamera.Ref != IntPtr.Zero)
            {
                if (Value == null) throw new ArgumentNullException("String must not be null");

                //convert string to byte array
                byte[] propertyValueBytes = System.Text.Encoding.ASCII.GetBytes(Value + '\0');
                int propertySize = propertyValueBytes.Length;

                //check size of string
                if (propertySize > 32) throw new ArgumentOutOfRangeException("Value must be smaller than 32 bytes");

                //set value
                SendSDKCommand(delegate { Error = EDSDK.EdsSetPropertyData(MainCamera.Ref, PropID, 0, 32, propertyValueBytes); });
            }
            else { throw new ArgumentNullException("Camera or camera reference is null/zero"); }
        }

        /// <summary>
        /// Sets a struct value for the given property ID
        /// </summary>
        /// <param name="PropID">The property ID</param>
        /// <param name="Value">The value which will be set</param>
        public void SetStructSetting<T>(uint PropID, T Value) where T : struct
        {
            if (MainCamera.Ref != IntPtr.Zero)
            {
                SendSDKCommand(delegate { Error = EDSDK.EdsSetPropertyData(MainCamera.Ref, PropID, 0, Marshal.SizeOf(typeof(T)), Value); });
            }
            else { throw new ArgumentNullException("Camera or camera reference is null/zero"); }
        }

        #endregion

        #region Live view

        /// <summary>
        /// Starts the live view
        /// </summary>
        public void StartLiveView()
        {
            if (!IsLiveViewOn)
            {
                SetSetting(EDSDK.PropID_Evf_OutputDevice, EDSDK.EvfOutputDevice_PC);
                IsLiveViewOn = true;
            }
        }

        /// <summary>
        /// Stops the live view
        /// </summary>
        public void StopLiveView(bool LVoff = true)
        {
            this.LVoff = LVoff;
            IsLiveViewOn = false;
        }

        /// <summary>
        /// Downloads the live view image
        /// </summary>
        private void DownloadEvf()
        {
            LVThread = STAThread.Create(delegate
            {
                IntPtr jpgPointer;
                IntPtr stream = IntPtr.Zero;
                IntPtr EvfImageRef = IntPtr.Zero;
                UnmanagedMemoryStream ums;

                uint err;
                uint length;
                //create stream
                Error = EDSDK.EdsCreateMemoryStream(0, out stream);

                //run live view
                while (IsLiveViewOn)
                {
                    lock (STAThread.ExecLock)
                    {
                        //download current live view image
                        err = EDSDK.EdsCreateEvfImageRef(stream, out EvfImageRef);
                        if (err == EDSDK.EDS_ERR_OK) err = EDSDK.EdsDownloadEvfImage(MainCamera.Ref, EvfImageRef);
                        if (err == EDSDK.EDS_ERR_OBJECT_NOTREADY) { Thread.Sleep(4); continue; }
                        else Error = err;
                    }

                    //get pointer
                    Error = EDSDK.EdsGetPointer(stream, out jpgPointer);
                    Error = EDSDK.EdsGetLength(stream, out length);

                    //get some live view image metadata
                    if (!IsCoordSystemSet) { Evf_CoordinateSystem = GetEvfCoord(EvfImageRef); IsCoordSystemSet = true; }
                    Evf_ZoomRect = GetEvfZoomRect(EvfImageRef);
                    Evf_ZoomPosition = GetEvfPoints(EDSDK.PropID_Evf_ZoomPosition, EvfImageRef);
                    Evf_ImagePosition = GetEvfPoints(EDSDK.PropID_Evf_ImagePosition, EvfImageRef);

                    //release current evf image
                    if (EvfImageRef != IntPtr.Zero) { Error = EDSDK.EdsRelease(EvfImageRef); }

                    //create stream to image
                    unsafe { ums = new UnmanagedMemoryStream((byte*)jpgPointer.ToPointer(), length, length, FileAccess.Read); }

                    //fire the LiveViewUpdated event with the live view image stream
                    if (LiveViewUpdated != null) LiveViewUpdated(ums);
                    ums.Close();
                }

                //release and finish
                if (stream != IntPtr.Zero) { Error = EDSDK.EdsRelease(stream); }
                //stop the live view
                SetSetting(EDSDK.PropID_Evf_OutputDevice, LVoff ? 0 : EDSDK.EvfOutputDevice_TFT);
            });
            LVThread.Start();
        }

        /// <summary>
        /// Get the live view ZoomRect value
        /// </summary>
        /// <param name="imgRef">The live view reference</param>
        /// <returns>ZoomRect value</returns>
        private EDSDK.EdsRect GetEvfZoomRect(IntPtr imgRef)
        {
            int size = Marshal.SizeOf(typeof(EDSDK.EdsRect));
            IntPtr ptr = Marshal.AllocHGlobal(size);
            uint err = EDSDK.EdsGetPropertyData(imgRef, EDSDK.PropID_Evf_ZoomRect, 0, size, ptr);
            EDSDK.EdsRect rect = (EDSDK.EdsRect)Marshal.PtrToStructure(ptr, typeof(EDSDK.EdsRect));
            Marshal.FreeHGlobal(ptr);
            if (err == EDSDK.EDS_ERR_OK) return rect;
            else return new EDSDK.EdsRect();
        }

        /// <summary>
        /// Get the live view coordinate system
        /// </summary>
        /// <param name="imgRef">The live view reference</param>
        /// <returns>the live view coordinate system</returns>
        private EDSDK.EdsSize GetEvfCoord(IntPtr imgRef)
        {
            int size = Marshal.SizeOf(typeof(EDSDK.EdsSize));
            IntPtr ptr = Marshal.AllocHGlobal(size);
            uint err = EDSDK.EdsGetPropertyData(imgRef, EDSDK.PropID_Evf_CoordinateSystem, 0, size, ptr);
            EDSDK.EdsSize coord = (EDSDK.EdsSize)Marshal.PtrToStructure(ptr, typeof(EDSDK.EdsSize));
            Marshal.FreeHGlobal(ptr);
            if (err == EDSDK.EDS_ERR_OK) return coord;
            else return new EDSDK.EdsSize();
        }

        /// <summary>
        /// Get a live view EdsPoint value
        /// </summary>
        /// <param name="imgRef">The live view reference</param>
        /// <returns>a live view EdsPoint value</returns>
        private EDSDK.EdsPoint GetEvfPoints(uint PropID, IntPtr imgRef)
        {
            int size = Marshal.SizeOf(typeof(EDSDK.EdsPoint));
            IntPtr ptr = Marshal.AllocHGlobal(size);
            uint err = EDSDK.EdsGetPropertyData(imgRef, PropID, 0, size, ptr);
            EDSDK.EdsPoint data = (EDSDK.EdsPoint)Marshal.PtrToStructure(ptr, typeof(EDSDK.EdsPoint));
            Marshal.FreeHGlobal(ptr);
            if (err == EDSDK.EDS_ERR_OK) return data;
            else return new EDSDK.EdsPoint();
        }

        #endregion

        #region Filming

        /// <summary>
        /// Starts recording a video and downloads it when finished
        /// </summary>
        /// <param name="FilePath">Directory to where the final video will be saved to</param>
        public void StartFilming(string FilePath)
        {
            if (!IsFilming)
            {
                StartFilming();
                this.DownloadVideo = true;
                ImageSaveDirectory = FilePath;
            }
        }

        /// <summary>
        /// Starts recording a video
        /// </summary>
        public void StartFilming()
        {
            if (!IsFilming)
            {
                //Check if the camera is ready to film
                if (GetSetting(EDSDK.PropID_Record) != 3) throw new InvalidOperationException("Camera is not in film mode");

                IsFilming = true;

                //to restore the current setting after recording
                PrevSaveTo = GetSetting(EDSDK.PropID_SaveTo);
                //when recording videos, it has to be saved on the camera internal memory
                SetSetting(EDSDK.PropID_SaveTo, (uint)EDSDK.EdsSaveTo.Camera);
                this.DownloadVideo = false;
                //start the video recording
                SendSDKCommand(delegate { Error = EDSDK.EdsSetPropertyData(MainCamera.Ref, EDSDK.PropID_Record, 0, 4, 4); });
            }
        }

        /// <summary>
        /// Stops recording a video
        /// </summary>
        public void StopFilming()
        {
            if (IsFilming)
            {
                SendSDKCommand(delegate
                {
                    //Shut off live view (it will hang otherwise)
                    StopLiveView(false);
                    //stop video recording
                    Error = EDSDK.EdsSetPropertyData(MainCamera.Ref, EDSDK.PropID_Record, 0, 4, 0);
                });
                //set back to previous state
                SetSetting(EDSDK.PropID_SaveTo, PrevSaveTo);
                IsFilming = false;
            }
        }

        #endregion

        #region Taking photos

        /// <summary>
        /// Press the shutter button
        /// </summary>
        /// <param name="state">State of the shutter button</param>
        public void PressShutterButton(EDSDK.EdsShutterButton state)
        {
            //start thread to not block everything
            SendSDKCommand(delegate
            {
                //send command to camera
                lock (STAThread.ExecLock) { Error = EDSDK.EdsSendCommand(MainCamera.Ref, EDSDK.CameraCommand_PressShutterButton, (uint)state); };
            }, true);
        }

        /// <summary>
        /// Takes a photo with the current camera settings
        /// </summary>
        public void TakePhoto()
        {
            //start thread to not block everything
            SendSDKCommand(delegate
            {
                //send command to camera
                lock (STAThread.ExecLock) { Error = EDSDK.EdsSendCommand(MainCamera.Ref, EDSDK.CameraCommand_TakePicture, 0); };
            }, true);
        }

        /// <summary>
        /// Takes a photo in bulb mode with the current camera settings
        /// </summary>
        /// <param name="BulbTime">The time in milliseconds for how long the shutter will be open</param>
        public void TakePhoto(uint BulbTime)
        {
            //bulbtime has to be at least a second
            if (BulbTime < 1000) { throw new ArgumentException("Bulbtime has to be bigger than 1000ms"); }

            //start thread to not block everything
            SendSDKCommand(delegate
            {
                //open the shutter
                lock (STAThread.ExecLock) { Error = EDSDK.EdsSendCommand(MainCamera.Ref, EDSDK.CameraCommand_BulbStart, 0); }
                //wait for the specified time
                Thread.Sleep((int)BulbTime);
                //close shutter
                lock (STAThread.ExecLock) { Error = EDSDK.EdsSendCommand(MainCamera.Ref, EDSDK.CameraCommand_BulbEnd, 0); }
            }, true);
        }

        #endregion

        #region Other

        /// <summary>
        /// Sends a command to the camera safely
        /// </summary>
        private void SendSDKCommand(Action command, bool longTask = false)
        {
            if (longTask) STAThread.Create(command).Start();
            else STAThread.ExecuteSafely(command);
        }

        /// <summary>
        /// Tells the camera that there is enough space on the HDD if SaveTo is set to Host
        /// This method does not use the actual free space!
        /// </summary>
        public void SetCapacity()
        {            
            //create new capacity struct
            EDSDK.EdsCapacity capacity = new EDSDK.EdsCapacity();

            //set big enough values
            capacity.Reset = 1;
            capacity.BytesPerSector = 0x1000;
            capacity.NumberOfFreeClusters = 0x7FFFFFFF;

            //set the values to camera
            SendSDKCommand(delegate { Error = EDSDK.EdsSetCapacity(MainCamera.Ref, capacity); });
        }

        /// <summary>
        /// Tells the camera how much space is available on the host PC
        /// </summary>
        /// <param name="BytesPerSector">Bytes per sector on HD</param>
        /// <param name="NumberOfFreeClusters">Number of free clusters on HD</param>
        public void SetCapacity(int BytesPerSector, int NumberOfFreeClusters)
        {
            //create new capacity struct
            EDSDK.EdsCapacity capacity = new EDSDK.EdsCapacity();

            //set given values
            capacity.Reset = 1;
            capacity.BytesPerSector = BytesPerSector;
            capacity.NumberOfFreeClusters = NumberOfFreeClusters;

            //set the values to camera
            SendSDKCommand(delegate { Error = EDSDK.EdsSetCapacity(MainCamera.Ref, capacity); });
        }

        /// <summary>
        /// Moves the focus (only works while in live view)
        /// </summary>
        /// <param name="Speed">Speed and direction of focus movement</param>
        public void SetFocus(uint Speed)
        {
            if (IsLiveViewOn) SendSDKCommand(delegate { Error = EDSDK.EdsSendCommand(MainCamera.Ref, EDSDK.CameraCommand_DriveLensEvf, Speed); });
        }

        /// <summary>
        /// Sets the WB of the live view while in live view
        /// </summary>
        /// <param name="x">X Coordinate</param>
        /// <param name="y">Y Coordinate</param>
        public void SetManualWBEvf(ushort x, ushort y)
        {
            if (IsLiveViewOn)
            {
                //converts the coordinates to a form the camera accepts
                byte[] xa = BitConverter.GetBytes(x);
                byte[] ya = BitConverter.GetBytes(y);
                uint coord = BitConverter.ToUInt32(new byte[] { xa[0], xa[1], ya[0], ya[1] }, 0);
                //send command to camera
                SendSDKCommand(delegate { Error = EDSDK.EdsSendCommand(MainCamera.Ref, EDSDK.CameraCommand_DoClickWBEvf, coord); });
            }
        }

        /// <summary>
        /// Gets all volumes, folders and files existing on the camera
        /// </summary>
        /// <returns>A CameraFileEntry with all informations</returns>
        public CameraFileEntry GetAllEntries()
        {
            //create the main entry which contains all subentries
            CameraFileEntry MainEntry = new CameraFileEntry("Camera", true);

            //get the number of volumes currently installed in the camera
            int VolumeCount;
            Error = EDSDK.EdsGetChildCount(MainCamera.Ref, out VolumeCount);
            List<CameraFileEntry> VolumeEntries = new List<CameraFileEntry>();

            //iterate through all of them
            for (int i = 0; i < VolumeCount; i++)
            {
                //get information about volume
                IntPtr ChildPtr;
                Error = EDSDK.EdsGetChildAtIndex(MainCamera.Ref, i, out ChildPtr);
                EDSDK.EdsVolumeInfo vinfo = new EDSDK.EdsVolumeInfo();
                SendSDKCommand(delegate { Error = EDSDK.EdsGetVolumeInfo(ChildPtr, out vinfo); });

                //ignore the HDD
                if (vinfo.szVolumeLabel != "HDD")
                {
                    //add volume to the list
                    VolumeEntries.Add(new CameraFileEntry("Volume" + i + "(" + vinfo.szVolumeLabel + ")", true));
                    //get all child entries on this volume
                    VolumeEntries[i].AddSubEntries(GetChildren(ChildPtr));
                }
                //release the volume
                Error = EDSDK.EdsRelease(ChildPtr);
            }
            //add all volumes to the main entry and return it
            MainEntry.AddSubEntries(VolumeEntries.ToArray());
            return MainEntry;
        }

        /// <summary>
        /// Locks or unlocks the cameras UI
        /// </summary>
        /// <param name="LockState">True for locked, false to unlock</param>
        public void UILock(bool LockState)
        {
            SendSDKCommand(delegate
            {
                if (LockState == true) Error = EDSDK.EdsSendStatusCommand(MainCamera.Ref, EDSDK.CameraState_UILock, 0);
                else Error = EDSDK.EdsSendStatusCommand(MainCamera.Ref, EDSDK.CameraState_UIUnLock, 0);
            });
        }

        /// <summary>
        /// Gets the children of a camera folder/volume. Recursive method.
        /// </summary>
        /// <param name="ptr">Pointer to volume or folder</param>
        /// <returns></returns>
        private CameraFileEntry[] GetChildren(IntPtr ptr)
        {
            int ChildCount;
            //get children of first pointer
            Error = EDSDK.EdsGetChildCount(ptr, out ChildCount);
            if (ChildCount > 0)
            {
                //if it has children, create an array of entries
                CameraFileEntry[] MainEntry = new CameraFileEntry[ChildCount];
                for (int i = 0; i < ChildCount; i++)
                {
                    IntPtr ChildPtr;
                    //get children of children
                    Error = EDSDK.EdsGetChildAtIndex(ptr, i, out ChildPtr);
                    //get the information about this children
                    EDSDK.EdsDirectoryItemInfo ChildInfo = new EDSDK.EdsDirectoryItemInfo();
                    SendSDKCommand(delegate { Error = EDSDK.EdsGetDirectoryItemInfo(ChildPtr, out ChildInfo); });

                    //create entry from information
                    MainEntry[i] = new CameraFileEntry(ChildInfo.szFileName, GetBool(ChildInfo.isFolder));
                    if (!MainEntry[i].IsFolder)
                    {
                        //if it's not a folder, create thumbnail and safe it to the entry
                        IntPtr stream;
                        Error = EDSDK.EdsCreateMemoryStream(0, out stream);
                        SendSDKCommand(delegate { Error = EDSDK.EdsDownloadThumbnail(ChildPtr, stream); });
                        MainEntry[i].AddThumb(GetImage(stream, EDSDK.EdsImageSource.Thumbnail));                        
                    }
                    else
                    {
                        //if it's a folder, check for children with recursion
                        CameraFileEntry[] retval = GetChildren(ChildPtr);
                        if (retval != null) MainEntry[i].AddSubEntries(retval);
                    }
                    //release current children
                    EDSDK.EdsRelease(ChildPtr);
                }
                return MainEntry;
            }
            else return null;
        }

        /// <summary>
        /// Converts an int to a bool
        /// </summary>
        /// <param name="val">Value</param>
        /// <returns>A bool created from the value</returns>
        private bool GetBool(int val)
        {
            if (val == 0) return false;
            else return true;
        }

        #endregion

        #endregion
    }

    public class Camera
    {
        internal IntPtr Ref;
        public EDSDK.EdsDeviceInfo Info { get; private set; }

        public uint Error
        {
            get { return EDSDK.EDS_ERR_OK; }
            set { if (value != EDSDK.EDS_ERR_OK) throw new Exception("SDK Error: " + value); }
        }

        public Camera(IntPtr Reference)
        {
            if (Reference == IntPtr.Zero) throw new ArgumentNullException("Camera pointer is zero");
            this.Ref = Reference;
            EDSDK.EdsDeviceInfo dinfo;
            Error = EDSDK.EdsGetDeviceInfo(Reference, out dinfo);
            this.Info = dinfo;
        }
    }

    public static class CameraValues
    {
        private static CultureInfo cInfo = new CultureInfo("en-US");

        public static string AV(uint v)
        {
            switch (v)
            {
                case 0x00:
                    return "Auto";
                case 0x08:
                    return "1";
                case 0x40:
                    return "11";
                case 0x0B:
                    return "1.1";
                case 0x43:
                    return "13 (1/3)";
                case 0x0C:
                    return "1.2";
                case 0x44:
                    return "13";
                case 0x0D:
                    return "1.2 (1/3)";
                case 0x45:
                    return "14";
                case 0x10:
                    return "1.4";
                case 0x48:
                    return "16";
                case 0x13:
                    return "1.6";
                case 0x4B:
                    return "18";
                case 0x14:
                    return "1.8";
                case 0x4C:
                    return "19";
                case 0x15:
                    return "1.8 (1/3)";
                case 0x4D:
                    return "20";
                case 0x18:
                    return "2";
                case 0x50:
                    return "22";
                case 0x1B:
                    return "2.2";
                case 0x53:
                    return "25";
                case 0x1C:
                    return "2.5";
                case 0x54:
                    return "27";
                case 0x1D:
                    return "2.5 (1/3)";
                case 0x55:
                    return "29";
                case 0x20:
                    return "2.8";
                case 0x58:
                    return "32";
                case 0x23:
                    return "3.2";
                case 0x5B:
                    return "36";
                case 0x24:
                    return "3.5";
                case 0x5C:
                    return "38";
                case 0x25:
                    return "3.5 (1/3)";
                case 0x5D:
                    return "40";
                case 0x28:
                    return "4";
                case 0x60:
                    return "45";
                case 0x2B:
                    return "4.5";
                case 0x63:
                    return "51";
                case 0x2C:
                    return "4.5 (1/3)";
                case 0x64:
                    return "54";
                case 0x2D:
                    return "5.0";
                case 0x65:
                    return "57";
                case 0x30:
                    return "5.6";
                case 0x68:
                    return "64";
                case 0x33:
                    return "6.3";
                case 0x6B:
                    return "72";
                case 0x34:
                    return "6.7";
                case 0x6C:
                    return "76";
                case 0x35:
                    return "7.1";
                case 0x6D:
                    return "80";
                case 0x38:
                    return " 8";
                case 0x70:
                    return "91";
                case 0x3B:
                    return "9";
                case 0x3C:
                    return "9.5";
                case 0x3D:
                    return "10";

                case 0xffffffff:
                default:
                    return "N/A";
            }
        }

        public static string ISO(uint v)
        {
            switch (v)
            {
                case 0x00000000:
                    return "Auto ISO";
                case 0x00000028:
                    return "ISO 6";
                case 0x00000030:
                    return "ISO 12";
                case 0x00000038:
                    return "ISO 25";
                case 0x00000040:
                    return "ISO 50";
                case 0x00000048:
                    return "ISO 100";
                case 0x0000004b:
                    return "ISO 125";
                case 0x0000004d:
                    return "ISO 160";
                case 0x00000050:
                    return "ISO 200";
                case 0x00000053:
                    return "ISO 250";
                case 0x00000055:
                    return "ISO 320";
                case 0x00000058:
                    return "ISO 400";
                case 0x0000005b:
                    return "ISO 500";
                case 0x0000005d:
                    return "ISO 640";
                case 0x00000060:
                    return "ISO 800";
                case 0x00000063:
                    return "ISO 1000";
                case 0x00000065:
                    return "ISO 1250";
                case 0x00000068:
                    return "ISO 1600";
                case 0x00000070:
                    return "ISO 3200";
                case 0x00000078:
                    return "ISO 6400";
                case 0x00000080:
                    return "ISO 12800";
                case 0x00000088:
                    return "ISO 25600";
                case 0x00000090:
                    return "ISO 51200";
                case 0x00000098:
                    return "ISO 102400";
                case 0xffffffff:
                default:
                    return "N/A";
            }
        }

        public static string TV(uint v)
        {
            switch (v)
            {
                case 0x00:
                    return "Auto";
                case 0x0C:
                    return "Bulb";
                case 0x5D:
                    return "1/25";
                case 0x10:
                    return "30\"";
                case 0x60:
                    return "1/30";
                case 0x13:
                    return "25\"";
                case 0x63:
                    return "1/40";
                case 0x14:
                    return "20\"";
                case 0x64:
                    return "1/45";
                case 0x15:
                    return "20\" (1/3)";
                case 0x65:
                    return "1/50";
                case 0x18:
                    return "15\"";
                case 0x68:
                    return "1/60";
                case 0x1B:
                    return "13\"";
                case 0x6B:
                    return "1/80";
                case 0x1C:
                    return "10\"";
                case 0x6C:
                    return "1/90";
                case 0x1D:
                    return "10\" (1/3)";
                case 0x6D:
                    return "1/100";
                case 0x20:
                    return "8\"";
                case 0x70:
                    return "1/125";
                case 0x23:
                    return "6\" (1/3)";
                case 0x73:
                    return "1/160";
                case 0x24:
                    return "6\"";
                case 0x74:
                    return "1/180";
                case 0x25:
                    return "5\"";
                case 0x75:
                    return "1/200";
                case 0x28:
                    return "4\"";
                case 0x78:
                    return "1/250";
                case 0x2B:
                    return "3\"2";
                case 0x7B:
                    return "1/320";
                case 0x2C:
                    return "3\"";
                case 0x7C:
                    return "1/350";
                case 0x2D:
                    return "2\"5";
                case 0x7D:
                    return "1/400";
                case 0x30:
                    return "2\"";
                case 0x80:
                    return "1/500";
                case 0x33:
                    return "1\"6";
                case 0x83:
                    return "1/640";
                case 0x34:
                    return "1\"5";
                case 0x84:
                    return "1/750";
                case 0x35:
                    return "1\"3";
                case 0x85:
                    return "1/800";
                case 0x38:
                    return "1\"";
                case 0x88:
                    return "1/1000";
                case 0x3B:
                    return "0\"8";
                case 0x8B:
                    return "1/1250";
                case 0x3C:
                    return "0\"7";
                case 0x8C:
                    return "1/1500";
                case 0x3D:
                    return "0\"6";
                case 0x8D:
                    return "1/1600";
                case 0x40:
                    return "0\"5";
                case 0x90:
                    return "1/2000";
                case 0x43:
                    return "0\"4";
                case 0x93:
                    return "1/2500";
                case 0x44:
                    return "0\"3";
                case 0x94:
                    return "1/3000";
                case 0x45:
                    return "0\"3 (1/3)";
                case 0x95:
                    return "1/3200";
                case 0x48:
                    return "1/4";
                case 0x98:
                    return "1/4000";
                case 0x4B:
                    return "1/5";
                case 0x9B:
                    return "1/5000";
                case 0x4C:
                    return "1/6";
                case 0x9C:
                    return "1/6000";
                case 0x4D:
                    return "1/6 (1/3)";
                case 0x9D:
                    return "1/6400";
                case 0x50:
                    return "1/8";
                case 0xA0:
                    return "1/8000";
                case 0x53:
                    return "1/10 (1/3)";
                case 0x54:
                    return "1/10";
                case 0x55:
                    return "1/13";
                case 0x58:
                    return "1/15";
                case 0x5B:
                    return "1/20 (1/3)";
                case 0x5C:
                    return "1/20";

                case 0xffffffff:
                default:
                    return "N/A";
            }
        }


        public static uint AV(string v)
        {
            switch (v)
            {
                case "Auto":
                    return 0x00;
                case "1":
                    return 0x08;
                case "11":
                    return 0x40;
                case "1.1":
                    return 0x0B;
                case "13 (1/3)":
                    return 0x43;
                case "1.2":
                    return 0x0C;
                case "13":
                    return 0x44;
                case "1.2 (1/3)":
                    return 0x0D;
                case "14":
                    return 0x45;
                case "1.4":
                    return 0x10;
                case "16":
                    return 0x48;
                case "1.6":
                    return 0x13;
                case "18":
                    return 0x4B;
                case "1.8":
                    return 0x14;
                case "19":
                    return 0x4C;
                case "1.8 (1/3)":
                    return 0x15;
                case "20":
                    return 0x4D;
                case "2":
                    return 0x18;
                case "22":
                    return 0x50;
                case "2.2":
                    return 0x1B;
                case "25":
                    return 0x53;
                case "2.5":
                    return 0x1C;
                case "27":
                    return 0x54;
                case "2.5 (1/3)":
                    return 0x1D;
                case "29":
                    return 0x55;
                case "2.8":
                    return 0x20;
                case "32":
                    return 0x58;
                case "3.2":
                    return 0x23;
                case "36":
                    return 0x5B;
                case "3.5":
                    return 0x24;
                case "38":
                    return 0x5C;
                case "3.5 (1/3)":
                    return 0x25;
                case "40":
                    return 0x5D;
                case "4":
                    return 0x28;
                case "45":
                    return 0x60;
                case "4.5":
                    return 0x2B;
                case "51":
                    return 0x63;
                case "4.5 (1/3)":
                    return 0x2C;
                case "54":
                    return 0x64;
                case "5.0":
                    return 0x2D;
                case "57":
                    return 0x65;
                case "5.6":
                    return 0x30;
                case "64":
                    return 0x68;
                case "6.3":
                    return 0x33;
                case "72":
                    return 0x6B;
                case "6.7":
                    return 0x34;
                case "76":
                    return 0x6C;
                case "7.1":
                    return 0x35;
                case "80":
                    return 0x6D;
                case " 8":
                    return 0x38;
                case "91":
                    return 0x70;
                case "9":
                    return 0x3B;
                case "9.5":
                    return 0x3C;
                case "10":
                    return 0x3D;

                case "N/A":
                default:
                    return 0xffffffff;
            }
        }

        public static uint ISO(string v)
        {
            switch (v)
            {
                case "Auto ISO":
                    return 0x00000000;
                case "ISO 6":
                    return 0x00000028;
                case "ISO 12":
                    return 0x00000030;
                case "ISO 25":
                    return 0x00000038;
                case "ISO 50":
                    return 0x00000040;
                case "ISO 100":
                    return 0x00000048;
                case "ISO 125":
                    return 0x0000004b;
                case "ISO 160":
                    return 0x0000004d;
                case "ISO 200":
                    return 0x00000050;
                case "ISO 250":
                    return 0x00000053;
                case "ISO 320":
                    return 0x00000055;
                case "ISO 400":
                    return 0x00000058;
                case "ISO 500":
                    return 0x0000005b;
                case "ISO 640":
                    return 0x0000005d;
                case "ISO 800":
                    return 0x00000060;
                case "ISO 1000":
                    return 0x00000063;
                case "ISO 1250":
                    return 0x00000065;
                case "ISO 1600":
                    return 0x00000068;
                case "ISO 3200":
                    return 0x00000070;
                case "ISO 6400":
                    return 0x00000078;
                case "ISO 12800":
                    return 0x00000080;
                case "ISO 25600":
                    return 0x00000088;
                case "ISO 51200":
                    return 0x00000090;
                case "ISO 102400":
                    return 0x00000098;

                case "N/A":
                default:
                    return 0xffffffff;
            }
        }

        public static uint TV(string v)
        {
            switch (v)
            {
                case "Auto":
                    return 0x00;
                case "Bulb":
                    return 0x0C;
                case "1/25":
                    return 0x5D;
                case "30\"":
                    return 0x10;
                case "1/30":
                    return 0x60;
                case "25\"":
                    return 0x13;
                case "1/40":
                    return 0x63;
                case "20\"":
                    return 0x14;
                case "1/45":
                    return 0x64;
                case "20\" (1/3)":
                    return 0x15;
                case "1/50":
                    return 0x65;
                case "15\"":
                    return 0x18;
                case "1/60":
                    return 0x68;
                case "13\"":
                    return 0x1B;
                case "1/80":
                    return 0x6B;
                case "10\"":
                    return 0x1C;
                case "1/90":
                    return 0x6C;
                case "10\" (1/3)":
                    return 0x1D;
                case "1/100":
                    return 0x6D;
                case "8\"":
                    return 0x20;
                case "1/125":
                    return 0x70;
                case "6\" (1/3)":
                    return 0x23;
                case "1/160":
                    return 0x73;
                case "6\"":
                    return 0x24;
                case "1/180":
                    return 0x74;
                case "5\"":
                    return 0x25;
                case "1/200":
                    return 0x75;
                case "4\"":
                    return 0x28;
                case "1/250":
                    return 0x78;
                case "3\"2":
                    return 0x2B;
                case "1/320":
                    return 0x7B;
                case "3\"":
                    return 0x2C;
                case "1/350":
                    return 0x7C;
                case "2\"5":
                    return 0x2D;
                case "1/400":
                    return 0x7D;
                case "2\"":
                    return 0x30;
                case "1/500":
                    return 0x80;
                case "1\"6":
                    return 0x33;
                case "1/640":
                    return 0x83;
                case "1\"5":
                    return 0x34;
                case "1/750":
                    return 0x84;
                case "1\"3":
                    return 0x35;
                case "1/800":
                    return 0x85;
                case "1\"":
                    return 0x38;
                case "1/1000":
                    return 0x88;
                case "0\"8":
                    return 0x3B;
                case "1/1250":
                    return 0x8B;
                case "0\"7":
                    return 0x3C;
                case "1/1500":
                    return 0x8C;
                case "0\"6":
                    return 0x3D;
                case "1/1600":
                    return 0x8D;
                case "0\"5":
                    return 0x40;
                case "1/2000":
                    return 0x90;
                case "0\"4":
                    return 0x43;
                case "1/2500":
                    return 0x93;
                case "0\"3":
                    return 0x44;
                case "1/3000":
                    return 0x94;
                case "0\"3 (1/3)":
                    return 0x45;
                case "1/3200":
                    return 0x95;
                case "1/4":
                    return 0x48;
                case "1/4000":
                    return 0x98;
                case "1/5":
                    return 0x4B;
                case "1/5000":
                    return 0x9B;
                case "1/6":
                    return 0x4C;
                case "1/6000":
                    return 0x9C;
                case "1/6 (1/3)":
                    return 0x4D;
                case "1/6400":
                    return 0x9D;
                case "1/8":
                    return 0x50;
                case "1/8000":
                    return 0xA0;
                case "1/10 (1/3)":
                    return 0x53;
                case "1/10":
                    return 0x54;
                case "1/13":
                    return 0x55;
                case "1/15":
                    return 0x58;
                case "1/20 (1/3)":
                    return 0x5B;
                case "1/20":
                    return 0x5C;

                case "N/A":
                default:
                    return 0xffffffff;
            }
        }
    }
    
    public class CameraFileEntry
    {
        public string Name { get; private set; }
        public bool IsFolder { get; private set; }
        public Bitmap Thumbnail { get; private set; }
        public CameraFileEntry[] Entries { get; private set; }

        public CameraFileEntry(string Name, bool IsFolder)
        {
            this.Name = Name;
            this.IsFolder = IsFolder;
        }

        public void AddSubEntries(CameraFileEntry[] Entries)
        {
            this.Entries = Entries;
        }

        public void AddThumb(Bitmap Thumbnail)
        {
            this.Thumbnail = Thumbnail;
        }
    }
    
    public static class STAThread
    {
        /// <summary>
        /// The object that is used to lock the live view thread
        /// </summary>
        public static readonly object ExecLock = new object();
        /// <summary>
        /// States if the calling thread is an STA thread or not
        /// </summary>
        public static bool IsSTAThread
        {
            get { return Thread.CurrentThread.GetApartmentState() == ApartmentState.STA; }
        }

        /// <summary>
        /// The main thread where everything will be executed on
        /// </summary>
        private static Thread main;
        /// <summary>
        /// The action to be executed
        /// </summary>
        private static Action runAction;
        /// <summary>
        /// Storage for an exception that might have happened on the execution thread
        /// </summary>
        private static Exception runException;
        /// <summary>
        /// States if the execution thread is currently running
        /// </summary>
        private static bool isRunning = false;
        /// <summary>
        /// Lock object to make sure only one command at a time is executed
        /// </summary>
        private static object runLock = new object();
        /// <summary>
        /// Lock object to synchronize between execution and calling thread
        /// </summary>
        private static object threadLock = new object();

        /// <summary>
        /// Starts the execution thread
        /// </summary>
        internal static void Init()
        {
            if (!isRunning)
            {
                main = Create(SafeExecutionLoop);
                isRunning = true;
                main.Start();
            }
        }

        /// <summary>
        /// Shuts down the execution thread
        /// </summary>
        internal static void Shutdown()
        {
            if (isRunning)
            {
                isRunning = false;
                lock (threadLock) { Monitor.Pulse(threadLock); }
                main.Join();
            }
        }

        /// <summary>
        /// Creates an STA thread that can safely execute SDK commands
        /// </summary>
        /// <param name="a">The command to run on this thread</param>
        /// <returns>An STA thread</returns>
        public static Thread Create(Action a)
        {
            var thread = new Thread(new ThreadStart(a));
            thread.SetApartmentState(ApartmentState.STA);
            return thread;
        }


        /// <summary>
        /// Safely executes an SDK command
        /// </summary>
        /// <param name="a">The SDK command</param>
        public static void ExecuteSafely(Action a)
        {
            lock (runLock)
            {
                if (!isRunning) return;

                if (IsSTAThread)
                {
                    runAction = a;
                    lock (threadLock)
                    {
                        Monitor.Pulse(threadLock);
                        Monitor.Wait(threadLock);
                    }
                    if (runException != null) throw runException;
                }
                else lock (ExecLock) { a(); }
            }
        }

        /// <summary>
        /// Safely executes an SDK command with return value
        /// </summary>
        /// <param name="func">The SDK command</param>
        /// <returns>the return value of the function</returns>
        public static T ExecuteSafely<T>(Func<T> func)
        {
            T result = default(T);
            ExecuteSafely(delegate { result = func(); });
            return result;
        }

        private static void SafeExecutionLoop()
        {
            lock (threadLock)
            {
                while (true)
                {
                    Monitor.Wait(threadLock);
                    if (!isRunning) return;
                    runException = null;
                    try { lock (ExecLock) { runAction(); } }
                    catch (Exception ex) { runException = ex; }
                    Monitor.Pulse(threadLock);
                }
            }
        }
    }
}

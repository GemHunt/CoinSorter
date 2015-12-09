using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

namespace ImageClassifier
{
    class Camera
    {
        [DllImport("E:\\build\\Caffe-prefix\\src\\Caffe-build\\examples\\cpp_classification\\Debug\\classification-d.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr ClassifyFromWebCam(int imageID, bool show, bool classify, bool deskew, bool autoRotate, bool saveImages, String modelDir);

        [DllImport("E:\\build\\Caffe-prefix\\src\\Caffe-build\\examples\\cpp_classification\\Debug\\classification-d.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int ReleaseMemory(IntPtr ptr);

        public static void ClassifyFromWebCam(int imageID, bool show, bool classify, bool deskew,bool autoRotate, bool saveImages, ref int date, ref String design)
        {
            date = 0;
            design = "null";
            String modelDir = "C:/Users/pkrush/Documents/GemHunt/CoinSorter/models";
            IntPtr ptr = ClassifyFromWebCam(imageID, show, classify,deskew,autoRotate, saveImages, modelDir);
            //the output result should be a structure, it's hard coded for now:
            double[] result = new double[8];
            if (ptr == IntPtr.Zero)
            {
                return;
            }
            Marshal.Copy(ptr, result, 0, 8);
            ReleaseMemory(ptr);
            if ((int)result[0] == 0)
            {
                design = "Bad Capture";
                return;
            }
            design = LabelsDB.GetLabel((int)result[2]);
            if ((int)result[2] == 1) {
                date = (int)result[6];
            }
        }









    }
}

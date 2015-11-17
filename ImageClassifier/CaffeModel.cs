using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;


namespace ImageClassifier
{
    class CaffeModel
    {
        [DllImport("E:\\build\\Caffe-prefix\\src\\Caffe-build\\examples\\cpp_classification\\Debug\\classification-d.dll")]
        private static extern IntPtr ClassifyImage(String modelDir, String image_file);

        [DllImport("E:\\build\\Caffe-prefix\\src\\Caffe-build\\examples\\cpp_classification\\Debug\\classification-d.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int ReleaseMemory(IntPtr ptr);

        public void ClassifyFiles(String oldImageDirectory, String newImageDirectory, String modelDir, bool toClassify, bool addImagesToDataBase, bool moveImage, bool includeSubDir)
        {
            string[] files;
            if (includeSubDir)
            {
                files = Directory.GetFiles(oldImageDirectory, "*.jpg*", SearchOption.AllDirectories);
            }
            else
            {
                files = Directory.GetFiles(oldImageDirectory, "*.jpg*");
            }

            List<ImageResult> imageResults = new List<ImageResult>();            
            Dictionary<int,int> images = new Dictionary<int,int>();
            foreach (string image_file in files)
            {
                int imageID = Convert.ToInt32(image_file.Substring(image_file.Length-12, 8));
                
                if (toClassify)
                {
                    IntPtr ptr = ClassifyImage(modelDir, image_file);
                    //the output result should be a structure, it's hard coded for now:
                    double[] result = new double[8];
                    Marshal.Copy(ptr, result, 0, 8);
                    ReleaseMemory(ptr);
                    imageResults.Add(new ImageResult(imageID, result));
                    
                    if (moveImage) {
                        FileInfo fi = new FileInfo(image_file);
                        String imageFileDestination = newImageDirectory + "/" + LabelsDB.GetLabel((int)result[0]) + "/" + fi.Name;
                        File.Move(image_file, imageFileDestination);
                    }
                }
            }
            if (addImagesToDataBase)
            {
                ImagesDB.AddImages(imageResults);
            }
            
        }
    }
}

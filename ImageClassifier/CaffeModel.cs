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
        private static extern IntPtr ClassifyImage(String model_file, String trained_file, String mean_file, String label_file, String image_file, bool resize);

        [DllImport("E:\\build\\Caffe-prefix\\src\\Caffe-build\\examples\\cpp_classification\\Debug\\classification-d.dll", CallingConvention = CallingConvention.Cdecl)]
        private static extern int ReleaseMemory(IntPtr ptr);

        public void Classify(String oldImageDirectory, String newImageDirectory, String model, String modelDirectory, bool toClassify, bool addImagesToDataBase, bool includeSubDir)
        {
            String model_file = modelDirectory +"/" + "deploy.prototxt";
            String trained_file = modelDirectory +"/" + "snapshot.caffemodel";
            String mean_file = modelDirectory +"/" + "mean.binaryproto";
            String label_file = modelDirectory + "/" + "labels.txt";
          
            SQLiteDB.Open();

            List<String> labels = LabelsDB.GetLabels(label_file);

            int resultCount = labels.Count;
            if (resultCount > 5) {
                resultCount = 5;
            }

            string[] files;
            if (includeSubDir)
            {
                files = Directory.GetFiles(oldImageDirectory, "*.*", SearchOption.AllDirectories);
            }
            else
            {
                files = Directory.GetFiles(oldImageDirectory);
            }
                        
            foreach (string image_file in files)
            {
                int imageID = Convert.ToInt32(image_file.Substring(image_file.Length-12, 8));
                
                if (toClassify)
                {
                    IntPtr ptr = ClassifyImage(model_file, trained_file, mean_file, label_file, image_file,true);
                    double[] result = new double[resultCount * 2];
                    Marshal.Copy(ptr, result, 0, resultCount * 2);
                    ReleaseMemory(ptr);
                    for (int count = 0; count > resultCount;count++ )
                    {
                        ResultsDB.AddResult(2, imageID, (int)result[count], result[count + resultCount]);
                    }

                    FileInfo fi = new FileInfo(image_file);
                    String imageFileDestination = newImageDirectory + "/" + LabelsDB.GetLabel((int)result[0]) + "/" + fi.Name;
                    File.Move(image_file, imageFileDestination);
                }
                if (addImagesToDataBase)
                {
                    ImagesDB.AddImage(imageID, 0);
                }
            }
            SQLiteDB.Close();
        }
    }
}

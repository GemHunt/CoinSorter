using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Data.SQLite;

namespace ImageClassifier
{
    class ImagesDB : SQLiteDB
    {
        [DllImport("E:\\build\\Caffe-prefix\\src\\Caffe-build\\examples\\cpp_classification\\Debug\\classification-d.dll")]
        private static extern int Augment(String image_file, String output_file, int angle);


        static public List<int> GetMislabeledImageIDs()
        {
             List<int> GetMislabeledImageIDs = new List<int>();
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("Select Images.ImageID");
            SQL.AppendLine("From Images");
            SQL.AppendLine("Inner Join Results");
            SQL.AppendLine("On Images.ImageID = Results.ImageID");
            SQL.AppendLine("And Results.Score < .5");
            SQL.AppendLine("And Images.LabelID = Results.LabelID");
            SQL.AppendLine("Order by 1;");
            Open();
            SQLiteDataReader reader = GetNewReader(SQL.ToString());
            while (reader.Read()){
                GetMislabeledImageIDs.Add(reader.GetInt32(0));
            }
            reader.Close();
            Close();
            return GetMislabeledImageIDs;
        }

        
        static public void AddImage(int imageID, int labelID)
        {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN;");
            SQL.Append("Insert into Images (ImageID,LabelID) values (");
            SQL.Append(imageID + ",");
            SQL.AppendLine(labelID + ");");
            SQL.AppendLine("COMMIT;");
            ExecuteQuery(SQL.ToString());
        }

        static public int GetImageIDFromFileName(String fileName)
        {
            if (fileName.Contains("raw")){
                 return Convert.ToInt32(fileName.Substring(0, 8)) - 10000000;
            }
            return Convert.ToInt32(fileName.Substring(fileName.Length - 12, 8)) - 10000000;
        }

        
        static public void AugmentImages(String directory)
        {
            String cropDirectory = directory + "/Crops/";
            String augmentDirectory = directory + "/Augmented/";
            if (!Directory.Exists(augmentDirectory))
            {
                Directory.CreateDirectory(augmentDirectory);
            }

            String[] files;
            files = Directory.GetFiles(cropDirectory, "*.*", SearchOption.AllDirectories);
            foreach (string image_file in files)
            {
                if (image_file.Contains("bad"))
                {
                    continue;
                }
                //int imageID = Convert.ToInt32(image_file.Substring(image_file.Length - 12, 8));
                for (int angle = 13; angle < 360; angle = angle + 21)
                {
                    String fileName = image_file.Replace("Crops/", "Augmented/");
                    fileName = fileName.Replace(".jpg", angle.ToString().PadLeft(3, '0') + ".png");
                    Augment(image_file, fileName, angle);
                }

            }
        }







    }
}

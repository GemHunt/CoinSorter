using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageClassifier
{
    class LabelsDB:SQLiteDB
    {
        public static List<String> GetLabels(String labelFileName) {
            List<String> labels = new List<String>();
            string line;

            // Read the file and display it line by line.
            System.IO.StreamReader file = new System.IO.StreamReader(labelFileName);
            while ((line = file.ReadLine()) != null)
            {
                labels.Add(line);
            }

            file.Close();
            return labels;
        }

        public static String GetLabel(int labelID)
        {
            //cheating: This should be from the database:
            switch (labelID)
            {
                case 0:
                    return ("canadaOther");
                case 1:
                    return ("heads");
                case 2:
                    return ("maple");
                case 3:
                    return ("tails");
                case 4:
                    return ("wheat");
                default:
                    return ("unknown");
            }
        }
    }



}


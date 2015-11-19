using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageClassifier
{
    class LabelsDB : SQLiteDB
    {
        public static List<String> GetLabels(String labelFileName)
        {
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

        public static String GetLabel(int DesignID)
        {
            //cheating: This should be from the database:
            switch (DesignID)
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

        public static List<String> GetDateLabels(bool decades)
        {
            List<String> dateLables = new List<String>();
            if (decades)
            {
                for (int y = 1900; y < 2020; y = y + 10)
                {
                    dateLables.Add((y).ToString());
                }
            }
            else
            {
                for (int y = 1950; y < 1983; y++)
                {
                    dateLables.Add(y.ToString());
                }
            }

            return dateLables;
        }

        public static List<String> GetDesignLabels()
        {
            List<String> designLables = new List<String>();
            designLables.Add("canadaOther");
            designLables.Add("heads");
            designLables.Add("maple");
            designLables.Add("tails");
            designLables.Add("wheat");
            return designLables;
        }

        public static int GetDesignID(String dir)
        {
            //cheating: This should be from the database:
            if (dir.Contains("canadaOther")) { return 0; }
            if (dir.Contains("heads")) { return 1; }
            if (dir.Contains("maple")) { return 2; }
            if (dir.Contains("tails")) { return 3; }
            if (dir.Contains("wheat")) { return 4; }
            if (dir.Contains("bad")) { return 4; }
            if (dir.Contains("Dates4\\19"))
            {
                int year = 0;
                int.TryParse(dir.Substring(17, 4), out year);
                return year;
            }
            return -1;
        }
    }
}






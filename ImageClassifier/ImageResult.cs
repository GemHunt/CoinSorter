using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageClassifier
{
    class ImageResult
    {
        public int ImageID;
        public int Centered;
        public double CenterResult;
        public int DesignID;
        public double DesignResult;
        public int Angle;
        public double AngleResult;
        public int Date;
        public double DateResult;

        public ImageResult(int imageID, double[] results)
        {
            ImageID = imageID;
            Centered = (int)results[0];
            CenterResult = results[1];
            DesignID = (int)results[2];
            DesignResult = results[3];
            Angle = (int)results[4];
            AngleResult = results[5];
            Date = (int)results[6];
            DateResult = results[7];
        }
    }
}

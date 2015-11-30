using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageClassifier
{
    class ImageGroundTruth
    {
        public int ImageID;
        public int CenteredGT;
        public int DesignIDGT;
        public Double AngleGT;
        public int DateGT;
        
        public ImageGroundTruth(int imageID,int centeredGT, int designIDGT, double angleGT, int dateGT)
        {
            ImageID = imageID;
            CenteredGT = centeredGT;
            DesignIDGT = designIDGT;
            AngleGT = angleGT;
            DateGT = dateGT;
        }
    }
}

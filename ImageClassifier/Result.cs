using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageClassifier
{
    class Result
    {
        public int ModelID;
        public int ImageID;
        public int DesignID;
        public double Score;

        public Result(int modelID, int imageID, int designID, double score)
        {
            ModelID = modelID;
            ImageID = imageID;
            DesignID = designID;
            Score = score;
        }
    }
}

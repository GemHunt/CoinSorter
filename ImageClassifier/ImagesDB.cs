using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageClassifier
{
    class ImagesDB:SQLiteDB
    {
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
    }
}

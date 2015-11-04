using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ImageClassifier
{
    class ResultsDB:SQLiteDB
    {
        static public void AddResult(int modelID, int imageID, int labelID, double score)
        {
            StringBuilder SQL = new StringBuilder();

            SQL.AppendLine("BEGIN;");
            SQL.Append("Insert into Results (ModelID,ImageID,LabelID,Score) values (");
            SQL.Append(modelID + ",");
            SQL.Append(imageID + ",");
            SQL.Append(labelID + ",");
            SQL.AppendLine(score + ");");
            SQL.AppendLine("COMMIT;");
            ExecuteQuery(SQL.ToString());
            //sql_con.Close();
        }
        static public void AddResults(List<Result> results)
        {
            StringBuilder SQL = new StringBuilder();
            SQL.AppendLine("BEGIN;");
            
            foreach (Result result in results){
                SQL.Append("Insert into Results (ModelID,ImageID,LabelID,Score) values (");
                SQL.Append(result.ModelID + ",");
                SQL.Append(result.ImageID + ",");
                SQL.Append(result.LabelID + ",");
                SQL.AppendLine(result.Score + ");");
            }
            SQL.AppendLine("COMMIT;");
            Open();
            ExecuteQuery(SQL.ToString());
            Close();
        }

    }
}

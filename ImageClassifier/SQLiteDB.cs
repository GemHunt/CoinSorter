using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ImageClassifier
{
    class SQLiteDB
    {
        static private SQLiteConnection sql_con;
        static private SQLiteCommand sql_cmd;
        
        public void New()
        {
        }

        static public void Open()
        {
            sql_con = new SQLiteConnection
                ("Data Source=C:/Users/pkrush/Documents/GemHunt/CoinSorter/networks/classify.db;Version=3;New=False;Compress=True;");
            sql_con.Open();

        }

        static public void ExecuteQuery(string txtQuery)
        {
            sql_cmd = sql_con.CreateCommand();
            sql_cmd.CommandText = txtQuery;
            sql_cmd.ExecuteNonQuery();
        }


        static public SQLiteDataReader GetNewReader(string SQL)
        {
            SQLiteCommand command = new SQLiteCommand(sql_con);
            command.CommandText = SQL;
            SQLiteDataReader reader = command.ExecuteReader();
            return reader;
        }


        static public void Close()
        {
            sql_con.Close();
        }
    }
}

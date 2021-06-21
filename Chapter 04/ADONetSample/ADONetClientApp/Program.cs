using System;
using System.Data;
using System.IO;
using ADONetLibrary;

namespace ADONetClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string dbPath = (Path.Combine("../../../..", "Logstorage.db"));

            TestInsert(@"Data source = " + dbPath, "SQLITE");
        }

        static void TestInsert(string conStr, string driver)
        {
            DbEngineAdapter db = new DbEngineAdapter(conStr, driver);

            //A test log entry
            string test = "Log value is " + Math.PI * 1999;

            if (db.Open())
            {
                string query = "INSERT INTO logs VALUES('" + test + "');";

                bool result = db.ExecuteNonQuery(query);
            }

            db.Close();

            return;
        }

        static void TestDataSet(string conStr, string driver)
        {
            IDbEngineAdapter db = new DbEngineAdapter(conStr, driver);

            if (db.Open())
            {
                string query = "SELECT * FROM logs";
                DataSet ds = db.Execute(query);
                DataTable dt = ds.Tables[0];
                int i = 0;
                int max = dt.Rows.Count;

                while (i < max)
                {
                    DataRow dr = dt.Rows[i];
                    Console.WriteLine(dr[0]);
                    i++;
                }
            }

            db.Close();

            return;
        }

        static void TestDataReader(string conStr, string driver)
        {
            IDbEngineAdapter db = new DbEngineAdapter(conStr, driver);
            string query = "SELECT * FROM logs";

            if (db.Open())
            {
                IDataReader reader = db.ExecuteQuery(query);

                while (reader.Read())
                {
                    Console.WriteLine(reader.GetString(1));
                }
            }

            db.Close();
        }
    }
}

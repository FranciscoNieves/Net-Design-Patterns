using System;
using System.Data;
using System.Data.SQLite;
using System.Runtime.Serialization;

namespace ADONetLibrary
{
    [Serializable()]
    public class SQLiteDbFactory : DbAbstractFactory, ISerializable
    {
        public string DriverType { get; set; }

        public SQLiteDbFactory()
        {
            DriverType = null;
        }

        public SQLiteDbFactory(SerializationInfo info, StreamingContext context)
        {
        }

        public override IDbConnection CreateConnection(string conStr)
        {
            if (string.IsNullOrEmpty(conStr))
            {
                return null;
            }

            return new SQLiteConnection(conStr);
        }

        public override IDbCommand CreateCommand(IDbConnection con, string cmd)
        {
            if (con == null || string.IsNullOrEmpty(cmd))
            {
                return null;
            }

            if (con is SQLiteConnection)
            {
                return new SQLiteCommand(cmd, (SQLiteConnection)con);
            }

            return null;
        }

        public override IDbDataAdapter CreateDbAdapter(IDbCommand cmd)
        {
            if (cmd == null)
            {
                return null;
            }

            if (cmd is SQLiteCommand)
            {
                return new SQLiteDataAdapter((SQLiteCommand)cmd);
            }

            return null;
        }

        public override IDataReader CreateDataReader(IDbCommand cmd)
        {
            if (cmd == null)
            {
                return null;
            }

            if (cmd is SQLiteCommand)
            {
                return (SQLiteDataReader)cmd.ExecuteReader();
            }

            return null;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
        }
    }
}

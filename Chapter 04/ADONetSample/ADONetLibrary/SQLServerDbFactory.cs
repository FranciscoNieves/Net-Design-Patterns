using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace ADONetLibrary
{
    [Serializable()]
    public class SQLServerDbFactory : DbAbstractFactory, ISerializable
    {
        private string DriverType { get; set; }

        public SQLServerDbFactory()
        {
            DriverType = null;
        }

        protected SQLServerDbFactory(SerializationInfo info, StreamingContext context)
        {
        }

        //Create a Connection object
        //Returns a reference to a IDbConnection Interfce
        public override IDbConnection CreateConnection(string conStr)
        {
            if (string.IsNullOrEmpty(conStr))
            {
                return null;
            }

            return new SqlConnection(conStr);
        }

        public override IDbCommand CreateCommand(IDbConnection con, string cmd)
        {
            if (con == null || string.IsNullOrEmpty(cmd))
            {
                return null;
            }

            if (con is SqlConnection)
            {
                return new SqlCommand(cmd, (SqlConnection)con);
            }

            return null;
        }

        public override IDbDataAdapter CreateDbAdapter(IDbCommand cmd)
        {
            if (cmd == null)
            {
                return null;
            }

            if (cmd is SqlCommand)
            {
                return new SqlDataAdapter((SqlCommand)cmd);
            }

            return null;
        }

        public override IDataReader CreateDataReader(IDbCommand cmd)
        {
            if (cmd == null)
            {
                return null;
            }

            if (cmd is SqlCommand)
            {
                return (SqlDataReader)cmd.ExecuteReader();
            }

            return null;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
        }
    }
}

using System;
using System.Data;
using System.Data.Odbc;
using System.Runtime.Serialization;

namespace ADONetLibrary
{
    [Serializable()]
    public class ODbcDbFactory : DbAbstractFactory, ISerializable
    {
        public string DriverType { get; set; }

        public ODbcDbFactory()
        {
            DriverType = null;
        }

        protected ODbcDbFactory(SerializationInfo info, StreamingContext context)
        {
        }

        public override IDbConnection CreateConnection(string conStr)
        {
            if (string.IsNullOrEmpty(conStr))
            {
                return null;
            }

            return new OdbcConnection(conStr);
        }

        public override IDbCommand CreateCommand(IDbConnection con, string cmd)
        {
            if (con == null || string.IsNullOrEmpty(cmd))
            {
                return null;
            }

            if (con is OdbcConnection)
            {
                return new OdbcCommand(cmd, (OdbcConnection)con);
            }

            return null;
        }

        public override IDbDataAdapter CreateDbAdapter(IDbCommand cmd)
        {
            if (cmd == null)
            {
                return null;
            }

            if (cmd is OdbcCommand)
            {
                return new OdbcDataAdapter((OdbcCommand)cmd);
            }

            return null;
        }

        public override IDataReader CreateDataReader(IDbCommand cmd)
        {
            if (cmd == null)
            {
                return null;
            }

            if (cmd is OdbcCommand)
            {
                return (OdbcDataReader)cmd.ExecuteReader();
            }

            return null;
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
        }
    }
}

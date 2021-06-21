using System.Data;

namespace ADONetLibrary
{
    public abstract class DbAbstractFactory
    {
        public abstract IDbConnection CreateConnection(string conStr);
        public abstract IDbCommand CreateCommand(IDbConnection con, string cmd);
        public abstract IDbDataAdapter CreateDbAdapter(IDbCommand cmd);
        public abstract IDataReader CreateDataReader(IDbCommand cmd);
    }
}

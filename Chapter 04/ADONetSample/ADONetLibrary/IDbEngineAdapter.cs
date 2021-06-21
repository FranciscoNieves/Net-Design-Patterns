using System.Data;

namespace ADONetLibrary
{
    public interface IDbEngineAdapter
    {
        bool Open();
        DataSet Execute(string SQL);
        IDataReader ExecuteQuery(string SQL);
        bool ExecuteNonQuery(string SQL);
        bool Close();
    }
}

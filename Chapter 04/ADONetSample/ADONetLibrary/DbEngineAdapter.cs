using System;
using System.Data;
using System.IO;

namespace ADONetLibrary
{
    public class DbEngineAdapter : IDbEngineAdapter
    {
        static ObjectFactory of = new ObjectFactory(Path.Combine("../../../..", "DbDrivers.xml"));
        private IDbConnection _con = null;
        private IDbCommand _cmd = null;
        private DbAbstractFactory df = null;
        private string _conStr;
        private string _driver;

        public DbEngineAdapter(string conStr, string driver)
        {
            _conStr = conStr;
            _driver = driver;

            //Instantiate the correct concrete class based on the driver
            df = (DbAbstractFactory)of.Get(driver, "prototype");
        }

        public bool Open()
        {
            try
            {
                if (_con != null || df == null || _conStr == null)
                {
                    return false;
                }

                //Create Connection object
                _con = df.CreateConnection(_conStr);

                if (_con == null)
                {
                    return false;
                }

                _con.Open();

                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();

                return false;
            }
        }

        public DataSet Execute(string SQL)
        {
            try
            {
                if (_con == null || df == null || _cmd != null)
                {
                    return null;
                }

                _cmd = df.CreateCommand(_con, SQL);
                IDbDataAdapter da = df.CreateDbAdapter(_cmd);

                if (da == null)
                {
                    return null;
                }

                DataSet ds = new DataSet();

                da.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                ex.ToString();

                return null;
            }
        }

        public IDataReader ExecuteQuery(string SQL)
        {
            try
            {
                if (_con == null || df == null || _cmd != null)
                {
                    return null;
                }

                _cmd = df.CreateCommand(_con, SQL);

                if (_cmd == null)
                {
                    return null;
                }

                IDataReader rs = df.CreateDataReader(_cmd);

                return rs;
            }
            catch (Exception ex)
            {
                ex.ToString();

                return null;
            }
        }

        public bool ExecuteNonQuery(string SQL)
        {
            try
            {
                if (_con == null || df == null || _cmd != null)
                {
                    return false;
                }

                _cmd = df.CreateCommand(_con, SQL);

                if (_cmd == null)
                {
                    return false;
                }

                _cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();

                return false;
            }
        }

        public bool Close()
        {
            if (_con != null)
            {
                _con.Close();
                _con = null;

                return true;
            }

            return false;
        }
    }
}

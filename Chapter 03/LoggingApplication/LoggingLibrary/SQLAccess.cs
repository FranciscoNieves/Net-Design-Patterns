using System;
using System.Data.SQLite;

namespace LogLibrary
{
    public class SQLAccess
    {
        private SQLiteConnection _con = null;
        private SQLiteCommand _cmd = null;
        private SQLiteTransaction _cts = null;
        private string _constr;

        public SQLAccess(string constr)
        {
            _constr = constr;
        }

        public bool Open(bool trans = false)
        {
            try
            {
                _con = new SQLiteConnection(_constr);
                _con.Open();

                if (trans)
                {
                    _cts = _con.BeginTransaction();
                }

                return true;
            }
            catch (SQLiteException ex)
            {
                return false;
            }
        }

        public bool Close()
        {
            if (_con != null)
            {
                if (_cts != null)
                {
                    _cts.Commit();
                    _cts = null;
                }

                _con.Close();
                _con = null;

                return true;
            }

            return false;
        }

        public bool ExecuteNonQuery(string SQL)
        {
            try
            {
                _cmd = new SQLiteCommand(SQL, _con);
                _cmd.ExecuteNonQuery();

                _con.Close();
                _con = null;

                return true;
            }
            catch (Exception ex)
            {
                //_con.Close();
                _con = null;

                return false;
            }
        }
    }
}

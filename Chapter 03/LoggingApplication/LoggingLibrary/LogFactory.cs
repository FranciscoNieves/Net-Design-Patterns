using System;
using System.Security.Cryptography;

namespace LogLibrary
{
    public abstract class LogStrategy
    {
        //DoLog is our Template method
        //Concrete classes will override this
        protected abstract bool DoLog(string logitem);

        public bool Log(string app, string key, string cause)
        {
            return DoLog(app + " " + key + " " + cause);
        }
    }

    public class NullLogStrategy : LogStrategy
    {
        protected override bool DoLog(string logitem)
        {
            // Log into the Console
            Console.WriteLine(logitem + "\r\n");

            return true;
        }
    }

    public class DbLogStrategy : LogStrategy
    {
        BaseContentWriter wt = new DbContentWriter();

        protected override bool DoLog(string logitem)
        {
            // Log into the Database
            wt.Write(logitem);

            return true;
        }
    }

    public class FileLogStrategy : LogStrategy
    {
        BaseContentWriter wt = new FileContentWriter(@"log.txt");

        protected override bool DoLog(string logitem)
        {
            // Log into the File
            wt.Write(logitem);

            return true;
        }
    }

    public class NetLogStrategy : LogStrategy
    {
        BaseContentWriter nc = new NetworkContentWriter();

        protected override bool DoLog(string logitem)
        {
            //Log into the network socket
            nc.Write(logitem);

            return true;
        }
    }

    public class LoggerFactory
    {
        private static ObjectFactory of = new ObjectFactory("LogStrategy.xml");

        public static LogStrategy CreateLogger(string loggertype)
        {
            LogStrategy sf = (LogStrategy)of.Get(loggertype);

            return sf ?? new NullLogStrategy();
        }
        //public static LogStrategy CreateLogger(string loggerType)
        //{
        //    if (loggerType == "DB")
        //    {
        //        return new DbLogStrategy();
        //    }
        //    else if (loggerType == "File")
        //    {
        //        return new FileLogStrategy();
        //    }
        //    else if (loggerType == "NET")
        //    {
        //        return new NetLogStrategy();
        //    }
        //    else
        //    {
        //        return new NullLogStrategy();
        //    }
        //}
    }
}

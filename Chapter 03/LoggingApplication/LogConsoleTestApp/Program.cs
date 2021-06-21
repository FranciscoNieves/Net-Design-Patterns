using LogLibrary;

namespace LogConsoleTestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                return;
            }

            string loggertype = args[0];
            LogStrategy lf = LoggerFactory.CreateLogger(loggertype);
            Table(lf);
        }

        private static bool Table(LogStrategy ls)
        {
            int a = 10;
            int b = 1;

            while (b < 100)
            {
                ls.Log("Table", a.ToString() + " * " + b.ToString(), "=" + (a * b).ToString());
                b++;
            }

            return true;
        }
    }
}

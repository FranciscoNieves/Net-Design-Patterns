using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace LogLibrary
{
    public class NetworkContentWriter : BaseContentWriter
    {
        private static string domain = "127.0.0.1";
        private static int port = 4500;

        public NetworkContentWriter()
        {
        }

        public override bool WriteToMedia(string logcontent)
        {
            TcpClient _client = new TcpClient();

            if (_client == null)
            {
                return false;
            }

            try
            {
                _client.Connect(domain, port);
            }
            catch (Exception ex)
            {
                return false;
            }

            StreamWriter _sWriter = new StreamWriter(_client.GetStream(), Encoding.ASCII);
            _sWriter.WriteLine(logcontent);
            _sWriter.Flush();
            _sWriter.Close();

            _client.Close();

            return true;
        }
    }
}

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace LogServer
{
    class LogSocketServer
    {
        private TcpListener _server;
        private bool _running;
        private int port = 4500;

        public LogSocketServer()
        {
            _server = new TcpListener(IPAddress.Any, port);
            _server.Start();
            _running = true;

            AcceptClients();
        }

        public void AcceptClients()
        {
            while (_running)
            {
                TcpClient newClient = _server.AcceptTcpClient();
                Thread t = new Thread(new ParameterizedThreadStart(
                    HandleClientData));

                t.Start(newClient);
            }
        }

        public void HandleClientData(object obj)
        {
            TcpClient client = obj as TcpClient;
            StreamReader sReader = new StreamReader(client.GetStream(), Encoding.ASCII);

            bool bRead = true;

            while (bRead == true)
            {
                string sData = sReader.ReadLine();

                if (sData == null || sData.Length == 0)
                {
                    bRead = false;
                }

                Console.WriteLine();
            }
        }
    }
}

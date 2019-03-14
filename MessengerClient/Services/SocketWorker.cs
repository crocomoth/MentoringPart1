using MessengerCommon.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MessengerClient.Services
{
    public class SocketWorker
    {
        public bool stopReading;
        private Socket mainSocket;
        private Thread thread;
        private byte[] buffer;
        private ByteFormatter byteFormatter;
        private int amount;
        private string dataAsString;
        private MainWorker mainWorker;

        public SocketWorker(MainWorker main)
        {
            this.buffer = new byte[1024];
            this.stopReading = false;
            this.byteFormatter = new ByteFormatter();
            dataAsString = string.Empty;
            this.mainWorker = main;
        }

        public void Initialize()
        {
            this.mainSocket = new Socket(SocketType.Stream, ProtocolType.IPv4);
            mainSocket.Connect(IPAddress.Loopback, 1050);

        }

        private void Listen(object socketAsObj)
        {
            Socket socket = (Socket)socketAsObj;
            while (!stopReading)
            {
                amount = mainSocket.Receive(buffer);
                dataAsString = this.byteFormatter.ConvertToString(buffer, 0, amount);
            }
        }

        private void Send(string data)
        {

        }

        private string ParseMessage(string dataAsString)
        {
            string[] splitted = dataAsString.Split(new char[] { ':' }, 2);
            if (splitted.Length > 1)
            {
                var message = splitted[1];
                var parts = message.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            }
        }
    }
}

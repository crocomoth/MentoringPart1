using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MessengerServer.Services
{
    public class Listener
    {
        private MessageQueue messageQueue;
        private Socket mainSocket;
        private List<Thread> Threads;

        public Listener()
        {
            this.messageQueue = new MessageQueue();
            this.Threads = new List<Thread>();
        }

        public void Initialize()
        {
            this.mainSocket = new Socket(SocketType.Stream, ProtocolType.IPv4);
            this.mainSocket.Bind(new IPEndPoint(IPAddress.Loopback, 1050));
            this.mainSocket.Listen(20);
        }

        public void Listen()
        {
            while (true)
            {
                var clientSocket = this.mainSocket.Accept();
                var thread = new Thread(() => ReadDataFromClient(clientSocket));
                this.Threads.Add(thread);
            }
        }

        private void ReadDataFromClient(object socketAsObj)
        {
            var socket = (Socket)socketAsObj;
            byte[] data = new byte[1024];
            while (true)
            {
                int amount = socket.Receive(data);
                var dataAsString = Encoding.Unicode.GetString(data, 0, amount);
                this.ParseString(dataAsString);
            }
        }

        private void ParseString(string dataAsString)
        {
            throw new NotImplementedException();
        }
    }
}

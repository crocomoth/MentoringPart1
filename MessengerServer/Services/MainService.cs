using MessengerCommon.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace MessengerServer.Services
{
    public class MainService
    {
        private MessageQueue messageQueue;
        public List<ClientSocketWrapper> clientWorkers;
        //lock object to lock collection of workers
        public object locker;
        private Socket mainSocket;

        public MainService()
        {
            this.messageQueue = new MessageQueue();
            this.clientWorkers = new List<ClientSocketWrapper>();
            this.locker = new object();
        }

        public void Initialize()
        {
            this.mainSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            this.mainSocket.Bind(new IPEndPoint(IPAddress.Loopback, 1050));
            this.mainSocket.Listen(20);
        }

        public void Listen()
        {
            while (true)
            {
                var clientSocket = this.mainSocket.Accept();
                var worker = new ClientSocketWrapper(clientSocket, this);
                lock (locker)
                {
                    this.clientWorkers.Add(worker);
                    worker.StartWorkWithClient();
                }
            }
        }

        public void SendMessageToClients(string text)
        {
            lock (locker)
            {
                foreach (var worker in clientWorkers)
                {
                    worker.SendMessage(text);
                }
            }
        }

        public void RemoveWorker(ClientSocketWrapper worker)
        {
            lock (locker)
            {
                this.clientWorkers.Remove(worker);
            }
        }

        public void AddMessageToQueue(Message message)
        {
            this.messageQueue.AddToQueue(message);
        }

        public List<Message> GetMessagesFromQueue()
        {
            return this.messageQueue.GetAllMessages();
        }
    }
}

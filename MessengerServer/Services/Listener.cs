using MessengerCommon;
using MessengerServer.Exceptions;
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
        private CommandConverter commandConverter;
        private MessageConverter messageConverter;
        private ByteFormatter byteFormatter;
        private Dictionary<CommandEnum, Action<string>> actions;

        public Listener()
        {
            this.messageQueue = new MessageQueue();
            this.Threads = new List<Thread>();
            commandConverter = new CommandConverter();
            messageConverter = new MessageConverter();
            byteFormatter = new ByteFormatter();
            actions = new Dictionary<CommandEnum, Action<string>>();
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
            byte[] data = new byte[1024];//ok for now
            var clientName = string.Empty;
            int amount = 0;
            string dataAsString = string.Empty;

            // Receive login. Variables will be reused in data reading;
            try
            {
                amount = socket.Receive(data);
                dataAsString = this.byteFormatter.ConvertToString(data, 0, amount);
                clientName = this.ParseName(dataAsString);
                this.PushHistoryToClient(socket);

                while (true)
                {
                    amount = socket.Receive(data);
                    dataAsString = this.byteFormatter.ConvertToString(data, 0, amount);
                    this.ExecuteCommand(dataAsString);
                }
            }
            catch (WrongCommandException)
            {
                //Notify ? abort if wrong command
            }
            finally
            {
                socket.Dispose();
            }
        }

        private void ExecuteCommand(string dataAsString)
        {
            string[] splitted = dataAsString.Split(new char[] { ':' }, 2);
            var commandAsString = splitted[0];
            CommandEnum command = this.commandConverter.GetCommand(commandAsString);

            if (this.actions.TryGetValue(command, out var func))
            {
                func.Invoke(splitted.Length > 1 ? splitted[1] : null);
                return;
            }

            throw new WrongCommandException();
        }

        private void PushHistoryToClient(Socket socket)
        {
            var history = this.messageConverter.ConvertHistory(messageQueue.GetAllMessages());
            var historyAsByteArray = this.byteFormatter.ConvertToByteArray(history);
            socket.Send(historyAsByteArray);
        }

        private string ParseName(string dataAsString)
        {
            string[] splitted = dataAsString.Split(new char[] { ':' }, 2);
            var commandAsString = splitted[0];
            CommandEnum command = this.commandConverter.GetCommand(commandAsString);
            if (command != CommandEnum.Login)
            {
                throw new WrongCommandException($"login expected but reveived {nameof(command)}");
            }

            // It is login command
            return splitted[1];
        }
    }
}

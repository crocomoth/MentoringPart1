using MessengerCommon.Models;
using MessengerCommon.Services;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MessengerClient.Services
{
    public class SocketWorker: IDisposable
    {
        private const string saidConstant = " said on ";
        public bool stopReading;
        private string userName;
        private Socket mainSocket;
        private Thread thread;
        private byte[] buffer;
        private ByteFormatter byteFormatter;
        private int amount;
        private string dataAsString;
        private MainWorker mainWorker;
        private MessageConverter messageConverter;
        private ConsoleLogger logger;

        public SocketWorker(MainWorker main)
        {
            this.buffer = new byte[10000];
            this.stopReading = false;
            this.byteFormatter = new ByteFormatter();
            dataAsString = string.Empty;
            this.mainWorker = main;
            this.messageConverter = new MessageConverter();
            this.logger = new ConsoleLogger();
        }

        public void Initialize()
        {
            this.mainSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            try
            {
                mainSocket.Connect(IPAddress.Loopback, 1050);
            }
            catch (Exception)
            {
                this.logger.Log("server seems not to be up sorry :(");
                this.Dispose();
                Console.ReadLine();
                throw;
            }

            this.thread = new Thread(() => Listen(mainSocket));
        }

        public void StartListening()
        {
            this.thread.Start();
        }

        private void Listen(object socketAsObj)
        {
            Socket socket = (Socket)socketAsObj;
            try
            {
                while (!stopReading)
                {
                    amount = mainSocket.Receive(buffer);
                    if (amount > 0)
                    {
                        dataAsString = this.byteFormatter.ConvertToString(buffer, 0, amount);
                        var messageData = ParseMessage(dataAsString);
                        this.mainWorker.WriteToConsole(messageData);
                    }
                }
            }
            catch (SocketException)
            {
                //user shoudlnt know when it is closed
                logger.Log("server error closing app...");
            }
            catch(Exception e)
            {
                logger.Log("got exception. Exit application", e);
            }
            finally
            {
                this.Dispose();
            }

        }

        public void Send(string data)
        {
            var message = new Message(userName, data, DateTime.Now);
            var text = this.messageConverter.ConvertMessage(message);
            var byteArray = this.byteFormatter.ConvertToByteArray(text);
            mainSocket.Send(byteArray);
        }

        public void SendName(string name)
        {
            this.userName = name;
            var text = nameof(CommandEnum.Login) + ":" + name;
            var byteArray = this.byteFormatter.ConvertToByteArray(text);
            mainSocket.Send(byteArray);
            this.GetHistory();
        }

        private void GetHistory()
        {
            int amount = this.mainSocket.Receive(buffer);
            dataAsString = this.byteFormatter.ConvertToString(buffer, 0, amount);
            var commandAndData = dataAsString.Split(new[] { ':' }, 2);
            //less nested + shorter
            if (commandAndData[1].Length == 0)
            {
                return;
            }

            var parts = commandAndData[1].Split(new[] { '\x0003' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var part in parts)
            {
                var messageData = ParseMessage(part);
                this.mainWorker.WriteToConsole(messageData);
            }

        }

        public void CloseConnection()
        {
            var text = nameof(CommandEnum.LogOut) + ":";
            var byteArrat = this.byteFormatter.ConvertToByteArray(text);
            mainSocket.Send(byteArrat);
        }

        private string[] ParseMessage(string dataAsString)
        {
            string[] splitted = dataAsString.Split(new char[] { ':' }, 2);
            
            if (splitted.Length > 1)
            {
                var message = splitted[1];
                var parts = message.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

                string[] result = new string[2];
                result[0] = parts[0] + saidConstant + parts[1];
                result[1] = parts[2];
                return result;
            }

            throw new Exception("wrong number of elements in message");
        }

        public void Dispose()
        {
            this.stopReading = true;
            this.mainSocket.Dispose();
        }
    }
}

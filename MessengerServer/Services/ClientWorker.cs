using MessengerCommon.Exceptions;
using MessengerCommon.Models;
using MessengerCommon.Services;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

namespace MessengerServer.Services
{
    public class ClientWorker : IDisposable
    {
        public Socket socket;
        private byte[] data;
        private string clientName;
        private int amount;
        private string dataAsString;
        private Thread thread;
        private Listener parent;
        private CommandConverter commandConverter;
        private MessageConverter messageConverter;
        private ByteFormatter byteFormatter;
        private Dictionary<CommandEnum, Action<string>> actions;
        private ConsoleLogger logger;
        private bool shouldExit;

        public ClientWorker(Socket socket, Listener listener)
        {
            this.socket = socket;
            data = new byte[1024];//ok for now
            clientName = string.Empty;
            amount = 0;
            dataAsString = string.Empty;
            this.parent = listener;
            commandConverter = new CommandConverter();
            messageConverter = new MessageConverter();
            byteFormatter = new ByteFormatter();
            logger = new ConsoleLogger();

            this.shouldExit = false;

            actions = new Dictionary<CommandEnum, Action<string>>();
            actions.Add(CommandEnum.Message, StartSending);
            actions.Add(CommandEnum.LogOut, LogOut);
        }

        public void StartWorkWithClient()
        {
            this.thread = new Thread(() => WorkWithClient(socket));
            this.thread.Start();
        }

        private void WorkWithClient(Socket socket)
        {
            try
            {
                amount = socket.Receive(data);
                dataAsString = this.byteFormatter.ConvertToString(data, 0, amount);
                clientName = this.ParseName(dataAsString);
                this.logger.Log($"got client with name {clientName}");
                this.PushHistoryToClient(socket);

                while (!shouldExit)
                {
                    amount = socket.Receive(data);
                    dataAsString = this.byteFormatter.ConvertToString(data, 0, amount);
                    this.ExecuteCommand(dataAsString);
                }
            }
            catch (WrongCommandException)
            {
                logger.Log("wrong command specified");
            }
            catch (SocketException e)
            {
                logger.Log("connection closed", e);
            }
            finally
            {
                this.Dispose();
            }
        }

        //object can be extracted - but it will be a god object. Or a whole new hierarchy is needed to support this 2 functions
        #region CommandParsing
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
        #endregion

        //methods to send messages/process commands are here
        #region Messaging
        public void SendMessage(string text)
        {
            var message = this.messageConverter.CreateMessage(text);
            this.parent.AddMessageToQueue(message);
            //replace with message convert
            var convertedMessage = this.messageConverter.ComposeMessage(text);
            var bytePackage = this.byteFormatter.ConvertToByteArray(convertedMessage);
            // try catch can be used here to work with errors
            this.socket.Send(bytePackage);
            this.logger.Log($"message sent on {DateTime.Now}");
        }

        //send history to client
        private void PushHistoryToClient(Socket socket)
        {
            var history = this.messageConverter.ConvertHistory(this.parent.GetMessagesFromQueue());
            var historyAsByteArray = this.byteFormatter.ConvertToByteArray(history);
            if (historyAsByteArray.Length == 0)
            {
                socket.Send(this.byteFormatter.ConvertToByteArray(" "));
            }
            socket.Send(historyAsByteArray);
        }

        //notify parent that message should be sent
        private void StartSending(string text)
        {
            this.parent.SendMessageToClients(text);
        }

        //break from cycle
        private void LogOut(string text)
        {
            this.shouldExit = true;
        }
        #endregion

        public void Dispose()
        {
            //remove this from active workers and close socket
            this.parent.RemoveWorker(this);
            this.socket.Dispose();
        }
    }
}

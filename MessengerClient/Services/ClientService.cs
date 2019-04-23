using System;
using System.Threading;
using MessengerClient.Services.Interfaces;

namespace MessengerClient.Services
{
    public class ClientService : IClientService
    {
        private readonly Thread thread;
        private string data;
        private bool isWorking;
        private readonly ISocketWrapper wrapper;

        public ClientService()
        {
            this.data = string.Empty;
            this.isWorking = true;
            this.wrapper = new SocketWrapper(this);
            this.wrapper.threadFinished += this.HandleWrapperClosing; 
            this.thread = new Thread(() => ReadFromConsole());
        }

        public void Start()
        {
            this.wrapper.Initialize();
            Console.WriteLine("enter username");
            var firstInput = string.Empty;
            do
            {
                firstInput = Console.ReadLine();
            } while (firstInput == string.Empty);

            this.wrapper.SendName(firstInput);

            this.thread.Start();
            this.wrapper.StartListening();
        }

        public void WriteToConsole(string text)
        {
            Console.WriteLine(text);
        }

        public void WriteToConsole(string[] texts)
        {
            foreach (var text in texts)
            {
                Console.WriteLine(text);
            }
        }

        public void HandleWrapperClosing()
        {
            this.isWorking = false;
            Console.WriteLine("error occured press enter to exit");
        }

        private void ReadFromConsole()
        {
            while (true)
            {
                this.data = Console.ReadLine();
                if (this.data == "quit")
                {
                    break;
                }

                if (!isWorking)
                {
                    break;
                }

                try
                {
                    this.wrapper.Send(data);
                }
                catch (Exception)
                {
                    Console.WriteLine("Server is unreachable");
                    isWorking = false;
                }

            }

            this.wrapper.StopReading = true;
            this.wrapper.Dispose();
        }
    }
}

using System;
using System.Threading;

namespace MessengerClient.Services
{
    public class ClientService
    {
        private Thread thread;
        private string data;
        private bool isWorking;
        private SocketWrapper worker;

        public ClientService()
        {
            this.data = string.Empty;
            this.isWorking = true;
            this.worker = new SocketWrapper(this);
            this.thread = new Thread(() => ReadFromConsole());
        }

        public void Start()
        {
            this.worker.Initialize();
            Console.WriteLine("enter username");
            var firstInput = string.Empty;
            do
            {
                firstInput = Console.ReadLine();
            } while (firstInput == string.Empty);

            this.worker.SendName(firstInput);

            this.thread.Start();
            this.worker.StartListening();
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

        private void ReadFromConsole()
        {
            while (isWorking)
            {
                this.data = Console.ReadLine();
                if (this.data == "quit")
                {
                    break;
                }

                this.worker.Send(data);
            }

            this.worker.stopReading = true;
            this.worker.Dispose();
        }
    }
}

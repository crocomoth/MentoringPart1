using System;
using System.Threading;

namespace MessengerClient.Services
{
    public class MainWorker
    {
        public string UserName;
        private Thread thread;
        private string data;
        private bool isWorking;
        private SocketWorker worker;

        public MainWorker()
        {
            data = string.Empty;
            isWorking = true;
            worker = new SocketWorker(this);
            worker.Initialize();
            thread = new Thread(() => ReadFromConsole());
        }

        public void Start()
        {
            Console.WriteLine("enter username");
            var firstInput = string.Empty;
            do
            {
                firstInput = Console.ReadLine();
            } while (firstInput == string.Empty);

            worker.SendName(firstInput);

            thread.Start();
            worker.StartListening();
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
                data = Console.ReadLine();
                if (data == "quit")
                {
                    break;
                }

                worker.Send(data);
            }

            this.worker.stopReading = true;
            this.worker.Dispose();
        }
    }
}

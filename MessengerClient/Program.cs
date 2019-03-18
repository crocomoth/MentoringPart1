using MessengerClient.Services;
using System;

namespace MessengerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            MainWorker worker = new MainWorker();
            try
            {
                worker.Start();
            }
            catch (Exception)
            {
                //catch in case server is not running
                return;
            }
        }
    }
}

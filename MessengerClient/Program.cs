using MessengerClient.Services;
using System;

namespace MessengerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ClientService worker = new ClientService();
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

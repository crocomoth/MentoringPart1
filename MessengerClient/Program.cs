using MessengerClient.Services;
using System;
using MessengerClient.Services.Interfaces;
using MessengerCommon.Services;

namespace MessengerClient
{
    class Program
    {
        static void Main(string[] args)
        {

            IClientService worker = InterceptSetter.SetInterceptorToClass(new ClientService());
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

using MessengerClient.Services;
using System;
using MessengerClient.Services.Interfaces;
using MessengerCommon.Services;
using NLog;
using NLog.Config;
using NLog.Targets;

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

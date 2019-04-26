using MessengerClient.Services;
using System;
using MessengerCommon.Services;
using NLog;
using NLog.Config;
using NLog.Targets;
using PostSharp.Patterns.Diagnostics;

namespace MessengerClient
{
    [Log(AttributeExclude = true)]
    class Program
    {
        static void Main(string[] args)
        {
            LoggingInitializer.Initialize();

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

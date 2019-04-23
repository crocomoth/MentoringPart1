using MessengerClient.Services;
using System;
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
            var config = new LoggingConfiguration();

            var fileTarget = new FileTarget("target2")
            {
                FileName = "${basedir}/clientLog.txt",
                Layout = "${longdate} ${level} ${message}  ${exception}"
            };
            config.AddTarget(fileTarget);

            config.AddRuleForAllLevels(fileTarget);
            LogManager.Configuration = config;

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

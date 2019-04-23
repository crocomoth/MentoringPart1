using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using MessengerCommon.Models;
using Newtonsoft.Json;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace MessengerCommon.Services
{
    public class LoggingInterceptor : IInterceptor
    {
        private readonly Logger logger;

        public LoggingInterceptor()
        {
            var config = new LoggingConfiguration();

            var fileTarget = new FileTarget("target")
            {
                FileName = "${basedir}/file.txt",
                Layout = "${longdate} ${level} ${message}  ${exception}"
            };
            config.AddTarget(fileTarget);

            config.AddRuleForAllLevels(fileTarget);
            LogManager.Configuration = config;

            logger = NLog.LogManager.GetCurrentClassLogger();
        }

        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();

            var loggingModel =
                new LoggingModel(invocation.Arguments, invocation.Method.Name, invocation.Method.ReturnType);

            var result = JsonConvert.SerializeObject(loggingModel);

            logger.Log(LogLevel.Info, result);
        }
    }
}

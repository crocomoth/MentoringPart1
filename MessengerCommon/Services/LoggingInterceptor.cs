using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using MessengerCommon.Models;
using Newtonsoft.Json;
using NLog;

namespace MessengerCommon.Services
{
    public class LoggingInterceptor : IInterceptor
    {
        private readonly Logger logger;

        public LoggingInterceptor()
        {
            logger = LogManager.GetLogger("interceptLogger");
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

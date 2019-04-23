using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerCommon.Models
{
    public class LoggingModel
    {
        public LoggingModel(object[] parameters, string methodName, Type returnType)
        {
            Parameters = parameters;
            MethodName = methodName;
            ReturnType = returnType;
        }

        public string MethodName { get; set; }

        public Type ReturnType { get; set; }

        public object[] Parameters { get; set; }
    }
}

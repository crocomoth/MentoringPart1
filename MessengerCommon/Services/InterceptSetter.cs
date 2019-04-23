using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.DynamicProxy;

namespace MessengerCommon.Services
{
    public static class InterceptSetter
    {
        private static readonly ProxyGenerator Generator = new ProxyGenerator();

        public static T SetInterceptorToClass<T>(T obj) where T : class 
        {
            return Generator.CreateClassProxyWithTarget(obj, new LoggingInterceptor());
        }
    }
}

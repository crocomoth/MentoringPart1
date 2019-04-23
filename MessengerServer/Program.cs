using MessengerServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessengerCommon.Services;

namespace MessengerServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MainService listener = InterceptSetter.SetInterceptorToClass(new MainService());
            listener.Initialize();
            listener.Listen();
        }
    }
}

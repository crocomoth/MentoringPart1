using MessengerServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessengerCommon.Services;
using PostSharp.Patterns.Diagnostics;

namespace MessengerServer
{
    [Log(AttributeExclude = true)]
    public class Program
    {
        public static void Main(string[] args)
        {
            LoggingInitializer.Initialize();

            MainService listener = new MainService();
            listener.Initialize();
            listener.Listen();
        }
    }
}

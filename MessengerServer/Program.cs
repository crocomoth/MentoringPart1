using MessengerServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Listener listener = new Listener();
            listener.Initialize();
            listener.Listen();
        }
    }
}

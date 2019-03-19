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
            MainService listener = new MainService();
            listener.Initialize();
            listener.Listen();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MessengerClient.Services
{
    public class MainWorker
    {
        private Thread thread;
        private string data;
        private bool isWorking;
        private SocketWorker worker;

        public MainWorker()
        {
            data = string.Empty;
            isWorking = true;
            worker = new SocketWorker(this);
        }

        public void Start()
        {
            
        }
    }
}

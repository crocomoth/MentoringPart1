﻿using MessengerClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerClient
{
    class Program
    {
        static void Main(string[] args)
        {
            MainWorker worker = new MainWorker();
            worker.Start();
        }
    }
}

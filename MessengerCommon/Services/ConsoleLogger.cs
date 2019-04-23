﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessengerCommon.Services.Interfaces;

namespace MessengerCommon.Services
{
    public class ConsoleLogger : IConsoleLogger
    {
        public virtual void Log(string message)
        {
            Console.WriteLine(message);
        }

        public virtual void Log(string message, Exception e)
        {
            Console.WriteLine(message + " " + e.Message);
        }
    }
}

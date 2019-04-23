using System;

namespace MessengerCommon.Services.Interfaces
{
    public interface IConsoleLogger
    {
        void Log(string message);
        void Log(string message, Exception e);
    }
}
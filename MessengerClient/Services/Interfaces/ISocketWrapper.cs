using System;

namespace MessengerClient.Services.Interfaces
{
    public interface ISocketWrapper
    {
        event Action threadFinished;

        bool StopReading { get; set; }
        void CloseConnection();
        void Dispose();
        void Initialize();
        void Send(string data);
        void SendName(string name);
        void StartListening();
    }
}
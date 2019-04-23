using System.Collections.Generic;
using MessengerCommon.Models;

namespace MessengerServer.Services.Interfaces
{
    public interface IMainService
    {
        void AddMessageToQueue(Message message);
        List<Message> GetMessagesFromQueue();
        void Initialize();
        void Listen();
        void RemoveWorker(ClientSocketWrapper worker);
        void SendMessageToClients(string text);
    }
}
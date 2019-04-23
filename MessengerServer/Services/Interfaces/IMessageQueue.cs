using System.Collections.Generic;
using MessengerCommon.Models;

namespace MessengerServer.Services.Interfaces
{
    public interface IMessageQueue
    {
        void AddToQueue(Message message);
        Message Dequeue();
        List<Message> GetAllMessages();
    }
}
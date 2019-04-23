using System.Collections.Generic;
using MessengerCommon.Models;

namespace MessengerCommon.Services.Interfaces
{
    public interface IMessageConverter
    {
        string ComposeMessage(string data);
        string ConvertHistory(List<Message> messages);
        string ConvertMessage(Message message);
        Message CreateMessage(string data);
    }
}
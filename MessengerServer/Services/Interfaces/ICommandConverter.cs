using MessengerCommon.Models;

namespace MessengerServer.Services.Interfaces
{
    public interface ICommandConverter
    {
        CommandEnum GetCommand(string data);
    }
}
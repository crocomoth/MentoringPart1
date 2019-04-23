using MessengerCommon.Models;
using MessengerServer.Services.Interfaces;

namespace MessengerServer.Services
{
    public class CommandConverter : ICommandConverter
    {
        public CommandEnum GetCommand(string data)
        {
            switch (data)
            {
                case nameof(CommandEnum.Login): return CommandEnum.Login;
                case nameof(CommandEnum.LogOut): return CommandEnum.LogOut;
                case nameof(CommandEnum.Message): return CommandEnum.Message;
                case nameof(CommandEnum.RecvHistory): return CommandEnum.RecvHistory;
                default:
                    return CommandEnum.Unknown;
            }
        }
    }
}

namespace MessengerServer.Services.Interfaces
{
    public interface IClientSocketWrapper
    {
        void SendMessage(string text);
        void StartWorkWithClient();
    }
}
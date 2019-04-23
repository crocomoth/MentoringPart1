namespace MessengerClient.Services.Interfaces
{
    public interface IClientService
    {
        void HandleWrapperClosing();
        void Start();
        void WriteToConsole(string text);
        void WriteToConsole(string[] texts);
    }
}
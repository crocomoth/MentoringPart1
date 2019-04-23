namespace MessengerCommon.Services.Interfaces
{
    public interface IByteFormatter
    {
        byte[] ConvertToByteArray(string data);
        string ConvertToString(byte[] array, int index, int count);
    }
}
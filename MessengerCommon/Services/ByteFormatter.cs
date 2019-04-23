using System.Text;
using MessengerCommon.Services.Interfaces;

namespace MessengerCommon.Services
{
    public class ByteFormatter : IByteFormatter
    {
        public byte[] ConvertToByteArray(string data)
        {
            return Encoding.Unicode.GetBytes(data);
        }

        public string ConvertToString(byte[] array,int index, int count)
        {
            return Encoding.Unicode.GetString(array, index, count);
        }
    }
}

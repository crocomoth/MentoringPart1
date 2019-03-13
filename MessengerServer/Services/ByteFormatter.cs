using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerServer.Services
{
    public class ByteFormatter
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

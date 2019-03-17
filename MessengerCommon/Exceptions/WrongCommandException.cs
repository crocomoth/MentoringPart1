using System;
using System.Runtime.Serialization;

namespace MessengerCommon.Exceptions
{
    public class WrongCommandException : Exception
    {
        public WrongCommandException()
        {
        }

        public WrongCommandException(string message) : base(message)
        {
        }

        public WrongCommandException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected WrongCommandException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

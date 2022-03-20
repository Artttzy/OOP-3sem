using System;
using System.Runtime.Serialization;

namespace BackupsExtra.Exceptions
{
    public class UnexpectedStateException : Exception
    {
        public UnexpectedStateException()
        {
        }

        public UnexpectedStateException(string message)
            : base(message)
        {
        }

        public UnexpectedStateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected UnexpectedStateException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

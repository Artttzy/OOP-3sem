using System;
using System.Runtime.Serialization;

namespace BackupsExtra.Exceptions
{
    public class AllRestorePointsDeletedException : Exception
    {
        public AllRestorePointsDeletedException()
        {
        }

        public AllRestorePointsDeletedException(string message)
            : base(message)
        {
        }

        public AllRestorePointsDeletedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected AllRestorePointsDeletedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}

using System;

namespace IsuExtra.Tools
{
    public class InvalidGroupNameIsuException : IsuException
    {
        public InvalidGroupNameIsuException()
        {
        }

        public InvalidGroupNameIsuException(string message)
            : base(message)
        {
        }

        public InvalidGroupNameIsuException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
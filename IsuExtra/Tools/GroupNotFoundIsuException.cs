using System;

namespace IsuExtra.Tools
{
    public class GroupNotFoundIsuException : IsuException
    {
        public GroupNotFoundIsuException()
        {
        }

        public GroupNotFoundIsuException(string message)
            : base(message)
        {
        }

        public GroupNotFoundIsuException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
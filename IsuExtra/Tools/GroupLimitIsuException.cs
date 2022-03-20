using System;

namespace IsuExtra.Tools
{
    public class GroupLimitIsuException : IsuException
    {
        public GroupLimitIsuException()
        {
        }

        public GroupLimitIsuException(string message)
            : base(message)
        {
        }

        public GroupLimitIsuException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
using System;

namespace IsuExtra.Tools
{
    public class RegisterIsFailedIsuException : IsuException
    {
        public RegisterIsFailedIsuException()
        {
        }

        public RegisterIsFailedIsuException(string message)
            : base(message)
        {
        }

        public RegisterIsFailedIsuException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
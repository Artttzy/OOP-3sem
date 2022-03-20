using System;

namespace IsuExtra.Tools
{
    public class StudentNotFoundIsuException : IsuException
    {
        public StudentNotFoundIsuException()
        {
        }

        public StudentNotFoundIsuException(string message)
            : base(message)
        {
        }

        public StudentNotFoundIsuException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
using System;

namespace Isu.Tools
{
    public class NoStudentInGroupIsuException : IsuException
    {
        public NoStudentInGroupIsuException()
        {
        }

        public NoStudentInGroupIsuException(string message)
            : base(message)
        {
        }

        public NoStudentInGroupIsuException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
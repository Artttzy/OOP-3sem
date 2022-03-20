using System;

namespace Reports.Exceptions
{
    public class DeleteEmployeeException : Exception
    {
        public DeleteEmployeeException(string message) : base(message)
        {
            
        }
    }
}
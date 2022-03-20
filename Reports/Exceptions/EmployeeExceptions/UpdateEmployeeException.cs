using System;

namespace Reports.Exceptions
{
    public class UpdateEmployeeException : Exception
    {
        public UpdateEmployeeException(string message) : base(message)
        {
            
        }
    }
}
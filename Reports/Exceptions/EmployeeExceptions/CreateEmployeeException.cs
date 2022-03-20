using System;

namespace Reports.Exceptions
{
    public class CreateEmployeeException : Exception
    {
        public CreateEmployeeException(string message) : base(message)
        {
            
        }
    }
}
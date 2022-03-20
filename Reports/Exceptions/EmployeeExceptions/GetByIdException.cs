using System;

namespace Reports.Exceptions
{
    public class GetByIdException : Exception
    {
        public GetByIdException(string message) : base(message)
        {
            
        }
    }
}
using System;

namespace Reports.Exceptions
{
    public class GetByPageException : Exception
    {
        public GetByPageException(string message) : base(message)
        {
            
        }
    }
}
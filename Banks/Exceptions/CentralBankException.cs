using System;

namespace Banks.Exceptions
{
    public class CentralBankException : Exception
    {
        public CentralBankException()
        {
        }

        public CentralBankException(string message)
            : base(message)
        {
        }

        public CentralBankException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
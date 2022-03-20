using System;

namespace Banks.Exceptions
{
    public class InvalidPassportCentralBankException : CentralBankException
    {
        public InvalidPassportCentralBankException()
        {
        }

        public InvalidPassportCentralBankException(string message)
            : base(message)
        {
        }

        public InvalidPassportCentralBankException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
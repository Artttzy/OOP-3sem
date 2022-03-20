using System;

namespace Banks.Exceptions
{
    public class ClientNotFoundCentralBankException : CentralBankException
    {
        public ClientNotFoundCentralBankException()
        {
        }

        public ClientNotFoundCentralBankException(string message)
            : base(message)
        {
        }

        public ClientNotFoundCentralBankException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
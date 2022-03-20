using System;

namespace Banks.Exceptions
{
    public class BankNotFoundCentralBankException : CentralBankException
    {
        public BankNotFoundCentralBankException()
        {
        }

        public BankNotFoundCentralBankException(string message)
            : base(message)
        {
        }

        public BankNotFoundCentralBankException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
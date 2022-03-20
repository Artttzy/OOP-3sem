using System;

namespace Banks.Exceptions
{
    public class DepositTermNotOverCentralBankException : CentralBankException
    {
        public DepositTermNotOverCentralBankException()
        {
        }

        public DepositTermNotOverCentralBankException(string message)
            : base(message)
        {
        }

        public DepositTermNotOverCentralBankException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
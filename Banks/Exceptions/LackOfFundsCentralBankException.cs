using System;

namespace Banks.Exceptions
{
    public class LackOfFundsCentralBankException : CentralBankException
    {
        public LackOfFundsCentralBankException()
        {
        }

        public LackOfFundsCentralBankException(string message)
            : base(message)
        {
        }

        public LackOfFundsCentralBankException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
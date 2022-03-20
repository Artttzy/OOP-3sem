using System;

namespace Banks.Exceptions
{
    public class SuspiciousLimitCentralBankException : CentralBankException
    {
        public SuspiciousLimitCentralBankException()
        {
        }

        public SuspiciousLimitCentralBankException(string message)
            : base(message)
        {
        }

        public SuspiciousLimitCentralBankException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
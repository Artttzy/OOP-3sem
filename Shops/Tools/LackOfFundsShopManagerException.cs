using System;

namespace Shops.Tools
{
    public class LackOfFundsShopManagerException : ShopManagerException
    {
        public LackOfFundsShopManagerException()
        {
        }

        public LackOfFundsShopManagerException(string message)
            : base(message)
        {
        }

        public LackOfFundsShopManagerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
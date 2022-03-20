using System;

namespace Shops.Tools
{
    public class ShopManagerException : Exception
    {
        public ShopManagerException()
        {
        }

        public ShopManagerException(string message)
            : base(message)
        {
        }

        public ShopManagerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
using System;

namespace Shops.Tools
{
    public class ProductIsNotRegisterShopManagerException : ShopManagerException
    {
        public ProductIsNotRegisterShopManagerException()
        {
        }

        public ProductIsNotRegisterShopManagerException(string message)
            : base(message)
        {
        }

        public ProductIsNotRegisterShopManagerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
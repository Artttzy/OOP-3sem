using System;

namespace Shops.Tools
{
    public class ProductNotFoundShopManagerException : ShopManagerException
    {
        public ProductNotFoundShopManagerException()
        {
        }

        public ProductNotFoundShopManagerException(string message)
            : base(message)
        {
        }

        public ProductNotFoundShopManagerException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
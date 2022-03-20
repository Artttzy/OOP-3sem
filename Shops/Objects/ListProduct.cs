namespace Shops
{
    public class ListProduct
    {
        private int _count;

        public ListProduct(Product product, int count)
        {
            Product = product;
            _count = count;
        }

        public Product Product { get; }
        public int Count => _count;
    }
}
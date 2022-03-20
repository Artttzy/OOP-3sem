namespace Shops
{
    public class ShopProduct : Product
    {
        private int _id;
        private string _name;
        public ShopProduct(int id, string name, float price, int count)
            : base(id, name)
        {
            _id = id;
            _name = name;
            Count = count;
            Price = price;
        }

        public float Price { get; set; } = 0;

        public int Count { get; set; }

        public int ShopId { get; }
    }
}
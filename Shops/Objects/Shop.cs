using System;
using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops
{
    public class Shop
    {
        private readonly List<ShopProduct> _products = new List<ShopProduct>();
        private IReadOnlyList<Product> _registrateProducts = new List<Product>();
        private int _id;
        private string _name;
        private string _address;

        public Shop(int id, string name, string address, IReadOnlyList<Product> products)
        {
            _id = id;
            _name = name;
            _address = address;
            _registrateProducts = products;
        }

        public int Id => _id;
        public string Name => _name;
        public string Address => _address;

        public ShopProduct FindProduct(string name)
        {
            return _products.Find(s => s.Name == name);
        }

        public void CheckProductInShop(string name)
        {
            if (_products.Find(p => p.Name == name) == null)
            {
                throw new ProductNotFoundShopManagerException("Product is not found in shop!");
            }
        }

        public void Supply(List<ShopProduct> products)
        {
            foreach (ShopProduct product in products)
            {
                CheckProduct(product);
                if (_products.Find(p => p.Id == product.Id) != null)
                {
                    _products.Find(p => p.Id == product.Id).Count += product.Count;
                }
                else
                {
                    var shopProduct = new ShopProduct(product.Id, product.Name, product.Price, product.Count);
                    _products.Add(shopProduct);
                }
            }
        }

        public void SetProductPrice(int id, float newPrice)
        {
            _products.Find(p => p.Id == id).Price = newPrice;
        }

        public void Buy(Customer customer, List<ListProduct> products)
        {
            foreach (ListProduct product in products)
            {
                CheckProduct(product.Product);
                CheckProductInShop(product.Product.Name);
                var product1 = _products.Find(p => p.Name == product.Product.Name);
                if (product1.Count >= product.Count)
                {
                    _products.Find(p => p.Name == product.Product.Name).Count -= product.Count;
                    customer.Withdraw(product1.Price * product.Count);
                }
            }
        }

        public void CheckProduct(Product product)
        {
            if (_registrateProducts.First(p => p.Id == product.Id) == null)
            {
                throw new ProductIsNotRegisterShopManagerException("Product is not registered!");
            }
        }
    }
}
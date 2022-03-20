using System;
using System.Collections.Generic;
using System.Net;
using Shops.Tools;

namespace Shops
{
    public class ShopManager
    {
        private int _lastShopId = 0;
        private int _lastProductId = 0;
        private List<Shop> _shops = new List<Shop>();
        private List<Product> _products = new List<Product>();
        public IReadOnlyList<Product> Products => _products;

        public Shop CreateShop(string name, string address)
        {
            var shop = new Shop(_lastShopId++, name, address, Products);
            _shops.Add(shop);
            return shop;
        }

        public Product RegisterProduct(string name)
        {
            var product = new Product(_lastProductId++, name);
            _products.Add(product);
            return product;
        }

        public Shop GetShop(int id)
        {
            return _shops.Find(s => s.Id == id);
        }

        public Shop FindBestPrice(List<ListProduct> products)
        {
            float bestPrice = 100000000000;
            float tempPrice = 0;
            Shop bestShop = null;

            foreach (Shop shop in _shops)
            {
                foreach (ListProduct product in products)
                {
                    if (_products.Find(p => p.Name == product.Product.Name) == null) continue;
                    var product1 = shop.FindProduct(product.Product.Name);
                    if (product1 != null)
                    {
                        if (product1.Count >= product.Count)
                        {
                            tempPrice += product1.Price;
                        }
                        else
                        {
                            tempPrice = 0;
                            break;
                        }
                    }
                    else
                    {
                        tempPrice = 0;
                        break;
                    }
                }

                if (tempPrice < bestPrice && tempPrice != 0)
                {
                    bestPrice = tempPrice;
                    bestShop = shop;
                }

                tempPrice = 0;
            }

            return bestShop;
        }
    }
}
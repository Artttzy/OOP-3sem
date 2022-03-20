using System.Collections.Generic;
using System.Runtime.InteropServices;
using NUnit.Framework;
using Shops;
using Shops.Tools;

namespace Shops.Tests
{
    [TestFixture]
    public class ShopManagerTest
    {
        private ShopManager _shopManager;

        [SetUp]
        public void Setup()
        {
            _shopManager = new ShopManager();
        }

        [Test]
        public void Supply_ProductsAreInShop()
        {
            var shop = _shopManager.CreateShop("TestBay", "Bay 18");
            _shopManager.RegisterProduct("tomato");
            _shopManager.RegisterProduct("cucumber");
            _shopManager.GetShop(0).Supply(new List<ShopProduct> {new ShopProduct(0,"tomato", 32, 7), new ShopProduct(1,"cucumber", 50, 20)});
            Assert.AreEqual(7, shop.FindProduct("tomato").Count);
        }

        [Test]
        public void SetProductPrice_PriceHasChanged()
        {
            var shop = _shopManager.CreateShop("TestBay", "Bay 18");
            _shopManager.RegisterProduct("tomato");
            _shopManager.GetShop(0).Supply(new List<ShopProduct> {new ShopProduct(0,"tomato", 32, 7)});
            var oldPrice = shop.FindProduct("tomato").Price;
            var newPrice = 28;
            _shopManager.GetShop(0).SetProductPrice(0, newPrice);
            Assert.AreEqual(newPrice, shop.FindProduct("tomato").Price);
        }

        [Test]
        public void FindCheapestPrice_ShopFound()
        {
            var shop1 = _shopManager.CreateShop("CloudyBay", "Bay 42");
            var shop2 = _shopManager.CreateShop("TastyBay", "Bay 31");
            var shop3 = _shopManager.CreateShop("BlockBay", "Bay 228"); 
            var tomato = _shopManager.RegisterProduct("tomato");
            var cucumber = _shopManager.RegisterProduct("cucumber");
            var beer = _shopManager.RegisterProduct("beer");
            _shopManager.GetShop(0).Supply(new List<ShopProduct> {new ShopProduct(0,"tomato", 20, 7), new ShopProduct(1,"cucumber", 40, 100)});
            _shopManager.GetShop(1).Supply(new List<ShopProduct> {new ShopProduct(0,"tomato", 30, 30), new ShopProduct(1,"cucumber", 50, 30)});
            _shopManager.GetShop(2).Supply(new List<ShopProduct> {new ShopProduct(0,"tomato", 29, 30), new ShopProduct(1,"cucumber", 50, 30)});
            var bestShop = _shopManager.FindBestPrice(new List<ListProduct>
                {new ListProduct(tomato, 23), new ListProduct(cucumber, 15)});
            Assert.AreEqual(bestShop, shop3);
        }

        [Test]
        public void BuyProducts_CustomerBudgetAndProductsCountHaveChanged()
        {
            var shop1 = _shopManager.CreateShop("CloudyBay", "Bay 42");
            var cucumber = _shopManager.RegisterProduct("cucumber");
            var tomato = _shopManager.RegisterProduct("tomato");
            _shopManager.GetShop(0).Supply(new List<ShopProduct> {new ShopProduct(1,"tomato", 20, 7), new ShopProduct(0,"cucumber", 40, 100)});
            var person = new Customer("Artem", 500);
            _shopManager.GetShop(0).Buy(person, new List<ListProduct>{new ListProduct(tomato, 5), new ListProduct(cucumber, 3)});
            Assert.AreEqual(person.Budget, 280);
            Assert.AreEqual(shop1.FindProduct("tomato").Count, 2);
            Assert.AreEqual(shop1.FindProduct("cucumber").Count, 97);
        }
    }
}
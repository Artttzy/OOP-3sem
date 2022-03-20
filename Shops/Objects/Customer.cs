using System;
using Shops.Tools;

namespace Shops
{
    public class Customer
    {
        private string _name;
        private float _budget;

        public Customer(string name, float budget)
        {
            _name = name;
            _budget = budget;
        }

        public string Name => _name;
        public float Budget => _budget;

        public void Withdraw(float sum)
        {
            if (_budget >= sum)
            {
                _budget -= sum;
            }
            else
            {
                throw new LackOfFundsShopManagerException("Lack of funds!");
            }
        }
    }
}
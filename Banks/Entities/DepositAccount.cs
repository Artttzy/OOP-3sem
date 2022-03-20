using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Exceptions;
using Banks.Interfaces;

namespace Banks.Entities
{
    public class DepositAccount : IBankAccount
    {
        private int _id;
        private int _endMonths;
        private double _funds = 0;
        private double _percent;
        private Bank _bank;
        private Client _client;
        private string _type;
        private Dictionary<int, double> _operationsHistory = new Dictionary<int, double>();

        public DepositAccount(int id, double firstSum, double percent, int months)
        {
            _id = id;
            _funds += firstSum;
            _percent = percent;
            _endMonths = months;
            _type = "Депозитный счет";
        }

        public int Id => _id;
        public string Type => _type;
        public Bank Bank
        {
            get => _bank;
            set => _bank = value;
        }

        public Client Client
        {
            get => _client;
            set => _client = value;
        }

        public double Funds
        {
            get => _funds;
            set => _funds = value;
        }

        public double Percent => _percent;
        public int MonthsToEnd
        {
            get => _endMonths;
            set => _endMonths = value;
        }

        public IReadOnlyDictionary<int, double> OperationsHistory
        {
            get => _operationsHistory;
        }

        public void Withdraw(double sum)
        {
            if (_endMonths > 0)
            {
                throw new DepositTermNotOverCentralBankException("Невозможно вывести средства! Срок депозитного счета еще не подошел к концу!");
            }
            else
            {
                if (_funds >= sum)
                {
                    _funds -= sum;
                    _operationsHistory.Add((_operationsHistory.Count * 2) + 1, sum);
                }
                else
                {
                    throw new LackOfFundsCentralBankException("Недостаточно средств!");
                }
            }
        }

        public void Deposit(double sum)
        {
            _funds += sum;
            _operationsHistory.Add(_operationsHistory.Count * 2, sum);
        }

        public void AnnulTransaction(int id)
        {
            var idd = _operationsHistory.Keys.First(o => o == id);
            if (idd % 2 == 0)
            {
                _funds -= _operationsHistory[idd];
                _operationsHistory.Add((_operationsHistory.Count * 2) + 1, _operationsHistory[idd]);
            }
            else
            {
                _funds += _operationsHistory[idd];
                _operationsHistory.Add(_operationsHistory.Count * 2, _operationsHistory[idd]);
            }
        }
    }
}
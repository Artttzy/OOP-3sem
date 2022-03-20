using System.Collections.Generic;
using System.Linq;
using Banks.Interfaces;

namespace Banks.Entities
{
    public class CreditAccount : IBankAccount
    {
        private int _id;
        private double _limit;
        private double _commission;
        private double _funds = 0;
        private Bank _bank;
        private Client _client;
        private string _type;
        private Dictionary<int, double> _operationsHistory = new Dictionary<int, double>();

        public CreditAccount(int id, double limit, double commission)
        {
            _id = id;
            _limit = limit;
            _commission = commission;
            _type = "Кредитный счет";
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

        public IReadOnlyDictionary<int, double> OperationsHistory
        {
            get => _operationsHistory;
        }

        public void Withdraw(double sum)
        {
            if (_funds < _limit)
            {
                _funds -= sum * (1 + _commission);
                _operationsHistory.Add((_operationsHistory.Count * 2) + 1, sum * (1 + _commission));
            }
            else
            {
                _funds -= sum;
                _operationsHistory.Add((_operationsHistory.Count * 2) + 1, sum);
            }
        }

        public void Deposit(double sum)
        {
            if (_funds < _limit)
            {
                _funds += sum * (1 - _commission);
                _operationsHistory.Add(_operationsHistory.Count * 2, sum * (1 - _commission));
            }
            else
            {
                _funds += sum;
                _operationsHistory.Add(_operationsHistory.Count * 2, sum);
            }
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
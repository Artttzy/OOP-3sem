using System.Collections.Generic;
using System.Collections.Specialized;
using Banks.Interfaces;

namespace Banks.Entities
{
    public class Bank
    {
        private int _id;
        private string _name;
        private string _address;
        private double _debitPercent;
        private double _creditCommission;
        private double _suspiciousLimit;
        private Dictionary<double, double> _depositPercentDictionary;
        private List<DepositAccount> _depositAccounts = new List<DepositAccount>();
        private List<DebitAccount> _debitAccounts = new List<DebitAccount>();
        private List<CreditAccount> _creditAccounts = new List<CreditAccount>();
        private List<Client> _clients = new List<Client>();

        public Bank(int id, string name, string address)
        {
            _id = id;
            _name = name;
            _address = address;
        }

        public int Id => _id;
        public string Name => _name;
        public string Address => _address;
        public double DebitPercent => _debitPercent;
        public double CreditComission => _creditCommission;
        public IReadOnlyDictionary<double, double> DepositPercentDictionary => _depositPercentDictionary;
        public double SuspiciousLimit => _suspiciousLimit;
        public void AddClient(Client client)
        {
            _clients.Add(client);
        }

        public void AddDebitAccount(DebitAccount acc)
        {
            _debitAccounts.Add(acc);
        }

        public void AddDepositAccount(DepositAccount acc)
        {
            _depositAccounts.Add(acc);
        }

        public void AddCreditAccount(CreditAccount acc)
        {
            _creditAccounts.Add(acc);
        }

        public void SetDepositConditions(Dictionary<double, double> percents)
        {
            _depositPercentDictionary = percents;
            foreach (Client client in _clients)
            {
                client.Notifications.Add("Deposit conditions has updated!");
            }
        }

        public void SetDebitConditions(double percent)
        {
            _debitPercent = percent;
            foreach (Client client in _clients)
            {
                client.Notifications.Add("Debit conditions has updated!");
            }
        }

        public void SetCreditConditions(double commission)
        {
            _creditCommission = commission;
            foreach (Client client in _clients)
            {
                client.Notifications.Add("Credit conditions has updated!");
            }
        }

        public void SetSuspiciousConditions(double limit)
        {
            _suspiciousLimit = limit;
            foreach (Client client in _clients)
            {
                client.Notifications.Add("Suspicious conditions has updated!");
            }
        }
    }
}
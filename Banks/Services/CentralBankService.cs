using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Entities;
using Banks.Exceptions;
using Banks.Interfaces;

namespace Banks.Services
{
    public class CentralBankService : ICentralBankService
    {
        private int lastid = 0;
        private List<Bank> _banks = new List<Bank>();
        private List<Client> _clients = new List<Client>();
        private List<DebitAccount> _debitAccounts = new List<DebitAccount>();
        private List<DepositAccount> _depositAccounts = new List<DepositAccount>();
        private List<CreditAccount> _creditAccounts = new List<CreditAccount>();
        private List<IBankAccount> _accounts = new List<IBankAccount>();
        public IReadOnlyList<Bank> BanksList => _banks;

        public Bank RegistrateBank(string name, string address)
        {
            var bank = new Bank(lastid++, name, address);
            _banks.Add(bank);
            return bank;
        }

        public void SetDepositConditions(Bank bank, Dictionary<double, double> percents)
        {
            bank.SetDepositConditions(percents);
        }

        public void SetDebitConditions(Bank bank, double percent)
        {
            bank.SetDebitConditions(percent);
        }

        public void SetCreditConditions(Bank bank, double commission)
        {
            bank.SetCreditConditions(commission);
        }

        public void SetSuspiciousConditions(Bank bank, double limit)
        {
            bank.SetSuspiciousConditions(limit);
        }

        public Client RegistrateClient(string name, string surname, string phone)
        {
            var client = new Client(name, surname, phone);
            _clients.Add(client);
            return client;
        }

        public void AddClientAdress(Client client, string address)
        {
            client.AddAddress(address);
        }

        public void AddClientPassport(Client client, string passport)
        {
            client.AddPassport(passport);
        }

        public void OpenDebitAccount(Bank bank, Client client)
        {
            var acc = new DebitAccount(_accounts.Count + 1, bank.DebitPercent);
            _debitAccounts.Add(acc);
            _accounts.Add(acc);
            bank.AddDebitAccount(acc);
            bank.AddClient(client);
            client.Accounts.Add(acc);
            acc.Bank = bank;
            acc.Client = client;
        }

        public void OpenDepositAccount(Bank bank, Client client, double sum, int months)
        {
            var acc = new DepositAccount(_accounts.Count + 1, sum, bank.DepositPercentDictionary.First(d => d.Key > sum).Value, months);
            _depositAccounts.Add(acc);
            _accounts.Add(acc);
            bank.AddDepositAccount(acc);
            bank.AddClient(client);
            client.Accounts.Add(acc);
            acc.Bank = bank;
            acc.Client = client;
        }

        public void OpenCreditAccount(Bank bank, Client client, double limit)
        {
            var acc = new CreditAccount(_accounts.Count + 1, limit, bank.CreditComission);
            _creditAccounts.Add(acc);
            _accounts.Add(acc);
            bank.AddCreditAccount(acc);
            bank.AddClient(client);
            client.Accounts.Add(acc);
            acc.Bank = bank;
            acc.Client = client;
        }

        public void Withdraw(int accid, double sum)
        {
            var acc = _accounts.Find(a => a.Id == accid);
            CheckSuspicion(acc, sum);
            acc.Withdraw(sum);
        }

        public void Deposit(int accid, double sum)
        {
            _accounts.Find(a => a.Id == accid).Deposit(sum);
        }

        public void Transfer(int accid1,  int accid2, double sum)
        {
            var acc1 = _accounts.Find(a => a.Id == accid1);
            CheckSuspicion(acc1, sum);
            acc1.Withdraw(sum);
            var acc2 = _accounts.Find(a => a.Id == accid2);
            acc2.Deposit(sum);
        }

        public IReadOnlyDictionary<int, double> GetOperationsHistory(int accid)
        {
            return _accounts.Find(a => a.Id == accid).OperationsHistory;
        }

        public void AnnulOperation(int accid, int id)
        {
            var acc = _accounts.Find(a => a.Id == accid);
            acc.AnnulTransaction(id);
        }

        public void RewindTime(int months)
        {
            for (int i = 0; i < months; i++)
            {
                foreach (DebitAccount debitAccount in _debitAccounts)
                {
                    debitAccount.Funds += debitAccount.Funds * debitAccount.Percent;
                }

                foreach (DepositAccount depositAccount in _depositAccounts)
                {
                    depositAccount.Funds += depositAccount.Funds * depositAccount.Percent;
                    depositAccount.MonthsToEnd--;
                }
            }
        }

        public void CheckSuspicion(IBankAccount acc, double sum)
        {
            if (acc.Client.CheckSuspicion() == true)
            {
                if (acc.Bank.SuspiciousLimit < sum)
                {
                    throw new SuspiciousLimitCentralBankException("Аккаунт подозрительный и сумма вывода выше лимита!");
                }
            }
        }

        public IReadOnlyList<string> GetNotifications(Client client)
        {
            return client.Notifications;
        }

        public Client FindClient(string phone)
        {
            var client = _clients.Find(c => c.Phone == phone);
            if (client != null)
            {
                return client;
            }
            else
            {
                throw new ClientNotFoundCentralBankException("Клиент не найден!");
            }
        }

        public Bank FindBank(string name)
        {
            var bank = _banks.Find(b => b.Name == name);
            if (bank != null)
            {
                return bank;
            }
            else
            {
                throw new BankNotFoundCentralBankException("Банк не найден!");
            }
        }
    }
}
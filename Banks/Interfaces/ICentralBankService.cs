using System.Collections.Generic;
using Banks.Entities;

namespace Banks.Interfaces
{
    public interface ICentralBankService
    {
        IReadOnlyList<Bank> BanksList { get; }
        Bank RegistrateBank(string name, string address);
        void SetDebitConditions(Bank bank, double percent);
        void SetDepositConditions(Bank bank, Dictionary<double, double> percents);
        void SetCreditConditions(Bank bank, double commission);
        Client RegistrateClient(string name, string surname, string phone);
        void AddClientAdress(Client client, string address);
        void AddClientPassport(Client client, string passport);
        void OpenDebitAccount(Bank bank, Client client);
        void OpenDepositAccount(Bank bank, Client client, double sum, int months);
        void OpenCreditAccount(Bank bank, Client client, double limit);
        void Withdraw(int accid, double sum);
        void Deposit(int accid, double sum);
        void Transfer(int accid1, int accid2, double sum);
        void RewindTime(int months);
        void AnnulOperation(int accid, int id);
        IReadOnlyDictionary<int, double> GetOperationsHistory(int accid);
        void SetSuspiciousConditions(Bank bank, double limit);
        IReadOnlyList<string> GetNotifications(Client client);
        Client FindClient(string phone);
        Bank FindBank(string name);
    }
}
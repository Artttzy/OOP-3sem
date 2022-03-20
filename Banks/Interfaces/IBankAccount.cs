using System.Collections.Generic;
using Banks.Entities;

namespace Banks.Interfaces
{
    public interface IBankAccount
    {
        double Funds { get; }

        Bank Bank { get; }

        Client Client { get; }

        int Id { get; }

        string Type { get; }
        IReadOnlyDictionary<int, double> OperationsHistory { get; }

        void Withdraw(double sum);
        void Deposit(double sum);
        void AnnulTransaction(int id);
    }
}
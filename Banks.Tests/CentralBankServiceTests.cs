using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Exceptions;
using Banks.Interfaces;
using Banks.Services;
using Microsoft.VisualBasic;
using NUnit.Framework;

namespace Banks.Tests
{
    [TestFixture]
        
    public class CentralBankServiceTests
    {
        private ICentralBankService _centralBankService;

        [SetUp]
        public void Setup()
        {
            _centralBankService = new CentralBankService();
            _centralBankService.RegistrateBank("Тинькофф", "Гетто 66");
            _centralBankService.SetDebitConditions(_centralBankService.FindBank("Тинькофф"), 0.05);
            _centralBankService.SetCreditConditions(_centralBankService.FindBank("Тинькофф"), 0.1);
            var depositConditions = new Dictionary<double, double>();
            depositConditions.Add(50000, 0.03);
            depositConditions.Add(100000, 0.05);
            depositConditions.Add(200000, 0.07);
            _centralBankService.SetDepositConditions(_centralBankService.FindBank("Тинькофф"), depositConditions);
            _centralBankService.SetSuspiciousConditions(_centralBankService.FindBank("Тинькофф"), 100000);
            _centralBankService.RegistrateClient("Артем", "Васильев", "88005553535");
            _centralBankService.OpenDebitAccount(_centralBankService.FindBank("Тинькофф"), _centralBankService.FindClient("88005553535"));
            _centralBankService.OpenDepositAccount(_centralBankService.FindBank("Тинькофф"), _centralBankService.FindClient("88005553535"), 70000, 6);
            _centralBankService.OpenCreditAccount(_centralBankService.FindBank("Тинькофф"), _centralBankService.FindClient("88005553535"), 50000);
        }

        [Test]
        public void WithdrawFundsWithLackOfFundsAndWithDepositTermNotOver_CatchExceptions()
        {
            Assert.Catch<CentralBankException>(() =>
            {
                _centralBankService.Deposit(_centralBankService.FindClient("88005553535").Accounts[0].Id, 50000);
                _centralBankService.Withdraw(_centralBankService.FindClient("88005553535").Accounts.First().Id, 80000);
            });
            Assert.Catch<CentralBankException>(() =>
            {
                _centralBankService.Withdraw(_centralBankService.FindClient("88005553535").Accounts[1].Id, 50000);
            });
        }

        [Test]
        public void SetBankConditions_GetNotification()
        {
            _centralBankService.SetDebitConditions(_centralBankService.FindBank("Тинькофф"), 0.07);
            Assert.AreEqual(_centralBankService.GetNotifications(_centralBankService.FindClient("88005553535")).Last(), "Debit conditions has updated!");
        }

        [Test]
        public void WithdrawOnSuspiciousAccMoreThanLimitAllows_CatchException()
        {
            _centralBankService.Deposit(_centralBankService.FindClient("88005553535").Accounts[0].Id, 200000);
            Assert.Catch<CentralBankException>(() =>
            {
                _centralBankService.Withdraw(_centralBankService.FindClient("88005553535").Accounts[0].Id, 150000);
            });
           _centralBankService.AddClientAdress(_centralBankService.FindClient("88005553535"), "Гетто 5");
           _centralBankService.AddClientPassport(_centralBankService.FindClient("88005553535"), "4444444444");
           _centralBankService.Withdraw(_centralBankService.FindClient("88005553535").Accounts[0].Id, 150000);
           Assert.AreEqual(_centralBankService.FindClient("88005553535").Accounts[0].Funds, 50000);
        }

        [Test]
        public void RewindTime_DepositAndDebitFundsWillRaiseAndDepositAccAllowsToWithdraw()
        {
            _centralBankService.Deposit(_centralBankService.FindClient("88005553535").Accounts[0].Id, 50000);
            _centralBankService.RewindTime(10);
            Assert.AreEqual(Convert.ToInt32(_centralBankService.FindClient("88005553535").Accounts[0].Funds), 81445);
            _centralBankService.Withdraw(_centralBankService.FindClient("88005553535").Accounts[1].Id, 50000);
        }
    }
}
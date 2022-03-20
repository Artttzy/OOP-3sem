using System;
using System.Collections.Generic;
using Banks.Entities;
using Banks.Interfaces;
using Banks.Services;

namespace Banks
{
    internal static class ConsoleApplication
    {
        private static ICentralBankService _centralBank;

        public static void RegistrateBank()
        {
            Console.Write("Введите название банка.");
            string name = Console.ReadLine();
            Console.Write("Введите адрес банка.");
            string address = Console.ReadLine();
            _centralBank.RegistrateBank(name, address);
        }

        public static void SetBankConditions()
        {
            Console.Write("Введите название банка, в котором вы бы хотели изменить условия.");
            string bankName = Console.ReadLine();
            var bank = _centralBank.FindBank(bankName);
            Console.Write("Вы хотите изменить дебетовые условия?");
            string ans = Console.ReadLine();
            if (ans == "Да")
            {
                Console.Write("Введите процент на остаток.");
                double percent = Convert.ToDouble(Console.ReadLine()) / 100;
                _centralBank.SetDebitConditions(bank, percent);
            }

            Console.Write("Вы хотите изменить депозитные условия?");
            ans = Console.ReadLine();
            if (ans == "Да")
            {
                Console.Write("Вводите начальные суммы и процент, начисляемый на счет для суммы. Чтобы закончить, введите два 0.");
                double sum;
                double percent;
                Dictionary<double, double> depositConditions = new Dictionary<double, double>();
                sum = Convert.ToDouble(Console.ReadLine());
                percent = Convert.ToDouble(Console.ReadLine());
                while (percent != 0 && sum != 0)
                {
                    depositConditions.Add(sum, percent / 100);
                    sum = Convert.ToDouble(Console.ReadLine());
                    percent = Convert.ToDouble(Console.ReadLine());
                }

                _centralBank.SetDepositConditions(bank, depositConditions);
            }

            Console.Write("Вы хотите изменить кредитные условия?");
            ans = Console.ReadLine();
            if (ans == "Да")
            {
                Console.Write("Введите кредитную комиссию.");
                double comission = Convert.ToDouble(Console.ReadLine()) / 100;
                _centralBank.SetDebitConditions(bank, comission);
            }
        }

        public static void RegistrateClient()
        {
            Console.Write("Введите имя клиента.");
            string name = Console.ReadLine();
            Console.Write("Введите фамилию клиента.");
            string surname = Console.ReadLine();
            Console.Write("Введите номер телефона.");
            string phone = Console.ReadLine();
            _centralBank.RegistrateClient(name, surname, phone);
        }

        public static void AddClientInformation()
        {
            Console.Write("Введите номер телефона.");
            string phone = Console.ReadLine();
            var client = _centralBank.FindClient(phone);
            if (client == null) throw new Exception("Пользователь не найден!");
            Console.Write("Хотите добавить адрес клиента?");
            string ans = Console.ReadLine();
            if (ans == "Да")
            {
                Console.Write("Введите адрес клиента.");
                string address = Console.ReadLine();
                _centralBank.AddClientAdress(client, address);
            }

            Console.Write("Хотите добавить паспорт клиента?");
            ans = Console.ReadLine();
            if (ans == "Да")
            {
                Console.Write("Введите паспорт клиента.");
                string passport = Console.ReadLine();
                _centralBank.AddClientPassport(client, passport);
            }
        }

        public static void GetBanksList()
        {
            foreach (Bank bank in _centralBank.BanksList)
            {
                Console.WriteLine(bank.Id + " " + bank.Name + " " + bank.Address);
            }
        }

        public static void GetClientsAccounts()
        {
            Console.Write("Введите номер телефона.");
            string phone = Console.ReadLine();
            var client = _centralBank.FindClient(phone);
            if (client == null) throw new Exception("Пользователь не найден!");
            foreach (IBankAccount acc in client.Accounts)
            {
                Console.WriteLine("Id счета: " + acc.Id + " Тип счета: " + acc.Type + " Банк счета: " + acc.Bank +
                                  " Состояние счета: " + acc.Funds);
            }
        }

        public static void OpenDebitAccount()
        {
            Console.Write("Введите номер телефона.");
            string phone = Console.ReadLine();
            var client = _centralBank.FindClient(phone);
            Console.Write("Введите название банка, в котором вы бы хотели открыть счет.");
            string bankName = Console.ReadLine();
            var bank = _centralBank.FindBank(bankName);
            _centralBank.OpenDebitAccount(bank, client);
        }

        public static void OpenDepositAccount()
        {
            Console.Write("Введите номер телефона.");
            string phone = Console.ReadLine();
            var client = _centralBank.FindClient(phone);
            Console.Write("Введите название банка, в котором вы бы хотели открыть счет.");
            string bankName = Console.ReadLine();
            var bank = _centralBank.FindBank(bankName);
            Console.Write("Укажите начальную сумму, с которой вы бы хотели открыть счет.");
            double sum = Convert.ToDouble(Console.ReadLine());
            Console.Write("Укажите срок, на который вы хотите открыть счет, в месяцах.");
            int months = Convert.ToInt32(Console.ReadLine());
            _centralBank.OpenDepositAccount(bank, client, sum, months);
        }

        public static void OpenCreditAccount()
        {
            Console.Write("Введите номер телефона.");
            string phone = Console.ReadLine();
            var client = _centralBank.FindClient(phone);
            Console.Write("Введите название банка, в котором вы бы хотели открыть счет.");
            string bankName = Console.ReadLine();
            var bank = _centralBank.FindBank(bankName);
            Console.Write("Укажите лимит, с которым вы бы хотели открыть счет.");
            double limit = Convert.ToDouble(Console.ReadLine());
            _centralBank.OpenCreditAccount(bank, client, limit);
        }

        public static void WithdrawFunds()
        {
            Console.Write("Введите id счета, с которого вы бы хотели вывести средства.");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите сумму, которую вы бы хотели вывести.");
            double sum = Convert.ToDouble(Console.ReadLine());
            _centralBank.Withdraw(id, sum);
        }

        public static void DepositFunds()
        {
            Console.Write("Введите id счета, на который вы бы хотели внести средства.");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите сумму, которую вы бы хотели внести.");
            double sum = Convert.ToDouble(Console.ReadLine());
            _centralBank.Deposit(id, sum);
        }

        public static void TransferFunds()
        {
            Console.Write("Введите id счета, с которого вы бы хотели перевести средства.");
            int id1 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите id счета, на который вы бы хотели перевести средства.");
            int id2 = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите сумму, которую вы бы хотели перевести.");
            double sum = Convert.ToDouble(Console.ReadLine());
            _centralBank.Transfer(id1, id2, sum);
        }

        public static void GetOperationsHistory()
        {
            Console.Write("Введите id счета, историю операций которого вы бы хотели узнать.");
            int id = Convert.ToInt32(Console.ReadLine());
            var operationsHistory = _centralBank.GetOperationsHistory(id);
            foreach (int opId in operationsHistory.Keys)
            {
                string type;
                if (opId % 2 == 0)
                {
                    type = "Пополнение";
                }
                else
                {
                    type = "Снятие";
                }

                Console.WriteLine("Id операции: " + opId + " Вид операции: " + type + " Сумма: " + operationsHistory[opId]);
            }
        }

        public static void AnnulOperation()
        {
            Console.Write("Введите id счета, на котором нужно отменить операцию.");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите id операции, которую вы хотите отменить.");
            int opId = Convert.ToInt32(Console.ReadLine());
            _centralBank.AnnulOperation(id, opId);
        }

        public static void RewindTime()
        {
            Console.Write("Введите количество месяцев.");
            int months = Convert.ToInt32(Console.ReadLine());
            _centralBank.RewindTime(months);
        }

        public static void GetNotifications()
        {
            Console.Write("Введите номер телефона.");
            string phone = Console.ReadLine();
            var client = _centralBank.FindClient(phone);
            IReadOnlyList<string> notifications = _centralBank.GetNotifications(client);
            for (int i = 1; i <= notifications.Count; i++)
            {
                Console.Write(i + " " + notifications[i - 1]);
            }
        }

        private static void Main()
        {
            _centralBank = new CentralBankService();
            int select = 0;
            Console.Write(
                "Выберите опцию: 1 - Зарегистрировать банк; 2 - Установить условия счетов в банке; 3 - Зарегистрировать клиента; 4 - Добавить информацию по клиенту; " +
                "5 - Посмотреть список банков; 6 - Посмотреть список счетов клиента; 7 - Открыть дебетовый счет; " +
                "8 - Открыть депозитный счет; 9 - Открыть кредитный счет; 10 - Вывести средства; 11 - Внести средства; " +
                "12 - Перевести средства; 13 - Получить историю операций по счету; 14 - Отменить операцию; " +
                "15 - Перемотать время; 16 - Посмотреть уведомления пользователя; 17 - Выход");
            while (select != 17)
            {
                select = Convert.ToInt32(Console.ReadLine());
                switch (select)
                {
                    case 1:
                        RegistrateBank();
                        break;
                    case 2:
                        SetBankConditions();
                        break;
                    case 3:
                        RegistrateClient();
                        break;
                    case 4:
                        AddClientInformation();
                        break;
                    case 5:
                        GetBanksList();
                        break;
                    case 6:
                        GetClientsAccounts();
                        break;
                    case 7:
                        OpenDebitAccount();
                        break;
                    case 8:
                        OpenDepositAccount();
                        break;
                    case 9:
                        OpenCreditAccount();
                        break;
                    case 10:
                        WithdrawFunds();
                        break;
                    case 11:
                        DepositFunds();
                        break;
                    case 12:
                        TransferFunds();
                        break;
                    case 13:
                        GetOperationsHistory();
                        break;
                    case 14:
                        AnnulOperation();
                        break;
                    case 15:
                        RewindTime();
                        break;
                    case 16:
                        GetNotifications();
                        break;
                    case 17:
                        break;
                }
            }
        }
    }
}
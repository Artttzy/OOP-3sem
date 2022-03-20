using System.Collections.Generic;
using Banks.Exceptions;
using Banks.Interfaces;

namespace Banks.Entities
{
    public class Client
    {
        private string _name;
        private string _surname;
        private string _address;
        private string _passport;
        private string _phone;
        private List<IBankAccount> _accounts = new List<IBankAccount>();
        private List<string> _notifications = new List<string>();

        public Client(string name, string surname, string phone)
        {
            _name = name;
            _surname = surname;
            _phone = phone;
        }

        public List<IBankAccount> Accounts => _accounts;
        public string Passport => _passport;
        public string Address => _address;
        public string Phone => _phone;

        public List<string> Notifications => _notifications;

        public void AddAddress(string address)
        {
            _address = address;
        }

        public void AddPassport(string passport)
        {
            if (passport.Length != 10)
            {
                throw new InvalidPassportCentralBankException("Серия и номер паспорта должны вводится 10ю цифрами без пробелов!");
            }

            _passport = passport;
        }

        public void AddAccount(IBankAccount account)
        {
            _accounts.Add(account);
        }

        public void AddNotification(string notification)
        {
            _notifications.Add(notification);
        }

        public bool CheckSuspicion()
        {
            if (_address == null || _passport == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
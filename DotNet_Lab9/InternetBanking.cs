using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//-	See the balance of your credit card
//-	Pay for mobile services, public services (ком. услуги), etc.
//-	Transfer the data from your card to another
//-	See the archive of your payments
//-	See the list of ATMs in your city, and search by street name


namespace DotNet_Lab9
{
    class InternetBanking
    {
        List<ATM> atms;
        public Client client;
        public InternetBanking()
        {
            atms = new List<ATM> 
            {
                new ATM {address = "Zheltoksan, 90"}, 
                new ATM {address = "Kazybek bi, 8"}, 
                new ATM {address = "Abai, 24"}, 
                new ATM {address = "Abai, 89"}, 
                new ATM {address = "Satpaev, 90"}, 
                new ATM {address = "Kazybek bi, 67"}
            };
            client = new Client("Ivan", "Ivanov", "87014563215");
        }
        public string FindAtm(string street)
        {
            string result = default(string);
            foreach (var atm in atms)
            {
                if (atm.address.Contains(street))
                {
                    result += atm.address;
                }
            }
            return result;
        }
    }

    class CreditCard
    {
        public int balance;
        public int Number { get; set; }
        public DateTime ExpDate { get; set; }

        Random rnd = new Random();

        public CreditCard()
        {
            balance = rnd.Next(25000, 800000);
            Number = rnd.Next(365478, 2456891);
            ExpDate = new DateTime();
            ExpDate = DateTime.Today.AddYears(5);
        }
    }

    class ATM
    {
        public string address;
    }

    class Service
    {
        public String CommBill(ref int balance)
        {
            // Paying for Communal bill
            balance -= 20000;
            return "Paid for Communal Bill, new balance" + balance;
        }
        public String Mortgage(ref int balance)
        {
            // Paying for Mortgage
            balance -= 150000;
            return "Paid for Morgage, new balance = " + balance;
        }
        public String Mobile(ref int balance)
        {
            // Paying for Mobile 
            balance -= 1000;
            return "Paid for Mobile, new balance = " + balance;
        }
    }

    class Client
    {
        string name;
        string surname;
        string phone;
        CreditCard cCard;
        Service serv;

        List<String> servicesPaid = new List<String>();

        public Client(string name, string surname, string phone)
        {
            this.name = name;
            this.surname = surname;
            this.phone = phone;
            cCard = new CreditCard();
            serv = new Service();
        }

        public int CheckBalance()
        {
            return cCard.balance;
        }

        public void PayForCommBill()
        {
            servicesPaid.Add(serv.CommBill(ref cCard.balance));
        }

        public void PayForMortgage()
        {
            servicesPaid.Add(serv.Mortgage(ref cCard.balance));
        }

        public void PayForMobile()
        {
            servicesPaid.Add(serv.Mobile(ref cCard.balance));
        }
        public List<String> PaymentArchive()
        {
            return servicesPaid;
        }
    }
}

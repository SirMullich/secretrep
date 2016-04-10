using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_Lab9_ClassLibrary
{
    public class InternetBanking
    {
        List<ATM> atms;
        public List<Client> client;
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
            client = new List<Client>();
        }
        public string FindAtm(string street)
        {
            string result = default(string);
            foreach (var atm in atms)
            {
                if (atm.address.Contains(street))
                {
                    result = result + " \n\r" + atm.address;
                }
            }
            return result;
        }
        public void Transer(Client c1, Client c2, int amount)
        {
            c1.cCard.balance = c1.cCard.balance - amount;
            c2.cCard.balance = c2.cCard.balance + amount;
        }
    }

    public class CreditCard
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

    public class ATM
    {
        public string address;
    }

    public class Service
    {
        public int CommBill(int balance, int amount)
        {
            // Paying for Communal bill
            return balance - amount;
        }
        public int Mortgage(int balance, int amount)
        {
            // Paying for Mortgage
            return balance - amount;
        }
        public int Mobile(int balance, int amount)
        {
            // Paying for Mobile 
            return balance - amount;
        }
    }

    public class Client
    {
        public string name;
        public string surname;
        public string phone;
        public CreditCard cCard;
        public Dictionary<string, int> ratio;
        public List<String> servicesPaid;
        Service serv;

        public Client()
        {

            cCard = new CreditCard();
            serv = new Service();
            servicesPaid = new List<String>();
            ratio = new Dictionary<string, int>();
            ratio["CommBill"] = 0;
            ratio["Mortgage"] = 0;
            ratio["Mobile"] = 0;

        }

        public int CheckBalance()
        {
            return cCard.balance;
        }

        public void PayForCommBill(int amount)
        {
            cCard.balance = serv.CommBill(cCard.balance, amount);
            servicesPaid.Add(String.Format("Payed for CommBill {0}", amount));
            ratio["CommBill"] = ratio["CommBill"] + amount;
        }

        public void PayForMortgage(int amount)
        {
            cCard.balance = serv.Mortgage(cCard.balance, amount);
            servicesPaid.Add(String.Format("Payed for Mortgage {0}", amount));
            ratio["Mortgage"] = ratio["Mortgage"] + amount;
        }

        public void PayForMobile(int amount)
        {
            cCard.balance = serv.Mobile(cCard.balance, amount);
            servicesPaid.Add(String.Format("Payed for Mobile {0}", amount));
            ratio["Mobile"] = ratio["Mobile"] + amount;
        }
        public List<String> PaymentArchive()
        {
            return servicesPaid;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashMachine
{
    public class Repository
    {
        
        //public Repository(ApplicationContext con)
        //{
        //    context = con;
        //}

        public Account Balance()
        {
            using (ApplicationContext context = new ApplicationContext())
            {

                if (!context.Accounts.Any())
                {
                    Account account = new Account
                    {
                        Amount = 20000000
                    };

                    context.Add(account);
                    context.SaveChanges();
                }

                var balance = context.Accounts.First();
                return balance;
            }
        }

        public List<Money> GetMoney(int summ)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var balance = context.Accounts.First().Amount;
                context.Accounts.First().Amount = balance - summ;
                context.SaveChanges();
                List<Money> banknotes = new List<Money>();
               
                int banknote5000 = 0;
                int banknote1000 = 0;
                int banknote200 = 0;
                int banknote100 = 0;
                
                banknote5000 = summ / 5000;
                summ = summ % 5000;

                if (banknote5000 != 0)
                {
                    banknotes.Add( new Money { Value = 5000, Quantity = banknote5000 });
                }

                    
                banknote1000 = summ / 1000;
                summ = summ % 1000; 

                if (banknote1000 != 0)
                {
                    banknotes.Add(new Money { Value = 1000, Quantity = banknote1000 });
                }

                banknote200 = summ / 200;
                summ = summ % 200;

                if (banknote200 != 0)
                {
                    banknotes.Add(new Money { Value = 200, Quantity = banknote200 });
                }

                banknote100 = summ / 100;
                if (banknote100 != 0)
                {
                    banknotes.Add(new Money { Value = 100, Quantity = banknote100 });
                }
                
                return banknotes;

            }
        
        }

        public void AccountRefill(int changeBalance)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Accounts.First().Amount += changeBalance ;
                context.SaveChanges();
            }


        }

    }
}

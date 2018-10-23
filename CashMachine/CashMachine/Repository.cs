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

        public int Balance()
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

                var balance = context.Accounts.First().Amount;
                return balance;
            }
        }

        public Dictionary<int, int> GetMoney(int summ)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var balance = context.Accounts.First().Amount;
                context.Accounts.First().Amount = balance - summ;
                context.SaveChanges();
                Dictionary<int, int> banknotes = new Dictionary<int, int>();
               
                int banknote5000 = 0;
                int banknote1000 = 0;
                int banknote200 = 0;
                int banknote100 = 0;
                
                banknote5000 = summ / 5000;
                summ = summ % 5000;

                if (banknote5000 != 0)
                {
                    banknotes.Add(5000, banknote5000);
                }

                    
                banknote1000 = summ / 1000;
                summ = summ % 1000; 

                if (banknote1000 != 0)
                {
                    banknotes.Add(1000, banknote1000);
                }

                banknote200 = summ / 200;
                summ = summ % 200;

                if (banknote200 != 0)
                {
                    banknotes.Add(200, banknote200);
                }

                banknote100 = summ / 100;
                if (banknote100 != 0)
                {
                    banknotes.Add(100, banknote100);
                }

                return banknotes;

            }
                   

                 
                    
          
            

        }

    }
}

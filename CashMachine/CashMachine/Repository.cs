using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashMachine
{
    public class Repository:IRepository
    {
        public List<Money> GetBancnotesInfo()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var bancnotesInfo = context.Money.
                    OrderByDescending (n=>n.Value).ToList();
                return bancnotesInfo;
            }

        }    

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

        public List<Money> GetMoney(int getCash)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                List<Money> banknotes = GetBancnotesInfo();
                List<Money> userBanknotes = new List<Money>();

                if (banknotes.Any())
                {
                    int reminder = getCash;
                    foreach (var nominal in banknotes)
                    {
                        var quantity = 0;

                        if (nominal.Quantity != 0 && reminder != 0)
                        {
                            quantity = Math.Min(nominal.Quantity, reminder / nominal.Value);
                            reminder -= quantity * nominal.Value;
                            nominal.Quantity -= quantity;
                        }
                        if (quantity != 0)
                        {
                            userBanknotes.Add(new Money { Value = nominal.Value, Quantity = quantity });
                        }
                    }
                    if (reminder == 0)
                    {
                        foreach (var nominal in banknotes)
                        {
                            context.Money.FirstOrDefault(x => x.Id == nominal.Id).Quantity = nominal.Quantity; 
                        }
                        context.Accounts.First().Amount -= getCash;
                        context.SaveChanges();
                        return userBanknotes;
                    }
                    else return null;
                    
                    
                }
                else return null;
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

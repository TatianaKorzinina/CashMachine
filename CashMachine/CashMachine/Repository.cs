﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashMachine
{
    public class Repository:IRepository
    {   
        // получение информации о состоянии ячеек в банкомате: купюры в наличии и их количество
        public IEnumerable<Money> GetBancnotesInfo()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var bancnotesInfo = context.Money.
                OrderByDescending(n => n.Value).ToList();
                return bancnotesInfo;                
            }
        } 
   
        public Account GetBalance()
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                
                var balance = context.Accounts.First();
                return balance;
            }
        }

        public IEnumerable<Money> GetMoney(int getCash)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                var banknotes = GetBancnotesInfo();
                var userBanknotes = new List<Money>();
                // если в банкомате есть хоть одна банкнота, необходимая для снятия сумма набирается из купюр,
                //имеющихся в банкомате
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
                    // если необходимая сумма набралась, то сумма на счете уменьшается на снятую сумму и количество банкнот в банкомате 
                    //так же уменьшается на количество, необходимое для выдачи
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

        public void AccountRefill(int refillBalance)
        {
            using (ApplicationContext context = new ApplicationContext())
            {
                context.Accounts.First().Amount += refillBalance ;
                context.SaveChanges();
            }
        }
    }
}

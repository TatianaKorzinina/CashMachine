using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashMachine
{
    public interface IRepository
    {
        List<Money> GetBancnotesInfo();
        Account Balance();
        List<Money> GetMoney(int getCash);
        void AccountRefill(int changeBalance);

    }
}

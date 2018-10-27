using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashMachine
{
    public interface IRepository
    {
        IEnumerable<Money> GetBancnotesInfo();
        Account GetBalance();
        IEnumerable<Money> GetMoney(int getCash);
        void AccountRefill(int changeBalance);

    }
}

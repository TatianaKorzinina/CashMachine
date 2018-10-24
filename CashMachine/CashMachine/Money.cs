using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CashMachine
{
    public class Money
    {   
        public int Id { get; set; }
        public int Value { get; set; }
        public int Quantity { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashMachine
{
    public class ApplicationContext: DbContext
    {
        //public ApplicationContext(DbContextOptions<ApplicationContext> options) 
        //: base (options)
        //{
           
        //}


        public DbSet<Account> Accounts { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=Tanianotebook\\sqlexpress;Database=CashMachine;Trusted_Connection=True;");
        }


    }
}

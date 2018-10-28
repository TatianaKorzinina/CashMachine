using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CashMachine
{
    public class ApplicationContext: DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Money> Money { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=Tanianotebook\\sqlexpress;Database=CashMachine;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    AccountId = 1,
                    Amount= 80000,
                }
            );

            modelBuilder.Entity<Money>().HasData(
                new Money { Id = 1, Value = 200, Quantity = 50 },
                new Money { Id = 2, Value = 1000, Quantity = 50 },
                new Money { Id = 3, Value = 100, Quantity = 100 },
                new Money { Id = 4, Value = 5000, Quantity = 25 }
            );
        }
    }
}

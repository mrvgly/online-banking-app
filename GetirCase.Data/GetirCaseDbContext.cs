using GetirCase.Core.Models;
using GetirCase.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace GetirCase.Data
{
    public class GetirCaseDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public GetirCaseDbContext(DbContextOptions<GetirCaseDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new CustomerConfiguration());

            builder
                .ApplyConfiguration(new AccountConfiguration());

            builder
                .ApplyConfiguration(new TransactionConfiguration());
        }
    }
}

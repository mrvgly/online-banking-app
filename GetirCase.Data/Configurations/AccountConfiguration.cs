using GetirCase.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GetirCase.Data.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    { 

        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder
                .HasKey(a => a.Id);

            builder
                .Property(a => a.Id)
                .UseIdentityColumn();

            builder
                .HasOne(a => a.Customer)
                .WithMany()
                .HasForeignKey("CustomerId");

            builder
                .ToTable("Accounts");
        }
    }
}

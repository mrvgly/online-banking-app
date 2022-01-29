using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetirCase.Core.Models;
using GetirCase.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GetirCase.Data.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(GetirCaseDbContext context)
             : base(context)
        { }

        public async Task<List<Account>> GetAllAccountsWithCustomerInfoAsync()
        {
            return await GetirCaseDbContext.Accounts
                                           .Include(m => m.Customer)
                                           .ToListAsync();
        }

        public async Task<Account> GetAccountWithCustomerInfoByIdAsync(int id)
        {
            return await GetirCaseDbContext.Accounts
                                           .Include(m => m.Customer)
                                           .SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<Account>> GetAllAccountsByCustomerIdAsync(int customerId)
        {
            return await GetirCaseDbContext.Accounts
                                           .Include(m => m.Customer)
                                           .Where(m => m.Customer.Id == customerId)
                                           .ToListAsync();
        }

        private GetirCaseDbContext GetirCaseDbContext
        {
            get { return Context as GetirCaseDbContext; }
        }
    }
}

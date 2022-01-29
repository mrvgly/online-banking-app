using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GetirCase.Core.Models;
using GetirCase.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GetirCase.Data.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(GetirCaseDbContext context)
             : base(context)
        { }

        public async Task<List<Transaction>> GetAllTransactionsByAccountIdAsync(int accountId)
        {
            return await GetirCaseDbContext.Transactions
                                           .Include(t => t.Account)
                                           .ThenInclude(a => a.Customer)
                                           .Where(t => t.Account.Id == accountId)
                                           .ToListAsync();
        }

        public async Task<List<Transaction>> GetAllTransactionsByCustomerIdWithPeriodsAsync(int customerId, DateTime startDate, DateTime endDate)
        {
            return await GetirCaseDbContext.Transactions
                                           .Include(t => t.Account)
                                           .ThenInclude(a => a.Customer)
                                           .Where(t => t.Account.Customer.Id == customerId && t.Date >= startDate && t.Date <= endDate)
                                           .ToListAsync();
        }

        private GetirCaseDbContext GetirCaseDbContext
        {
            get { return Context as GetirCaseDbContext; }
        }
    }
}

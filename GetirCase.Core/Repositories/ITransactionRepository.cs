using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GetirCase.Core.Models;

namespace GetirCase.Core.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        Task<List<Transaction>> GetAllTransactionsByAccountIdAsync(int accountId);
        Task<List<Transaction>> GetAllTransactionsByCustomerIdWithPeriodsAsync(int customerId, DateTime startDate, DateTime endDate);
    }
}

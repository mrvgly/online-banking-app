using System.Collections.Generic;
using System.Threading.Tasks;
using GetirCase.Core.Models;

namespace GetirCase.Core.Services
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllTransactionsByAccountId(int accountId);
        Task<List<Transaction>> GetAllTransactionsByCustomerIdWithPeriods(int customerId, string startDate, string EndDate);
        Task<Transaction> MakeWithdrawal(Transaction transaction);
        Task<Transaction> MakeDeposit(Transaction transaction);
    }
}

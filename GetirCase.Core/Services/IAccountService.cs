using GetirCase.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GetirCase.Core.Services
{
    public interface IAccountService
    {
        Task<List<Account>> GetAccountsByCustomerId(int customerId);
        Task<Account> CreateAccount(Account account);
        Task<Account> UpdateAccountBalance(Account account, decimal amount);
        Task<Account> GetAccountDetailById(int id);
    }
}

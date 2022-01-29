using System.Collections.Generic;
using System.Threading.Tasks;
using GetirCase.Core.Models;

namespace GetirCase.Core.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<List<Account>> GetAllAccountsWithCustomerInfoAsync();
        Task<Account> GetAccountWithCustomerInfoByIdAsync(int id);
        Task<List<Account>> GetAllAccountsByCustomerIdAsync(int customerId);
    }
}

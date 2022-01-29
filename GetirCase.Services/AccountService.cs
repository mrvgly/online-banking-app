using System.Collections.Generic;
using System.Threading.Tasks;
using GetirCase.Core;
using GetirCase.Core.Models;
using GetirCase.Core.Services;

namespace GetirCase.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
           _unitOfWork = unitOfWork;
        }

        public async Task<Account> CreateAccount(Account account)
        {
            var customer = await _unitOfWork.Customers.GetByIdAsync(account.CustomerId);

            account.Customer = customer;

            await _unitOfWork.Accounts.AddAsync(account);

            await _unitOfWork.CommitAsync();

            return account;
        }

        public async Task<List<Account>> GetAccountsByCustomerId(int customerId)
        {
            return await _unitOfWork.Accounts
                                    .GetAllAccountsByCustomerIdAsync(customerId);
        }

        public async Task<Account> GetAccountDetailById(int id)
        {
            return await _unitOfWork.Accounts
                                    .GetAccountWithCustomerInfoByIdAsync(id);
        }

        public async Task<Account> UpdateAccountBalance(Account account, decimal amount)
        {
            account.Balance += amount;

            await _unitOfWork.CommitAsync();

            return account;
        }
    }
}

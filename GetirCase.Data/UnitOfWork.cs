using System.Threading.Tasks;
using GetirCase.Core;
using GetirCase.Core.Repositories;
using GetirCase.Data.Repositories;

namespace GetirCase.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GetirCaseDbContext _context;

        private CustomerRepository _customerRepository;

        private AccountRepository _accountRepository;

        private TransactionRepository _transactionRepository;

        public UnitOfWork(GetirCaseDbContext context)
        {
            _context = context;
        }

        public ICustomerRepository Customers => _customerRepository ??= new CustomerRepository(_context);

        public IAccountRepository Accounts => _accountRepository ??= new AccountRepository(_context);

        public ITransactionRepository Transaction => _transactionRepository ??= new TransactionRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

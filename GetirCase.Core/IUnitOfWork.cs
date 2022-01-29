using System.Threading.Tasks;
using GetirCase.Core.Repositories;

namespace GetirCase.Core
{
    public interface IUnitOfWork
    {
        IAccountRepository Accounts { get; }
        ICustomerRepository Customers { get; }
        ITransactionRepository Transaction { get; }
        Task<int> CommitAsync();
    }
}

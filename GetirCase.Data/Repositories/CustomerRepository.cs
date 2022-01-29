using System.Threading.Tasks;
using GetirCase.Core.Models;
using GetirCase.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GetirCase.Data.Repositories
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(GetirCaseDbContext context)
            : base(context)
        { }

        public async Task<Customer> GetCustomerByLoginRequestAsync(Login login)
        {
            return await GetirCaseDbContext.Customers
                                     .SingleOrDefaultAsync(a => a.Email == login.Email && a.Password == login.Password);
        }

        public async Task<Customer> GetCustomerByNameAsync(string customerName)
        {
            return await GetirCaseDbContext.Customers
                                                .SingleOrDefaultAsync(a => a.Name == customerName);
        }

        private GetirCaseDbContext GetirCaseDbContext
        {
            get { return Context as GetirCaseDbContext; }
        }
    }
}

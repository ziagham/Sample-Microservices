using System;
using System.Threading;
using System.Threading.Tasks;
using CustomerApi.Data.Database;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate;
using Microsoft.EntityFrameworkCore;

namespace CustomerApi.Data.Repository.v1
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CustomerContext customerContext) : base(customerContext)
        {
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await CustomerContext.Customer.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return await CustomerContext.Customer.CountAsync(e => e.Email.ToLower() == email.ToLower()) <= 0;
        }
    }
}
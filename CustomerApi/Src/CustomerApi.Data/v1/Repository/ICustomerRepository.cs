using System;
using System.Threading;
using System.Threading.Tasks;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate;

namespace CustomerApi.Data.v1.Repository
{
    public interface ICustomerRepository: IRepository<Customer>
    {
        Task<Customer> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<bool> IsEmailUniqueAsync(string email);
    }
}
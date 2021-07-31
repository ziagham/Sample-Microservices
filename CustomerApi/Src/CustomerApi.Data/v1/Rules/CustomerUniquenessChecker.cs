using System.Threading.Tasks;
using CustomerApi.Data.v1.Repository;
using CustomerApi.Domain.AggregatesModel.CustomerAggregate.Rules;

namespace CustomerApi.Data.v1.Rules
{
    public class CustomerUniquenessChecker : ICustomerUniquenessChecker
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerUniquenessChecker(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> IsUnique(string customerEmail)
        {
            return await _customerRepository.IsEmailUniqueAsync(customerEmail);
        }
    }
}
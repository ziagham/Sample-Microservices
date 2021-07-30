using System.Threading.Tasks;
using CustomerApi.Domain.SeekWork;

namespace CustomerApi.Domain.AggregatesModel.CustomerAggregate.Rules
{
    public interface ICustomerUniquenessChecker
    {
        Task<bool> IsUnique(string customerEmail);
    }
}

using System.Threading.Tasks;
using CustomerApi.Domain.SeekWork;

namespace CustomerApi.Domain.AggregatesModel.CustomerAggregate.Rules
{
    public interface ICustomerUniquenessChecker
    {
        bool IsUnique(string customerEmail);
    }
}

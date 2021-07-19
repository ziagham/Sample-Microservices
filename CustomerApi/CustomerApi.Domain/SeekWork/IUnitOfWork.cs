using System.Threading;
using System.Threading.Tasks;

namespace CustomerApi.Domain.SeekWork
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
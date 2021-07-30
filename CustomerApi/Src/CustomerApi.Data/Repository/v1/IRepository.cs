using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerApi.Domain.SeekWork;

namespace CustomerApi.Data.Repository.v1
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        IEnumerable<TEntity> GetAll();
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
    }
}
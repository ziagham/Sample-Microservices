using System.Collections.Generic;
using System.Threading.Tasks;
using OrderApi.Domain.SeekWork;

namespace OrderApi.Data.Repository.v1
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        IEnumerable<TEntity> GetAll();

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task UpdateRangeAsync(List<TEntity> entities);
    }
}
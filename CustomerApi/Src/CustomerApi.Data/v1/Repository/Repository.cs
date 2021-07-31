using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerApi.Data.v1.Database;
using CustomerApi.Domain.SeekWork;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CustomerApi.Data.v1.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly CustomerContext CustomerContext;
        private readonly DbSet<TEntity> _entities;

        public Repository(CustomerContext customerContext)
        {
            CustomerContext = customerContext;
            _entities = customerContext.Set<TEntity>();
        }

        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                return _entities.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                await _entities.AddAsync(entity);
                await CustomerContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(UpdateAsync)} entity must not be null");
            }

            try
            {
                _entities.Update(entity);
                await CustomerContext.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated {ex.Message}");
            }
        }
    }
}
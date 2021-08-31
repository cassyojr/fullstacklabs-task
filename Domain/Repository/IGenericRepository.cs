using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IGenericRepository<TEntity> : IDisposable where TEntity : class
    {
        Task<TEntity> GetByIdAsync(object id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> AddAsync(TEntity obj);
        Task<TEntity> UpdateAsync(TEntity obj);
    }
}

using Domain.Repository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationContext _context;
        protected readonly DbSet<TEntity> DbSet;

        protected GenericRepository(ApplicationContext context)
        {
            _context = context;
            DbSet = _context.Set<TEntity>();
        }

        public virtual async Task<TEntity> GetByIdAsync(object id) => await DbSet.FindAsync(id);

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await DbSet.ToListAsync();

        public virtual async Task<TEntity> UpdateAsync(TEntity obj)
        {
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return obj;
        }

        public virtual async Task<TEntity> AddAsync(TEntity obj)
        {
            var entryEntity = await DbSet.AddAsync(obj);
            await _context.SaveChangesAsync();
            return entryEntity.Entity;
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

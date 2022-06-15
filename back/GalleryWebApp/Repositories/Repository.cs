using GalleryWebApp.Data;
using GalleryWebApp.Repositories.Contracts;
using GalleryWebApp.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GalleryWebApp.Repositories
{
    public class Repository<T> : IRepository<T>, IPaginatedRepository<T> where T : class
    {
        protected DataContext _dataContext;
        protected DbSet<T> _dbSet;

        public Repository(DataContext dataContext)
        {
            _dataContext = dataContext;
            _dbSet = _dataContext.Set<T>();
        }

        public async Task CreateAsync(T item)
        {
            await _dbSet.AddAsync(item);
            await _dataContext.SaveChangesAsync();
        }

        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<PaginatedResult<T>> GetPaginatedListFromQueryAsync(IQueryable<T> query, int page, int pageSize)
        {
            if (page <= 0)
            {
                page = 1;
            }
            if (pageSize <= 0)
            {
                pageSize = 10;
            }

            var skip = (page - 1) * pageSize;
            var items = await query.Skip(skip).Take(pageSize).ToListAsync();

            var totalResults = await query.LongCountAsync();
            var totalPages = (int)Math.Ceiling((decimal)totalResults / pageSize);

            return new PaginatedResult<T>(items, page, pageSize, totalPages, totalResults);
        }

        public async Task RemoveAsync(T item)
        {
            EntityEntry<T> entry = null;
            try
            {
                entry = _dbSet.Remove(item);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                if (entry != null)
                {
                    entry.State = EntityState.Detached;
                }
                throw e;
            }
        }

        public async Task UpdateAsync(T item)
        {
            _dataContext.Entry(item).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GalleryWebApp.Repositories.Contracts
{
    public interface IRepository<T> where T : class
    {
        Task CreateAsync(T item);
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task RemoveAsync(T item);
        Task UpdateAsync(T item);
    }
}

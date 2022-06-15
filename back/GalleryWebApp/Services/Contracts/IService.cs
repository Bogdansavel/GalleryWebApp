using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GalleryWebApp.Services.Contracts
{
    public interface IService<Model, DTO> where Model : class 
                                          where DTO : class
    {
        Task CreateAsync(DTO item);
        Task<DTO> GetByIdAsync(Guid id);
        Task<IEnumerable<DTO>> GetAllAsync();
        Task<IEnumerable<DTO>> GetAsync(Expression<Func<Model, bool>> predicate);
        Task RemoveAsync(Guid itemId);
        Task UpdateAsync(DTO item);
    }
}

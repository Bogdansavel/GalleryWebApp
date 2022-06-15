using GalleryWebApp.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalleryWebApp.Repositories.Contracts
{
    public interface IPaginatedRepository<T> where T : class
    {
        public Task<PaginatedResult<T>> GetPaginatedListFromQueryAsync(IQueryable<T> query, int page, int pageSize);
    }
}

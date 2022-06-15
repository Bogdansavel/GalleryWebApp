using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GalleryWebApp.Models;

namespace GalleryWebApp.Repositories.Contracts
{
    public interface ICommentRepository : IRepository<Comment>, IPaginatedRepository<Comment>
    {
        public Task<IEnumerable<Comment>> GetAllByPaintingIdAsync(Guid paintingId);
    }
}

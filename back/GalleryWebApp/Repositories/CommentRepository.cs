using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalleryWebApp.Data;
using GalleryWebApp.Models;
using GalleryWebApp.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GalleryWebApp.Repositories
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(DataContext dataContext) : base(dataContext)
        {
        }

        public async Task<IEnumerable<Comment>> GetAllByPaintingIdAsync(Guid paintingId)
        {
            return await _dbSet.Include(c => c.Painting).Include(c => c.User).AsNoTracking().Where(c => Equals(c.PaintingId, paintingId)).ToListAsync();
        }
    }
}

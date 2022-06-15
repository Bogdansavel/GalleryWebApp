using GalleryWebApp.Data;
using GalleryWebApp.Models;
using GalleryWebApp.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GalleryWebApp.Repositories
{
    public class PaintingRepository : Repository<Painting>, IPaintingRepository
    {
        public PaintingRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public override async Task<IEnumerable<Painting>> GetAsync(Expression<Func<Painting, bool>> predicate)
        {
            return await _dbSet.Include(p => p.Artist).AsNoTracking().Where(predicate).ToListAsync();
        }

        public override async Task<IEnumerable<Painting>> GetAllAsync()
        {
            return await _dbSet.Include(p => p.Artist).AsNoTracking().ToListAsync();
        }

        public override async Task<Painting> GetByIdAsync(Guid Id)
        {
            return await _dbSet.Include(p => p.Artist).AsNoTracking().Where(p => string.Equals(p.Id, Id)).FirstOrDefaultAsync();
        }
    }
}

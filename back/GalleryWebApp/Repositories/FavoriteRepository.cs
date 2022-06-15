using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using GalleryWebApp.Data;
using GalleryWebApp.Models;
using GalleryWebApp.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GalleryWebApp.Repositories
{
    public class FavoriteRepository : Repository<Favorite>, IFavoriteRepository
    {

        public FavoriteRepository(DataContext dataContext) : base(dataContext)
        {

        }

        public async Task<IEnumerable<Favorite>> GetAllByUserIdAsync(string userId)
        {
            return await _dbSet.Where(f => f.UserId == userId)
                .Include(f => f.Painting).ThenInclude(p => p.Artist)
                .Include(f => f.User)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> CheckIfPaintingIsFavorite(string userId, Guid paintingId)
        {
            return await _dbSet.Where(f => f.UserId == userId && f.PaintingId.Equals(paintingId))
                .AsNoTracking()
                .AnyAsync();
        }

        public async Task<int> GetCountOfFavorites(string userId)
        {
            var group = await _dataContext.Users
                .Join(_dbSet, u => u.Id, f => f.UserId, (u, f) => new { UserId = u.Id, FavoriteId = f.Id})
                .Where(f => string.Equals(f.UserId, userId))
                .GroupBy(f => f.UserId)
                .Select(g => new {
                    UserId = g.Key,
                    Count = g.Count()
                }).FirstOrDefaultAsync();
            return group == null ? 0 : group.Count;
        }
    }
}

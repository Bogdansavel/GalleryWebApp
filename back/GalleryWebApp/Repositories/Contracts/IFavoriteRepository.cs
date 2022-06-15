using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GalleryWebApp.Models;

namespace GalleryWebApp.Repositories.Contracts
{
    public interface IFavoriteRepository : IRepository<Favorite>, IPaginatedRepository<Favorite>
    {
        Task<IEnumerable<Favorite>> GetAllByUserIdAsync(string userId);
        Task<bool> CheckIfPaintingIsFavorite(string userId, Guid paintingId);
        Task<int> GetCountOfFavorites(string userId);
    }
}

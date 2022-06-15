using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GalleryWebApp.Models;
using GalleryWebApp.Models.DTOs;

namespace GalleryWebApp.Services.Contracts
{
    public interface IFavoriteService : IService<Favorite, FavoriteDTO>
    {
        Task<IEnumerable<FavoriteDTO>> GetAllByUserIdAsync(string userId);
        Task<bool> CheckIfPaintingIsFavorite(string userId, Guid paintingId);
        Task<int> GetCountOfFavorites(string userId);
    }
}

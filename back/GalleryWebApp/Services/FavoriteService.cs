using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using GalleryWebApp.Exceptions;
using GalleryWebApp.Models;
using GalleryWebApp.Models.DTOs;
using GalleryWebApp.Repositories.Contracts;
using GalleryWebApp.Services.Contracts;

namespace GalleryWebApp.Services
{
    public class FavoriteService : Service<Favorite, FavoriteDTO>, IFavoriteService
    {
        private readonly IUserService _userService;
        private readonly IPaintingService _paintingService;
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoriteService(IFavoriteRepository repository, IMapper mapper, IUserService userService,
            IPaintingService paintingService) : base(repository, mapper) 
        {
            _userService = userService;
            _paintingService = paintingService;
            _favoriteRepository = repository;
        }

        public override async Task CreateAsync(FavoriteDTO favoriteDTO)
        {
            var sameFavorite = await _repository.GetAsync(f => f.PaintingId.Equals(favoriteDTO.PaintingId) &&
            f.UserId.Equals(favoriteDTO.UserId));
            if (sameFavorite.Any())
            {
                throw new RestException(HttpStatusCode.BadRequest, new { ErrorMessage = "This painting is already on this user's favorites list" });
            }

            await _favoriteRepository.CreateAsync(_mapper.Map<FavoriteDTO, Favorite>(favoriteDTO));
        }

        public async Task<IEnumerable<FavoriteDTO>> GetAllByUserIdAsync(string userId)
        {
            var user = await _userService.GetByIdAsync(userId);
            return (await _favoriteRepository.GetAllByUserIdAsync(userId)).Select(m => _mapper.Map<Favorite, FavoriteDTO>(m));
        }

        public async Task<bool> CheckIfPaintingIsFavorite(string userId, Guid paintingId)
        {
            await _userService.GetByIdAsync(userId);
            await _paintingService.GetByIdAsync(paintingId);
            return await _favoriteRepository.CheckIfPaintingIsFavorite(userId, paintingId);
        }

        public async Task<int> GetCountOfFavorites(string userId)
        {
            await _userService.GetByIdAsync(userId);
            return await _favoriteRepository.GetCountOfFavorites(userId);
        }
    }
}

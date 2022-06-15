using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalleryWebApp.Models.DTOs;
using GalleryWebApp.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GalleryWebApp.Controllers
{
    [Route("favorites")]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(IFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

        [Route("{userId}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FavoriteDTO>>> GetAllFavoritesForUserAsync(string userId)
        {
            return Ok((await _favoriteService.GetAllByUserIdAsync(userId)).Select(f => new FavoriteDTO
            {
                Id = f.Id,
                UserId = f.UserId,
                PaintingDTO = new PaintingDTO
                {
                    Id = f.PaintingDTO.Id,
                    Name = f.PaintingDTO.Name,
                    ArtistId = f.PaintingDTO.ArtistId,
                    Artist = f.PaintingDTO.Artist,
                    Year = f.PaintingDTO.Year,
                    ImageName = f.PaintingDTO.ImageName,
                    ImageSrc = string.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, f.PaintingDTO.ImageName)
                }
            }));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> CreateFavoriteAsync([FromBody] FavoriteDTO favoriteDTO)
        {
            await _favoriteService.CreateAsync(favoriteDTO);
            return Ok();
        }

        [Route("{userId}/{paintingId:Guid}")]
        [HttpGet]
        public async Task<ActionResult<FavoriteDTO>> GetFavorite(string userId, Guid paintingId)
        {
            var favorite = (await _favoriteService.GetAsync(f => f.UserId == userId && f.PaintingId.Equals(paintingId))).FirstOrDefault();
            if (favorite == null)
            {
                return NotFound();
            }
            return Ok(favorite);
        }

        [Authorize]
        [Route("{id:Guid}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteByIdAsync(Guid id)
        {
            await _favoriteService.RemoveAsync(id);
            return Ok();
        }

        [Route("count/{userId}")]
        [HttpGet]
        public async Task<ActionResult<int>> GetCountOfFavoritesAsync(string userId)
        {
            return await _favoriteService.GetCountOfFavorites(userId);
        }
    }
}

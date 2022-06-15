using GalleryWebApp.Models.DTOs;
using GalleryWebApp.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalleryWebApp.Controllers
{
    [Route("artists")]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistDTO>>> GetAllArtistsAsync()
        {
            return Ok(await _artistService.GetAllAsync());
        }

        [Route("create")]

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] ArtistDTO artistDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _artistService.CreateAsync(artistDTO);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [Route("delete/{artistId:Guid}")]
        [HttpPost]
        public async Task<ActionResult> DeleteAsync(Guid artistId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _artistService.RemoveAsync(artistId);
            return Ok();
        }
    }
}

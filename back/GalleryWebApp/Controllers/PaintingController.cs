using GalleryWebApp.Models.DTOs;
using GalleryWebApp.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GalleryWebApp.Controllers
{
    [Route("paintings")]
    public class PaintingController : ControllerBase
    {
        private readonly IPaintingService _paintingService;

        public PaintingController(IPaintingService paintingService)
        {
            _paintingService = paintingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaintingDTO>>> GetAllPaintingsAsync()
        {
            return Ok((await _paintingService.GetAllAsync()).Select(dto => new PaintingDTO 
            {
                Id = dto.Id,
                Name = dto.Name,
                ArtistId = dto.ArtistId,
                Artist = dto.Artist,
                Year = dto.Year,
                ImageName = dto.ImageName,
                ImageSrc = string.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, dto.ImageName)
            }));
        }

        [Route("create")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromForm] PaintingDTO paintingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _paintingService.CreateAsync(paintingDTO);
            return Ok();
        }

        [Route("update")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> UpdateAsync([FromForm] PaintingDTO paintingDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _paintingService.UpdateAsync(paintingDTO);
            return Ok();
        }

        [Route("delete/{paintingId:Guid}")]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> DeleteAsync(Guid paintingId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _paintingService.RemoveAsync(paintingId);
            return Ok();
        }

        [Route("{id:Guid}")]
        [HttpGet]
        public async Task<ActionResult> GetById(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dto = await _paintingService.GetByIdAsync(id);

            return Ok
            (
                new PaintingDTO
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    ArtistId = dto.ArtistId,
                    Artist = dto.Artist,
                    Year = dto.Year,
                    ImageName = dto.ImageName,
                    ImageSrc = string.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, dto.ImageName)
                }
            );
        }

        [Route("{search}")]
        [HttpGet]
        public async Task<ActionResult> Get(string search)
        {
            return Ok
            (
                (await _paintingService.GetAsync(p => p.Name.ToUpper().Contains(search.ToUpper()) || p.Artist.Name.ToUpper().Contains(search.ToUpper())))
                .Select(dto => new PaintingDTO
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    ArtistId = dto.ArtistId,
                    Artist = dto.Artist,
                    Year = dto.Year,
                    ImageName = dto.ImageName,
                    ImageSrc = string.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, dto.ImageName)
                })
            );
        }
    }
}

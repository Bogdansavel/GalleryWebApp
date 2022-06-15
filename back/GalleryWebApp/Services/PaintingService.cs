using AutoMapper;
using GalleryWebApp.Exceptions;
using GalleryWebApp.Models;
using GalleryWebApp.Models.DTOs;
using GalleryWebApp.Repositories;
using GalleryWebApp.Repositories.Contracts;
using GalleryWebApp.Services.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GalleryWebApp.Services
{
    public class PaintingService : Service<Painting, PaintingDTO>, IPaintingService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private const string ImageFilesFolderName = "Images";

        public PaintingService(IPaintingRepository repository, IMapper mapper,
            IWebHostEnvironment webHostEnvironment) : base(repository, mapper)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public override async Task CreateAsync(PaintingDTO paintingDTO)
        {
            var samePainting = await _repository.GetAsync(p => p.Name.Equals(paintingDTO.Name));
            if (samePainting.Any())
            {
                throw new RestException(HttpStatusCode.BadRequest, new { ErrorMessage = "Painting with such name already exists" });
            }

            paintingDTO.ImageName = await SaveImage(paintingDTO.Image, paintingDTO.ImageName);

            await _repository.CreateAsync(_mapper.Map<PaintingDTO, Painting>(paintingDTO));
        }

        public override async Task UpdateAsync(PaintingDTO paintingDTO)
        {
            var samePainting = await _repository.GetAsync(p => p.Name.Equals(paintingDTO.Name) && !p.Id.Equals(paintingDTO.Id));
            if (samePainting.Any())
            {
                throw new RestException(HttpStatusCode.BadRequest, new { ErrorMessage = "Painting with such name already exists" });
            }

            if (paintingDTO.Image != null)
            {
                paintingDTO.ImageName = await SaveImage(paintingDTO.Image, paintingDTO.ImageName);
            }

            await _repository.UpdateAsync(_mapper.Map<PaintingDTO, Painting>(paintingDTO));
        }

        public override async Task RemoveAsync(Guid itemId)
        {
            var painting = await _repository.GetByIdAsync(itemId);
            if (painting is null)
            {
                throw new RestException(HttpStatusCode.BadRequest, new { Id = "Painting with such id doesn't exist" });
            }

            await _repository.RemoveAsync(painting);
            await DeleteImage(painting.ImageName);
        }

        private async Task<string> SaveImage(IFormFile imageFile, string imageName)
        {
            var newImageName = (imageName.Replace(Path.GetExtension(imageName), string.Empty)
                                    + DateTime.Now.ToString("yymmssff")).Replace(" ", "-")
                                    + Path.GetExtension(imageName);
            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, ImageFilesFolderName, newImageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return newImageName;
        }

        private Task DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, ImageFilesFolderName, imageName);
            if (File.Exists(imagePath))
            {
                return Task.Factory.StartNew(() => File.Delete(imagePath));
            }
            return Task.FromResult(0);
        }
    }
}

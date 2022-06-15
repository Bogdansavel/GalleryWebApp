using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalleryWebApp.Models.DTOs
{
    public class PaintingDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ArtistId { get; set; }
        public ArtistDTO Artist { get; set; }
        public int Year { get; set; }

        public string ImageName { get; set; }
        public string ImageSrc { get; set; }

        public IFormFile Image { get; set; }
    }
}

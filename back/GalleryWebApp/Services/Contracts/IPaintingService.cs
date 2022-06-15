using GalleryWebApp.Models;
using GalleryWebApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalleryWebApp.Services.Contracts
{
    public interface IPaintingService : IService<Painting, PaintingDTO>
    {
    }
}

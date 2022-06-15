using GalleryWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalleryWebApp.Repositories.Contracts
{
    public interface IArtistRepository : IRepository<Artist>, IPaginatedRepository<Artist>
    {
    }
}

using GalleryWebApp.Data;
using GalleryWebApp.Models;
using GalleryWebApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalleryWebApp.Repositories
{
    public class ArtistRepository : Repository<Artist>, IArtistRepository
    {
        public ArtistRepository(DataContext dataContext) : base(dataContext)
        {

        }
    }
}

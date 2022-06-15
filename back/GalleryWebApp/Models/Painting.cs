using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalleryWebApp.Models
{
    public class Painting
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ArtistId { get; set; }
        public Artist Artist { get; set; }
        public int Year { get; set; }
        public string ImageName { get; set; }
    }
}

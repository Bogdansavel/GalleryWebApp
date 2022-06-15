using System;
namespace GalleryWebApp.Models
{
    public class Favorite
    {
        public Guid Id { get; set; }
        public Guid PaintingId { get; set; }
        public Painting Painting { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}

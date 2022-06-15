using System;
namespace GalleryWebApp.Models.DTOs
{
    public class FavoriteDTO
    {
        public Guid Id { get; set; }
        public Guid PaintingId { get; set; }
        public PaintingDTO PaintingDTO { get; set; }
        public string UserId { get; set; }
    }
}

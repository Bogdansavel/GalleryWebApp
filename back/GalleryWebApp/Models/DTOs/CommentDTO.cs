using System;
namespace GalleryWebApp.Models.DTOs
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public Painting Painting { get; set; }
        public Guid PaintingId { get; set; }
    }
}

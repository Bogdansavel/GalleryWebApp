using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalleryWebApp.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public Painting Painting { get; set; }
        public Guid PaintingId { get; set; }
        public Guid UserId1 { get; set; }
    }
}

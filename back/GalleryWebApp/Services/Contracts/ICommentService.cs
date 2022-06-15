using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GalleryWebApp.Models;
using GalleryWebApp.Models.DTOs;

namespace GalleryWebApp.Services.Contracts
{
    public interface ICommentService : IService<Comment, CommentDTO>
    {
        public Task<IEnumerable<Comment>> GetAllByPaintingIdAsync(Guid paintingId);
    }
}

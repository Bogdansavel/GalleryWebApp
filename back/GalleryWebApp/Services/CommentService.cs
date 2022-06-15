using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using GalleryWebApp.Models;
using GalleryWebApp.Models.DTOs;
using GalleryWebApp.Repositories.Contracts;
using GalleryWebApp.Services.Contracts;
using Microsoft.AspNetCore.Hosting;

namespace GalleryWebApp.Services
{
    public class CommentService : Service<Comment, CommentDTO>, ICommentService
    {


        public CommentService(ICommentRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }

        public async Task<IEnumerable<Comment>> GetAllByPaintingIdAsync(Guid paintingId)
        {
            return await ((ICommentRepository)_repository).GetAllByPaintingIdAsync(paintingId);
        }
    }
}

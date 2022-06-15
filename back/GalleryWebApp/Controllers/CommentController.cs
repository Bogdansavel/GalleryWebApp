using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalleryWebApp.Models;
using GalleryWebApp.Models.DTOs;
using GalleryWebApp.Repositories.Contracts;
using GalleryWebApp.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GalleryWebApp.Controllers
{
    [Route("comments")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IPaintingService _paintingService;
        private readonly UserManager<User> _userManager;

        public CommentController(ICommentService commentService, UserManager<User> userManager,
            IPaintingService paintingService)
        {
            _commentService = commentService;
            _userManager = userManager;
            _paintingService = paintingService;
        }

        [Route("{paintingId:Guid}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetAllByPaintingId(Guid paintingId)
        {
            var comments = await _commentService.GetAllByPaintingIdAsync(paintingId);
            return Ok(comments);
        }

        [Authorize]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> AddComment([FromBody] CommentDTO commentDTO)
        {
            await _commentService.CreateAsync(commentDTO);
            return Ok();
        }

        [Authorize]
        [Route("{commentId:Guid}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteComment(Guid commentId)
        {
            await _commentService.RemoveAsync(commentId);
            return Ok();
        }
    }
}

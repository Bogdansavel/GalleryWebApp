using GalleryWebApp.Exceptions;
using GalleryWebApp.Models;
using GalleryWebApp.Models.DTOs;
using GalleryWebApp.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GalleryWebApp.Controllers
{
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> LoginAsync([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(await _userService.LoginAsync(loginDTO));
        }

        [HttpPost("registration")]
        public async Task<ActionResult<UserDTO>> RegistrationAsync([FromBody] RegistrationDTO registrationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return await _userService.RegistrationAsync(registrationDTO);
        }

        [Route("user/{userId}")]
        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetAsync(string userId)
        {
            return await _userService.GetByIdAsync(userId);
        }

        [Route("users")]
        [HttpGet]
        public async Task<IEnumerable<UserDTO>> GetAll()
        {
            return await _userService.GetAll();
        }

        [Route("users/{userId}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(string userId)
        {
            await _userService.DeleteAsync(userId);
            return Ok();
        }
    }
}

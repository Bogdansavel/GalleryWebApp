using GalleryWebApp.Models;
using GalleryWebApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GalleryWebApp.Services.Contracts
{
    public interface IUserService
    {
        Task<UserDTO> LoginAsync(LoginDTO request);
        Task<UserDTO> RegistrationAsync(RegistrationDTO registrationDTO);
        Task<UserDTO> GetByIdAsync(string userId);
        Task<IEnumerable<UserDTO>> GetAll();
        Task DeleteAsync(string userId);
    }
}

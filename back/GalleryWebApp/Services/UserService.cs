using GalleryWebApp.Data.JWT.Contracts;
using GalleryWebApp.Exceptions;
using GalleryWebApp.Models;
using GalleryWebApp.Models.DTOs;
using GalleryWebApp.Repositories.Contracts;
using GalleryWebApp.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace GalleryWebApp.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IJwtGenerator _jwtGenerator;

		public UserService(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager, IJwtGenerator jwtGenerator)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
			_jwtGenerator = jwtGenerator;
		}

		public async Task<UserDTO> LoginAsync(LoginDTO loginDTO)
		{
			var user = await _userManager.FindByEmailAsync(loginDTO.Email);
			if (user == null)
			{
				throw new RestException(HttpStatusCode.Unauthorized, new { Email = "There is no user with such login" });
			}

			var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);

			if (result.Succeeded)
			{
				var userRoles = await _userManager.GetRolesAsync(user);

				return new UserDTO
				{
					Token = _jwtGenerator.CreateToken(user, userRoles),
					Id = user.Id,
					UserName = user.UserName,
					Email = user.Email,
					Roles = userRoles
				};
			}

			throw new RestException(HttpStatusCode.Unauthorized, new { Password = "Wrong password" });
		}

		public async Task<UserDTO> RegistrationAsync(RegistrationDTO registrationDTO)
        {
			if ((await _userManager.FindByEmailAsync(registrationDTO.Email)) != null)
			{
				throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email already exists" });
			}

			if ((await _userManager.FindByNameAsync(registrationDTO.UserName)) != null)
			{
				throw new RestException(HttpStatusCode.BadRequest, new { UserName = "UserName already exists" });
			}

			var user = new User { 
				UserName = registrationDTO.UserName,
				Email = registrationDTO.Email,
			};

			var result = await _userManager.CreateAsync(user, registrationDTO.Password);

			if (result.Succeeded)
            {
				user = await _userManager.FindByEmailAsync(registrationDTO.Email);

				foreach (var roleName in registrationDTO.Roles)
                {
					var role = await _roleManager.FindByNameAsync(roleName);

					if (role == null)
                    {
						throw new RestException(HttpStatusCode.BadRequest, new { UserName = string.Format("There is no {0} role ", roleName) });
					}

					var roleResult = await _userManager.AddToRoleAsync(user, roleName);
					if (!roleResult.Succeeded)
                    {
						throw new Exception("User creation failed due to role");
					}
                }

				var userRoles = await _userManager.GetRolesAsync(user);

				return new UserDTO
				{
					Token = _jwtGenerator.CreateToken(user, userRoles),
					Id = user.Id,
					UserName = user.UserName,
					Email = user.Email,
					Roles = userRoles
				};
			} else
            {
				throw new Exception("User creation failed");
			}
        }

		public async Task<UserDTO> GetByIdAsync(string userId)
		{
			var user = await CheckIfUserExistsByIdAsync(userId);
			var roles = await _userManager.GetRolesAsync(user);
			return new UserDTO
			{
				Id = user.Id,
				UserName = user.UserName,
				Email = user.Email,
				Roles = roles
			};
		}

		public async Task<IEnumerable<UserDTO>> GetAll()
        {
			var users = _userManager.Users.ToList();
			IList<UserDTO> userDTOs = new List<UserDTO>();
			foreach(var user in users)
            {
				var roles = await _userManager.GetRolesAsync(user);
				userDTOs.Add(new UserDTO()
					{
						Id = user.Id,
						UserName = user.UserName,
						Email = user.Email,
						Roles = roles
					}
				);
            }
			return userDTOs;
        }

		public async Task DeleteAsync(string userId)
        {
			var user = await CheckIfUserExistsByIdAsync(userId);
			await _userManager.DeleteAsync(user);
        }

		private async Task<User> CheckIfUserExistsByIdAsync(string id)
        {
			var user = await _userManager.FindByIdAsync(id);
			if (user == null)
			{
				throw new RestException(HttpStatusCode.BadRequest, new { UserName = "User with such id doesn't exist" });
			}
			return user;
		}
	}
}

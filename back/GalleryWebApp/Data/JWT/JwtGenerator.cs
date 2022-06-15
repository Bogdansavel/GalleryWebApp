using GalleryWebApp.Data.JWT.Contracts;
using GalleryWebApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GalleryWebApp.Data.JWT
{
    public class JwtGenerator : IJwtGenerator
    {
		private readonly SymmetricSecurityKey _key;
		private readonly AuthOptions _authOptions;

		public JwtGenerator(IConfiguration config)
		{
			_authOptions = config.GetSection("AuthOptions").Get<AuthOptions>();
			_key = _authOptions.GetSymmetricSecurityKey();
		}

		public string CreateToken(User user, IList<string> roles)
		{
			var claims = new List<Claim> { new Claim(JwtRegisteredClaimNames.NameId, user.UserName) };
			foreach (var role in roles)
			{
				claims.Add(new Claim(ClaimTypes.Role, role));
			}

			var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256);

			var jwt = new JwtSecurityToken(
					issuer: _authOptions.ValidIssuer,
					audience: _authOptions.ValidAudience,
					notBefore: DateTime.UtcNow,
					claims: claims,
					expires: DateTime.UtcNow.Add(TimeSpan.FromDays(_authOptions.Lifetime)),
					signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(jwt);
		}
	}
}

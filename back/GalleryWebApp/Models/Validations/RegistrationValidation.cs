using FluentValidation;
using GalleryWebApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalleryWebApp.Models.Validations
{
    public class RegistrationValidation : AbstractValidator<RegistrationDTO>
    {
		public RegistrationValidation()
		{
			RuleFor(x => x.UserName).NotEmpty();
			RuleFor(x => x.Email).NotEmpty().EmailAddress();
			RuleFor(x => x.Password).NotEmpty()
				.MinimumLength(6).WithMessage("Password must be at least 6 characters")
				.Matches("[A-Z]").WithMessage("Password must contain 1 uppercase letter")
				.Matches("[0-9]").WithMessage("Password must contain a number")
				.Matches("[^a-zA-Z0-9]").WithMessage("Password must contain non alphanumeric");
		}
	}
}

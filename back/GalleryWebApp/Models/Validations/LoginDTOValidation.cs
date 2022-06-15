using FluentValidation;
using GalleryWebApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalleryWebApp.Models.Validations
{
    public class LoginDTOValidation : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidation()
	    {
		    RuleFor(x => x.Email).EmailAddress().NotEmpty();
		    RuleFor(x => x.Password).NotEmpty();
	    }
    }
}

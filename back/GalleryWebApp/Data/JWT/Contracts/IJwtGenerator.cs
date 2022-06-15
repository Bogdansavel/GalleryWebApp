using GalleryWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalleryWebApp.Data.JWT.Contracts
{
    public interface IJwtGenerator
    {
        string CreateToken(User user, IList<string> roles);
    }
}

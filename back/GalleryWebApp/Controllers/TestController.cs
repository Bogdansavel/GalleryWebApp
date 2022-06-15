using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalleryWebApp.Controllers
{
    [Route("test")]
    [Authorize]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string Test()
        {
            return "succeed get";
        }
    }
}

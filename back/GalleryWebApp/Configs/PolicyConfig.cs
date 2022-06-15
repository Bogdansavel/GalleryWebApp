using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalleryWebApp.Configs
{
    public class PolicyConfig
    {
        public string Name { get; set; }
        public string[] Origins { get; set; }
        public string[] Methods { get; set; }
        public string[] Headers { get; set; }
        public string[] ExposedHeaders { get; set; }
        public bool AllowCredentials { get; set; }
    }
}

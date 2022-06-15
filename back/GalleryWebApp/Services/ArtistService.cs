using AutoMapper;
using GalleryWebApp.Exceptions;
using GalleryWebApp.Models;
using GalleryWebApp.Models.DTOs;
using GalleryWebApp.Repositories;
using GalleryWebApp.Repositories.Contracts;
using GalleryWebApp.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GalleryWebApp.Services
{
    public class ArtistService : Service<Artist, ArtistDTO>, IArtistService
    {
        public ArtistService(IArtistRepository repository, IMapper mapper) : base(repository, mapper)
        {
        }

        public override async Task CreateAsync(ArtistDTO artist)
        {
            var sameArtist = await _repository.GetAsync(a => a.Name.Equals(artist.Name));
            if (sameArtist.Any())
            {
                throw new RestException(HttpStatusCode.BadRequest, new { Name = "Artist with such name already exists" });
            }
            await _repository.CreateAsync(_mapper.Map<ArtistDTO, Artist>(artist));
        }
    }
}

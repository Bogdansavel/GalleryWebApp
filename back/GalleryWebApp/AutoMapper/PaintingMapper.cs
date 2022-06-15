using AutoMapper;
using GalleryWebApp.Models;
using GalleryWebApp.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalleryWebApp.AutoMapper
{
    public class PaintingMapper : Profile
    {
        public PaintingMapper()
        {
            CreateMap<Painting, PaintingDTO>()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(p => p.ArtistId, opt => opt.MapFrom(src => src.ArtistId))
                .ForMember(p => p.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(p => p.ImageName, opt => opt.MapFrom(src => src.ImageName));
            CreateMap<PaintingDTO, Painting>()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(p => p.ArtistId, opt => opt.MapFrom(src => src.ArtistId))
                .ForMember(p => p.Artist, opt => opt.MapFrom(src => src.Artist))
                .ForMember(p => p.Year, opt => opt.MapFrom(src => src.Year))
                .ForMember(p => p.ImageName, opt => opt.MapFrom(src => src.ImageName));

            CreateMap<Artist, ArtistDTO>()
                .ForMember(p => p.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(p => p.Name, opt => opt.MapFrom(src => src.Name));
            CreateMap<ArtistDTO, Artist>()
                .ForMember(a => a.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(a => a.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<Comment, CommentDTO>()
                .ForMember(c => c.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(c => c.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(c => c.User, opt => opt.MapFrom(src => src.User))
                .ForMember(c => c.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(c => c.Painting, opt => opt.MapFrom(src => src.Painting))
                .ForMember(c => c.PaintingId, opt => opt.MapFrom(src => src.PaintingId));
            CreateMap<CommentDTO, Comment>()
                .ForMember(c => c.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(c => c.Text, opt => opt.MapFrom(src => src.Text))
                .ForMember(c => c.User, opt => opt.MapFrom(src => src.User))
                .ForMember(c => c.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(c => c.Painting, opt => opt.MapFrom(src => src.Painting))
                .ForMember(c => c.PaintingId, opt => opt.MapFrom(src => src.PaintingId));

            CreateMap<Favorite, FavoriteDTO>()
                .ForMember(c => c.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(c => c.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(c => c.PaintingDTO, opt => opt.MapFrom(src => src.Painting))
                .ForMember(c => c.PaintingId, opt => opt.MapFrom(src => src.PaintingId));
            CreateMap<FavoriteDTO, Favorite>()
                .ForMember(c => c.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(c => c.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(c => c.Painting, opt => opt.MapFrom(src => src.PaintingDTO))
                .ForMember(c => c.PaintingId, opt => opt.MapFrom(src => src.PaintingId));
        }
    }
}

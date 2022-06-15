using GalleryWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GalleryWebApp.Data
{
    public class DataContext : IdentityDbContext<User>
    {
        public DbSet<Painting> Paintings { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        public DataContext(DbContextOptions options) : base(options) 
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var artist = new Artist() { Id = Guid.NewGuid(), Name = "Juan Álvarez Cebrián" };
            var painting = new Painting() { Id = Guid.NewGuid(), Name = "CAUTIVOS", Year = 2017, ArtistId = artist.Id };

            builder.Entity<Artist>().HasData(artist);
            builder.Entity<Painting>().HasData(painting);
        }
    }
}

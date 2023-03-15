using GaleriaT_API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GaleriaT_API
{
    public class GallerySeeder
    {
        private readonly GalleryDbContext _dbContext;
        private readonly IPasswordHasher<Admin> _passwordHasher;

        public GallerySeeder(GalleryDbContext dbContext, IPasswordHasher<Admin> passwordHasher)
        {
            _dbContext=dbContext;
            _passwordHasher = passwordHasher;
        }
        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                var pendingMigrations = _dbContext.Database.GetPendingMigrations();
                if (pendingMigrations != null && pendingMigrations.Any()) {

                    _dbContext.Database.Migrate();
            }

                if (!_dbContext.GalleryPosts.Any())
                {
                    var galleryPosts = GetGalleryPosts();
                    _dbContext.GalleryPosts.AddRange(galleryPosts);
                    _dbContext.SaveChanges();
                }
                if (!_dbContext.Admin.Any())
                {
                    var PASSWORD = "password";

                    var newAdmin = new Admin();
                

                    var hashedPassword = _passwordHasher.HashPassword(newAdmin, PASSWORD);
                    newAdmin.PasswordHash = hashedPassword;

                    _dbContext.Admin.Add(newAdmin);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<GalleryPost> GetGalleryPosts()
        {
            var galleryPosts = new List<GalleryPost>()
            {
            new GalleryPost()
            {
                Title = "Przyjładowy tytuł 1",
                ImageId = "img1",
                Text = "Przykładowy tekst 1",
                DateOfWork = DateTime.Now,
             //   CreatedDate= DateTime.Now,
            },
            new GalleryPost()
            {
                Title = "Przyjładowy tytuł 2",
                ImageId = "img2",
                Text = "Przykładowy tekst 2",
                DateOfWork = DateTime.Now,
               // CreatedDate= DateTime.Now,
            },
            new GalleryPost()
            {
                Title = "Przyjładowy tytuł 3",
                ImageId = "img3",
                Text = "Przykładowy tekst 3",
                DateOfWork = DateTime.Now,
              //  CreatedDate= DateTime.Now,
            }
            };
            return galleryPosts;
        }
   
    }
}

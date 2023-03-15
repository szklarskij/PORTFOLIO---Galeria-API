using Microsoft.EntityFrameworkCore;

namespace GaleriaT_API.Entities
{
    public class GalleryDbContext : DbContext
    {

        public GalleryDbContext(DbContextOptions<GalleryDbContext>options):base(options)
        {

        }
        public DbSet<GalleryPost> GalleryPosts { get; set; }
        public DbSet<Admin> Admin { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().Property(r => r.PasswordHash).IsRequired();

            modelBuilder.Entity<GalleryPost>().Property(r => r.Title).IsRequired();
          //  modelBuilder.Entity<GalleryPost>().Property(r => r.ImageUrl).IsRequired();
            modelBuilder.Entity<GalleryPost>().Property(r => r.DateOfWork).IsRequired();


        }
       
    }
}

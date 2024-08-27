using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace RepositoryLayer.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder = optionsBuilder.UseSqlServer("");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<ProviderPhoto> ProviderPhotos { get; set; }
    }
}

using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Url>().HasKey(x => x.Id);
            modelBuilder.Entity<Url>().HasMany(x => x.UrlMetrics);
            modelBuilder.Entity<UrlMetric>().HasKey(x => x.Id);
            modelBuilder.Entity<UrlMetric>().HasOne(x => x.Url);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Url> Urls { get; set; }
        public DbSet<UrlMetric> UrlMetrics { get; set; }
    }
}

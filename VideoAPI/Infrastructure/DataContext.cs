using Microsoft.EntityFrameworkCore;
using VideoAPI.app.models;

namespace VideoAPI.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Document> Document { get; set; }
        public DbSet<StreamingUrl> StreamingUrl { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.CustomNamingConvention();

            modelBuilder.Entity<DocumentStreamingUrl>()
            .HasKey(x => new { x.DocumentId, x.StreamingUrlId });

            modelBuilder.Entity<DocumentStreamingUrl>()
            .HasOne(d => d.Document)
            .WithMany(d => d.StreamingUrls)
            .HasForeignKey(d => d.DocumentId);

            modelBuilder.Entity<DocumentStreamingUrl>()
            .HasOne(d => d.StreamingUrl)
            .WithMany(d => d.Documents)
            .HasForeignKey(d => d.StreamingUrlId);

        }
    }
}
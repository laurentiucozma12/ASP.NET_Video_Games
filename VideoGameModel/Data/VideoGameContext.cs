using VideoGameModel.Models;
using Microsoft.EntityFrameworkCore;

namespace VideoGameModel.Data
{
    public class VideoGameContext : DbContext
    {
        public VideoGameContext(DbContextOptions<VideoGameContext> options) : base(options) { }
        public DbSet<Studio> Studios { get; set; }
        public DbSet<VideoGame> VideoGames { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<PublishedVideoGame> PublishedVideoGames { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Studio>().ToTable("Studios");
            modelBuilder.Entity<VideoGame>().ToTable("VideoGames");
            modelBuilder.Entity<Genre>().ToTable("Genres");
            modelBuilder.Entity<Platform>().ToTable("Platforms");
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<PublishedVideoGame>().ToTable("PublishedVideoGames");
            modelBuilder.Entity<PublishedVideoGame>().HasKey(c => new { c.VideoGameId, c.StudioId });
        }
    }
}

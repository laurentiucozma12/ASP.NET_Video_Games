using VideoGameModel.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace VideoGameModel
{
    public class DbContextFactory : IDesignTimeDbContextFactory<VideoGameContext>
    {
        public VideoGameContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<VideoGameContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=VideoGame;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");

            return new VideoGameContext(optionsBuilder.Options);
        }
    }
}
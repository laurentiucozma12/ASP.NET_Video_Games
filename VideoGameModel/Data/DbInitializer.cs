using VideoGameModel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace VideoGameModel.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new VideoGameContext(serviceProvider.GetRequiredService<DbContextOptions<VideoGameContext>>()))
            {
                if (context.VideoGames.Any())
                {
                    return;
                }

                var studios = new List<Studio>
                {
                    new Studio { Name = "Blizzard Entertainment" },
                    new Studio { Name = "Nintendo" },
                    new Studio { Name = "RITO Games" },
                    new Studio { Name = "Valve Corporation" },
                };

                var videoGames = new List<VideoGame>
                {
                    new VideoGame { Title = "World of Warcraft", Studio = studios[0], Price = Decimal.Parse("30") },
                    new VideoGame { Title = "Mario", Studio = studios[1], Price = Decimal.Parse("19") },
                    new VideoGame { Title = "League of Legends", Studio = studios[2], Price = Decimal.Parse("0") },
                    new VideoGame { Title = "Counter-Strike: Global Offensive", Studio = studios[3], Price = Decimal.Parse("15") },
                };

                foreach (VideoGame videoGame in videoGames)
                {
                    context.VideoGames.Add(videoGame);
                }

                context.SaveChanges();

                var genres = new List<Genre>
                {
                    new Genre { Name = "MMORPG", VideoGame = videoGames[0] },
                    new Genre { Name = "Fantasy", VideoGame = videoGames[0] },
                    new Genre { Name = "Platformer", VideoGame = videoGames[1] },
                    new Genre { Name = "Indie", VideoGame = videoGames[1] },
                    new Genre { Name = "MOBA", VideoGame = videoGames[2] },
                    new Genre { Name = "Strategy", VideoGame = videoGames[2] },
                    new Genre { Name = "Shooter", VideoGame = videoGames[3] },
                    new Genre { Name = "Action", VideoGame = videoGames[3] },
                };

                foreach (Genre genre in genres)
                {
                    context.Genres.Add(genre);
                }

                context.SaveChanges();

                var platforms = new List<Platform>
                {
                    new Platform { Name = "PC", VideoGame = videoGames[0] },
                    new Platform { Name = "PlayStation", VideoGame = videoGames[0] },
                    new Platform { Name = "Xbox", VideoGame = videoGames[0] },
                    new Platform { Name = "Nintentdo Switch", VideoGame = videoGames[1] },
                    new Platform { Name = "PC", VideoGame = videoGames[1] },
                    new Platform { Name = "PC", VideoGame = videoGames[2] },
                    new Platform { Name = "PC", VideoGame = videoGames[3] },
                };

                foreach (Platform platform in platforms)
                {
                    context.Platforms.Add(platform);
                }

                context.SaveChanges();
            }
        }
    }
}
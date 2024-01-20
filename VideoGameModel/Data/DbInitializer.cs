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

                context.SaveChanges();

                var genres = new List<Genre>
                {
                    new Genre { Name = "MMORPG" },
                    new Genre { Name = "Fantasy" },
                    new Genre { Name = "Platformer" },
                    new Genre { Name = "Indie" },
                    new Genre { Name = "MOBA" },
                    new Genre { Name = "Strategy" },
                    new Genre { Name = "Shooter" },
                    new Genre { Name = "Action" },
                };

                foreach (Genre genre in genres)
                {
                    context.Genres.Add(genre);
                }

                context.SaveChanges();

                var platforms = new List<Platform>
                {
                    new Platform { Name = "PC" },
                    new Platform { Name = "PlayStation" },
                    new Platform { Name = "Xbox" },
                    new Platform { Name = "Nintentdo Switch" },
                };

                foreach (Platform platform in platforms)
                {
                    context.Platforms.Add(platform);
                }

                var videoGames = new List<VideoGame>
                {
                    new VideoGame { Title = "World of Warcraft", Studio = studios[0], Genre = genres[0], Platform = platforms[0], Price = Decimal.Parse("30") },
                    new VideoGame { Title = "Mario", Studio = studios[1], Genre = genres[1], Platform = platforms[1], Price = Decimal.Parse("19") },
                    new VideoGame { Title = "League of Legends", Studio = studios[2], Genre = genres[2], Platform = platforms[2], Price = Decimal.Parse("0") },
                    new VideoGame { Title = "Counter-Strike: Global Offensive", Studio = studios[3], Genre = genres[3], Platform = platforms[3], Price = Decimal.Parse("15") },
                };

                foreach (VideoGame videoGame in videoGames)
                {
                    context.VideoGames.Add(videoGame);
                }

                context.SaveChanges();

                var customers = new List<Customer>
                {
                    new Customer { Name = "John", Address = "New York", BirthDate = DateTime.Parse("1987-02-06") },
                    new Customer { Name = "Obama", Address = "Loss Angeles", BirthDate = DateTime.Parse("1984-03-04") }
                };

                foreach (Customer customer in customers)
                {
                    context.Customers.Add(customer);
                }

                var orders = new List<Order>
                {
                    new Order { CustomerId = customers[0].Id, VideoGameId = videoGames[0].Id, OrderDate = DateTime.Parse("2024-01-01") },
                    new Order { CustomerId = customers[0].Id, VideoGameId = videoGames[1].Id, OrderDate = DateTime.Parse("2024-01-02") },
                    new Order { CustomerId = customers[1].Id, VideoGameId = videoGames[2].Id, OrderDate = DateTime.Parse("2024-01-03") }
                };

                foreach (Order order in orders)
                {
                    context.Orders.Add(order);
                }

                context.SaveChanges();
            }
        }
    }
}
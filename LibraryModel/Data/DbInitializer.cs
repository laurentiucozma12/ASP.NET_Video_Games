using LibraryModel.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

// I added this:
using Microsoft.Extensions.DependencyInjection;

namespace LibraryModel.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new LibraryContext(serviceProvider.GetRequiredService<DbContextOptions<LibraryContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }

                var authors = new List<Author>
                {
                    new Author { FirstName = "Mihail", LastName = "Sadoveanu" },
                    new Author { FirstName = "George", LastName = "Calinescu" },
                    new Author { FirstName = "Mircea", LastName = "Eliade" }
                };

                var books = new List<Book>
                {
                    new Book { Title = "Baltagul", Author = authors[0], Price = Decimal.Parse("22") },
                    new Book { Title = "Enigma Otiliei", Author = authors[1], Price = Decimal.Parse("18") },
                    new Book { Title = "Maytrei", Author = authors[2], Price = Decimal.Parse("27") }
                };

                foreach (Book book in books)
                {
                    context.Books.Add(book);
                }

                context.SaveChanges();

                var customers = new List<Customer>
                {
                    new Customer { Name = "Popescu Marcela", Address = "Str. Plopilor, nr. 24", BirthDate = DateTime.Parse("1979-09-01") },
                    new Customer { Name = "Mihailescu Cornel", Address = "Str. Bucuresti, nr. 45, ap. 2", BirthDate = DateTime.Parse("1969-07-08") }
                };

                foreach (Customer customer in customers)
                {
                    context.Customers.Add(customer);
                }

                context.SaveChanges();

                var publishers = new List<Publisher>
                {
                    new Publisher { PublisherName = "Humanitas", Address = "Str. Aviatorilor, nr. 40, Bucuresti" },
                    new Publisher { PublisherName = "Nemira", Address = "Str. Plopilor, nr. 35, Ploiesti" },
                    new Publisher { PublisherName = "Paralela 45", Address = "Str. Cascadelor, nr. 22, Cluj-Napoca" }
                };

                foreach (Publisher publisher in publishers)
                {
                    context.Publishers.Add(publisher);
                }

                context.SaveChanges();

                var orders = new List<Order>
                {
                    new Order { CustomerId = customers[0].Id, BookId = books[0].Id, OrderDate = DateTime.Parse("2021-02-25") },
                    new Order { CustomerId = customers[0].Id, BookId = books[1].Id, OrderDate = DateTime.Parse("2021-10-28") },
                    new Order { CustomerId = customers[1].Id, BookId = books[2].Id, OrderDate = DateTime.Parse("2021-09-28") }
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
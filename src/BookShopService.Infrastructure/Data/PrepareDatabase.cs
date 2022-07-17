using Microsoft.AspNetCore.Builder;
using BookShopService.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace BookShopService.Infrastructure.Data
{
    public static class PrepareDatabase
    {
        public static void Populate(IApplicationBuilder app, bool isProduction)
        {
            using( var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>(), isProduction);
            }
        }

        private static void SeedData( AppDbContext context, bool isProduction )
        {
            if(isProduction)
            {
                try
                {
                    context.Database.Migrate();
                }
                catch(Exception)
                {
                    Console.WriteLine("Failed to apply migrations");
                }
            }

            if (context != null && !context.Books.Any())
            {
                Console.WriteLine("Seeding data ...");
                context.AddRange(
                    new Book
                    {
                        Author = "A. A. Milne",
                        Title = "Winnie-the-Pooh",
                        Price = 19.25M
                    },
                    new Book
                    {
                        Author = "Jane Austin",
                        Title = "Pride and Prejudice",
                        Price = 5.49M
                    },
                    new Book
                    {
                        Author = "William Shakespeare",
                        Title = "Romeo and Juliet",
                        Price = 6.95M
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
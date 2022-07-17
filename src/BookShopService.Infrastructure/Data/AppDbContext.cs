using Microsoft.EntityFrameworkCore;
using BookShopService.Domain.Models;

namespace BookShopService.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Book>? Books { get; set; }
    }
}
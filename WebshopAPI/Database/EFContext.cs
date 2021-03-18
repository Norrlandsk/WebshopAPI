using Microsoft.EntityFrameworkCore;
using WebshopAPI.Models;

namespace WebshopAPI.Database
{
    public class EFContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<SoldBook> SoldBooks { get; set; }

        public EFContext(DbContextOptions<EFContext> options) : base(options)
        {
        }

        public EFContext() : base(new ContextFactory().CreateOptions())
        {
        }
    }
}
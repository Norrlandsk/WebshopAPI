using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WebshopAPI.Database
{
    public class ContextFactory : IDesignTimeDbContextFactory<EFContext>
    {
        private const string DatabaseName = "WebshopViktorSalmberg";

        public EFContext CreateDbContext(params string[] args)
        {
            return new EFContext(CreateOptions());
        }

        public DbContextOptions<EFContext> CreateOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder<EFContext>();
            optionsBuilder.UseSqlServer($@"Server =
.\SQLEXPRESS;Database={DatabaseName};trusted_connection=true");
            return optionsBuilder.Options;
        }
    }
}
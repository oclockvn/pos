using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using pos.infrastructure.data;

namespace pos.web
{
    public class TenantDbContextFactory : IDesignTimeDbContextFactory<TenantDbContext>
    {
        public TenantDbContext CreateDbContext(string[] args)
        {
            var configrationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", optional: true);

            var configuration = configrationBuilder.Build();
            var connectionString = configuration.GetConnectionString("TenantConnection");

            var dbContextOptionBuilder = new DbContextOptionsBuilder<TenantDbContext>();
            dbContextOptionBuilder.UseSqlServer(connectionString);

            return new TenantDbContext(dbContextOptionBuilder.Options);
        }
    }
}

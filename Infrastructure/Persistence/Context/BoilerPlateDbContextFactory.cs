using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.Context
{
    public class BoilerPlateDbContextFactory : IDesignTimeDbContextFactory<BoilerPlateDbContext>
    {
        public BoilerPlateDbContext CreateDbContext(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Presentation"))
            .AddJsonFile("appsettings.json")
            .Build();
            var optionsBuilder = new DbContextOptionsBuilder<BoilerPlateDbContext>();
            var connectionString = config.GetConnectionString("MyConnectionString");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            optionsBuilder.UseMySql(connectionString, serverVersion);

            return new BoilerPlateDbContext(optionsBuilder.Options);
        }
    }
}
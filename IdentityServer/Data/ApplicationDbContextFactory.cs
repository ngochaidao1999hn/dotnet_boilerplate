
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace IdentityServer.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connectionString = config.GetConnectionString("DefaultConnection");
            //var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            optionsBuilder.UseMySql(connectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
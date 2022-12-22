using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace Infrastructure.Persistence.Context
{
    public class BoilerPlateDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public BoilerPlateDbContext(DbContextOptions<BoilerPlateDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> Products { get; set; }
        public override int SaveChanges()
        {
            var now = DateTime.UtcNow;

            foreach (var changedEntity in ChangeTracker.Entries())
            {
                if (changedEntity.Entity is BaseEntity entity)
                {
                    switch (changedEntity.State)
                    {
                        case EntityState.Added:
                            entity.Created = now;
                            entity.Updated = now;
                            break;
                        case EntityState.Modified:
                            entity.Updated = now;
                            break;
                    }
                }
            }
            return base.SaveChanges();
        }
    }
}
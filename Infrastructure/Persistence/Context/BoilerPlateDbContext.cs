using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
            modelBuilder.Entity<ApplicationRole>().HasData(new ApplicationRole { Id = 1, Name = "admin", NormalizedName = "admin" });
            var hasher = new PasswordHasher<ApplicationUser>();
            modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = 1,
                UserName = "admin",
                NormalizedUserName = "admin",
                Email = "admin@fake.com",
                NormalizedEmail = "admin@fake.com",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Hai@123@"),
                SecurityStamp = string.Empty
            });
            modelBuilder.Entity<IdentityUserRole<int>>().HasData(new IdentityUserRole<int>
            {
                RoleId = 1,
                UserId = 1
            });

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
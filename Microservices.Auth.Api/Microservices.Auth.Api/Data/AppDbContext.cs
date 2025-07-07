//using Microservices.Auth.Api.Models;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;

//namespace Microservices.Auth.Api.Data
//{
//    public class AppDbContext : IdentityDbContext <ApplicationUser>
//    {
//        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) 
//        {

//        }
//        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            base.OnModelCreating(modelBuilder);
//        }
//    }
//}
using Microservices.Auth.Api.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Microservices.Auth.Api.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseMySql(
                        "Server=localhost;Database=AuthDB;User=sa;Password=12345678;",
                        new MySqlServerVersion(new Version(8, 0, 21)))
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones específicas para MySQL
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.Property(e => e.Id).HasMaxLength(255);
                entity.Property(e => e.UserName).HasMaxLength(255);
                entity.Property(e => e.NormalizedUserName).HasMaxLength(255);
                entity.Property(e => e.Email).HasMaxLength(255);
                entity.Property(e => e.NormalizedEmail).HasMaxLength(255);
            });
        }
    }
}
using Graduation_Project.Domain.Entity.RefreshTokenDomain;
using Graduation_Project.Domain.Entity.TrainerDomain;
using Graduation_Project.Domain.Entity.UserDomain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        public DbSet<RefreshToken> refreshTokens { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Trainer> trainers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}

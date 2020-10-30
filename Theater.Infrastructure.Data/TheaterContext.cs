using Microsoft.EntityFrameworkCore;
using Theater.Domain.Core.Models;
using Theater.Infrastructure.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Theater.Infrastructure.Data
{
    public class TheaterContext : IdentityDbContext<User>
    {
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Role> TheaterRoles { get; set; }
        public DbSet<ActorRole> ActorRoles { get; set; }
        public DbSet<Performance> Performances { get; set; }
        public DbSet<Poster> Posters { get; set; }

        public TheaterContext(DbContextOptions<TheaterContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ActorConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new ActorRoleConfiguration());
            modelBuilder.ApplyConfiguration(new PerformanceConfiguration());
            modelBuilder.ApplyConfiguration(new PosterConfiguration());
        }
    }
}

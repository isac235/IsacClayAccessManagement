namespace Data.Repository
{
    using Domain.Model;
    using Microsoft.EntityFrameworkCore;

    public class OfficesAccessDbContext : DbContext
    {
        public OfficesAccessDbContext(DbContextOptions<OfficesAccessDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Office> Offices { get; set; }

        public DbSet<Door> Doors { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Scope> Scopes { get; set; }

        public DbSet<UserRoleMapping> UserRoleMappings { get; set; }

        public DbSet<AccessEvent> AccessEvents { get; set; }

        public DbSet<Claim> Claims { get; set; }

        public DbSet<UserClaim> UserClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Add any custom configurations here
        }
    }
}

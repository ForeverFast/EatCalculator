using Microsoft.EntityFrameworkCore;
using Server.Core.Context.Entities.Identity;
using Server.Core.Context.Entities.UserData;

namespace Server.Core.Context
{
    internal sealed class ServerEatCalculatorDbContext : DbContext
    {
        #region Ctors

        public ServerEatCalculatorDbContext(DbContextOptions options) : base(options)
        {

        }

        #endregion

        #region Areas

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<UserEatData> UsersEatData { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.UserEatData).WithOne(ued => ued.User)
                .HasForeignKey<UserEatData>(u => u.Id);
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<UserEatData>().ToTable("Users");
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            ChangeTracker.Clear();

            return result;
        }
    }
}

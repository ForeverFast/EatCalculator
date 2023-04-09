using Microsoft.EntityFrameworkCore;

namespace Client.Core.Shared.Api.LocalDatabase.Context
{
    public class ClientEatCalculatorDbContext : DbContext
    {
        #region Ctors

        public ClientEatCalculatorDbContext(DbContextOptions options) : base(options)
        {

        }

        #endregion

        #region Areas

        public DbSet<Day> Days { get; set; } = null!;
        public DbSet<DayDateBind> DayDateBinds { get; set; } = null!;
        public DbSet<Meal> Meals { get; set; } = null!;
        public DbSet<Portion> Portions { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientEatCalculatorDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            ChangeTracker.Clear();

            return result;
        }
    }
}

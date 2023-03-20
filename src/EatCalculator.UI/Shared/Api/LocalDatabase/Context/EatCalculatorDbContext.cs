using EatCalculator.UI.Shared.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EatCalculator.UI.Shared.Api.LocalDatabase.Context
{
    public class EatCalculatorDbContext : DbContext
    {
        #region Ctors

        public EatCalculatorDbContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        #endregion

        #region Areas

        public DbSet<Day> Days { get; set; } = null!;
        public DbSet<Meal> Meals { get; set; } = null!;
        public DbSet<Portion> Portions { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EatCalculatorDbContext).Assembly);
        }   
    }
}

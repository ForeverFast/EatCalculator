using EatCalculator.UI.Shared.Api.LocalDatabase.Context;
using Microsoft.EntityFrameworkCore;

namespace Clients.Maui.Implementations
{
    internal sealed class MauiEatCalculatorDbContextFactory : IEatCalculatorDbContextFactory
    {
        #region Injects

        private readonly IDbContextFactory<EatCalculatorDbContext> _eatCalculatorDbContextFactory;

        #endregion

        #region Ctors

        public MauiEatCalculatorDbContextFactory(IDbContextFactory<EatCalculatorDbContext> eatCalculatorDbContextFactory)
            => _eatCalculatorDbContextFactory = eatCalculatorDbContextFactory;

        #endregion

        #region Fields

        private bool _init = false;

        #endregion

        public async Task<EatCalculatorDbContext> CreateContextAsync()
        {
            var dbContext = await _eatCalculatorDbContextFactory.CreateDbContextAsync();

            if (!_init)
            {
                await dbContext.Database.EnsureCreatedAsync();
                _init = true;
            }

            return dbContext;
        }
    }
}

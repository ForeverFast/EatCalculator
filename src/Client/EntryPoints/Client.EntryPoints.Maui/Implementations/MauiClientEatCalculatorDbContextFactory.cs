using Client.Core.Shared.Api.LocalDatabase.Context;
using Microsoft.EntityFrameworkCore;

namespace Client.EntryPoints.Maui.Implementations
{
    internal sealed class MauiClientEatCalculatorDbContextFactory : IClientEatCalculatorDbContextFactory
    {
        #region Injects

        private readonly IDbContextFactory<ClientEatCalculatorDbContext> _eatCalculatorDbContextFactory;

        #endregion

        #region Ctors

        public MauiClientEatCalculatorDbContextFactory(IDbContextFactory<ClientEatCalculatorDbContext> eatCalculatorDbContextFactory)
        {
            _eatCalculatorDbContextFactory = eatCalculatorDbContextFactory;
        }

        #endregion

        #region Fields

        private bool _init = false;

        #endregion

        public async Task<ClientEatCalculatorDbContext> CreateContextAsync()
        {
            var dbContext = await _eatCalculatorDbContextFactory.CreateDbContextAsync();

            if (!_init)
            {
                await dbContext.Database.MigrateAsync();
                _init = true;
            }

            return dbContext;
        }
    }
}

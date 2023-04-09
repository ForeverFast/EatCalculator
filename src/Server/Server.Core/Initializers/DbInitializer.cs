using Extensions.Hosting.AsyncInitialization;
using Microsoft.EntityFrameworkCore;
using Server.Core.Context;

namespace Server.Core.Initializers
{
    internal sealed class DbInitializer : IAsyncInitializer
    {
        #region Injects

        private readonly ServerEatCalculatorDbContext _dbContext;

        #endregion

        #region Ctors

        public DbInitializer(ServerEatCalculatorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        public Task InitializeAsync(CancellationToken cancellationToken)
            => _dbContext.Database.MigrateAsync(cancellationToken); 
    }
}

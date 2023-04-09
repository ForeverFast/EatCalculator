using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Server.Core.Context;

namespace Client.Core.Shared.Api.LocalDatabase.Context
{
    internal class ServerEatCalculatorDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ServerEatCalculatorDbContext>
    {
        public ServerEatCalculatorDbContext CreateDbContext(string[] args)
        {
            // Get connection string
            var optionsBuilder = new DbContextOptionsBuilder<ServerEatCalculatorDbContext>();

            var dbFilePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Context",
                "DesignTimeFiles",
                GlobalConstants.ServerDbFileName);

            optionsBuilder.UseSqlite($@"Data Source={dbFilePath};");

            return new ServerEatCalculatorDbContext(optionsBuilder.Options);
        }
    }
}

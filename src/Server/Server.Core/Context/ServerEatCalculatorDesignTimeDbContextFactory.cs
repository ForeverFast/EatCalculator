using Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Server.Core.Context
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

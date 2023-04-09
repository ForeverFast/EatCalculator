using Client.Core.Shared.Configs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Client.Core.Shared.Api.LocalDatabase.Context
{
    public class ClientEatCalculatorDesignTimeDbContextFactory : IDesignTimeDbContextFactory<ClientEatCalculatorDbContext>
    {
        public ClientEatCalculatorDbContext CreateDbContext(string[] args)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                ?? ClientGlobalConstants.Environment.ToString();

            // Build config
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
                .AddJsonFile("basesettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"basesettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var settings = config.GetSection(nameof(ClientEatCalculatorDbContextSettings)).Get<ClientEatCalculatorDbContextSettings>()
                ?? throw new NullReferenceException("Не найдена конфигурация базы данных");

            // Get connection string
            var optionsBuilder = new DbContextOptionsBuilder<ClientEatCalculatorDbContext>();

            var dbFilePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Shared",
                "Api",
                "LocalDatabase",
                "DesignTimeFiles",
                $"{settings.DbName}.db");

            optionsBuilder.UseSqlite($@"Data Source={dbFilePath};");

            return new ClientEatCalculatorDbContext(optionsBuilder.Options);
        }
    }
}

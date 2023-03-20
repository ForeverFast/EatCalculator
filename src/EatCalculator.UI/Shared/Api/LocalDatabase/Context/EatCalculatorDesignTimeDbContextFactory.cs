using EatCalculator.UI.Shared.Configs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EatCalculator.UI.Shared.Api.LocalDatabase.Context
{
    internal class EatCalculatorDesignTimeDbContextFactory : IDesignTimeDbContextFactory<EatCalculatorDbContext>
    {
        public EatCalculatorDbContext CreateDbContext(string[] args)
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                ?? GlobalConstants.Environment.ToString();

            // Build config
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
                .AddJsonFile("basesettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"basesettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var settings = config.GetSection(nameof(EatCalculatorDbContextSettings)).Get<EatCalculatorDbContextSettings>()
                ?? throw new NullReferenceException("Не найдена конфигурация базы данных");

            // Get connection string
            var optionsBuilder = new DbContextOptionsBuilder<EatCalculatorDbContext>();

            var dbFilePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Shared",
                "Api",
                "LocalDatabase",
                "DesignTimeFiles",
                $"{settings.DbName}.db");

            optionsBuilder.UseSqlite($@"Data Source={dbFilePath};");
            return new EatCalculatorDbContext(optionsBuilder.Options);
        }
    }
}

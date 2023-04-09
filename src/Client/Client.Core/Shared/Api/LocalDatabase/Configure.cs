using Client.Core.Shared.Api.LocalDatabase.Context;
using DALQueryChain.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Client.Core.Shared.Api.LocalDatabase
{
    internal static class Configure
    {
        public static ClientAppBuilder ConfigureDataAccessLayer(this ClientAppBuilder appBuilder)
        {
            var services = appBuilder.Services;
            var configuration = appBuilder.Configuration;

            services.Configure<ClientEatCalculatorDbContextSettings>(configuration.GetSection(nameof(ClientEatCalculatorDbContextSettings)));

            services.AddDbContextFactory<ClientEatCalculatorDbContext>((sp, options) =>
            {
                var settings = sp.GetRequiredService<IOptions<ClientEatCalculatorDbContextSettings>>().Value;
                var dbFilePathResolver = sp.GetRequiredService<IClientEatCalculatorDbContextPathResolver>();
                var dbFilePath = dbFilePathResolver.GetDbFilePath(settings.DbName);

                var connectionString = $"Data Source={dbFilePath}";

                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.UseSqlite(connectionString);
            });

            services.AddQueryChain(typeof(Configure).Assembly);

            return appBuilder;
        }
    }
}

using Client.Core.Shared.Api.LocalDatabase.Context;
using DALQueryChain.EntityFramework;

namespace Client.Core.Shared.Api.LocalDatabase
{
    internal static class Configure
    {
        public static ClientAppBuilder ConfigureDataAccessLayer(this ClientAppBuilder appBuilder)
        {
            var services = appBuilder.Services;
            var configuration = appBuilder.Configuration;

            services.Configure<ClientEatCalculatorDbContextSettings>(configuration.GetSection(nameof(ClientEatCalculatorDbContextSettings)));

            //services.AddDbContextFactory<ClientEatCalculatorDbContext>((sp, options) =>
            //{
            //    var dbFilePathResolver = sp.GetRequiredService<IClientEatCalculatorDbContextPathHelper>();
            //    var dbFilePath = dbFilePathResolver.GetDbFilePath();

            //    var connectionString = $"Data Source={dbFilePath}";

            //    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            //    options.UseSqlite(connectionString);
            //});

            services.AddScoped<IDalQcWrapper, DalQcWrapper>();

            //services.AddQueryChain(typeof(Configure).Assembly);

            return appBuilder;
        }
    }
}

using DALQueryChain.EntityFramework;
using EatCalculator.UI.Shared.Api.LocalDatabase.Context;
using EatCalculator.UI.Shared.Lib.AppBuilder;
using EatCalculator.UI.Shared.Lib.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MudBlazor.Services;
using System.Reflection;

namespace EatCalculator.UI.Shared
{
    internal static class Configure
    {
        public static ClientAppBuilder ConfigureSharedLayer(this ClientAppBuilder appBuilder)
        {
            var services = appBuilder.Services;
            var targetAssemblies = appBuilder.AdditionalAssemblies;
            var fullTargetAssemblies = targetAssemblies.Append(appBuilder.MainAssembly).ToArray();

            // UI

            services.AddMudServices();

            // Other

            services.AddValidators(fullTargetAssemblies);

            return appBuilder;
        }

        public static ClientAppBuilder ConfigureDataAccessLayer(this ClientAppBuilder appBuilder)
        {
            var services = appBuilder.Services;
            var configuration = appBuilder.Configuration;

            services.Configure<EatCalculatorDbContextSettings>(configuration.GetSection(nameof(EatCalculatorDbContextSettings)));

            services.AddDbContextFactory<EatCalculatorDbContext>((sp, options) =>
            {
                var settings = sp.GetRequiredService<IOptions<EatCalculatorDbContextSettings>>().Value;
                var dbFilePathResolver = sp.GetRequiredService<IEatCalculatorDbContextPathResolver>();
                var dbFilePath = dbFilePathResolver.GetDbFilePath(settings.DbName);

                var connectionString = $"Data Source={dbFilePath}";

                options.UseSqlite(connectionString);
            });

            services.AddQueryChain(typeof(Configure).Assembly);

            return appBuilder;
        }

        public static IServiceCollection AddValidators(this IServiceCollection services, params Assembly[] assemblies)
        {
            var types = assemblies
                .SelectMany(x => x.GetTypes())
                .Where(x
                    => !x.IsAbstract
                    && (x.BaseType?.IsGenericType ?? false)
                    && x.BaseType?.GetGenericTypeDefinition() == (typeof(BaseValidator<>)))
                .ToList();
            types.ForEach(item => services.AddScoped(item.BaseType!, item));

            return services;
        }
    }
}

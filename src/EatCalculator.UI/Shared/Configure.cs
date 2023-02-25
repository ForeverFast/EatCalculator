using EatCalculator.UI.Entities.Products.Models.Store;
using EatCalculator.UI.Shared.Lib.AppBuilder;
using EatCalculator.UI.Shared.Lib.EntityAdapter;
using EatCalculator.UI.Shared.Lib.Fluxor.Effects;
using EatCalculator.UI.Shared.Lib.Validation;
using Microsoft.Extensions.DependencyInjection;
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

            // Redux

            Adapters.Scan(fullTargetAssemblies);
            services.AddScoped<BaseEffectInjects>();
            services.AddFluxor(options =>
            {
                options.ScanAssemblies(appBuilder.MainAssembly, targetAssemblies);
                //options.WithLifetime(StoreLifetime.Singleton);
                if (appBuilder.IsDevelopment())
                    options.UseReduxDevTools();
            });

            services.AddScoped<ProductStateFacade>();

            // UI

            services.AddMudServices();

            // Other

            services.AddValidators(fullTargetAssemblies);



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

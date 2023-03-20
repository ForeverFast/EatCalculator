using EatCalculator.UI.Entities.Days.Models.Store;
using EatCalculator.UI.Entities.Meals.Models.Store;
using EatCalculator.UI.Entities.Products.Models.Store;
using EatCalculator.UI.Shared;
using EatCalculator.UI.Shared.Lib.AppBuilder;
using EatCalculator.UI.Shared.Lib.EntityAdapter;
using EatCalculator.UI.Shared.Lib.Fluxor.Effects;
using Microsoft.Extensions.DependencyInjection;

namespace EatCalculator.UI.App
{
    public static class Configure
    {
        public static ClientAppBuilder ConfigureAppLayer(this ClientAppBuilder appBuilder)
        {
            appBuilder.ConfigureSharedLayer();
            appBuilder.ConfigureDataAccessLayer();

            var services = appBuilder.Services;
            var targetAssemblies = appBuilder.AdditionalAssemblies;
            var fullTargetAssemblies = targetAssemblies.Append(appBuilder.MainAssembly).ToArray();

            // Redux

            Adapters.Scan(fullTargetAssemblies);
            services.AddScoped<BaseEffectInjects>();
            services.AddFluxor(options =>
            {
                options.ScanAssemblies(appBuilder.MainAssembly, targetAssemblies);
                if (appBuilder.IsDevelopment())
                    options.UseReduxDevTools();
            });

            services.AddScoped<DayStateFacade>();
            services.AddScoped<MealStateFacade>();
            services.AddScoped<ProductStateFacade>();

            return appBuilder;
        }
    }
}

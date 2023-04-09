using Client.Core.Entities.Days.Models.Store;
using Client.Core.Entities.Meals.Models.Store;
using Client.Core.Entities.Products.Models.Store;
using Client.Core.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Client.Core.App
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

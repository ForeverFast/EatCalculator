using Client.Core.Entities.Days.Models.Store;
using Client.Core.Entities.Meals.Models.Store;
using Client.Core.Entities.Products.Models.Store;
using Client.Core.Entities.Viewer.Models.Store;
using Client.Core.Shared;


namespace Client.Core.App
{
    public static class Configure
    {
        public static ClientAppBuilder ConfigureAppLayer(this ClientAppBuilder appBuilder)
        {
            appBuilder.ConfigureSharedLayer();

            // Flux

            Adapters.Scan(appBuilder.FullTargetAssemblies);
            appBuilder.Services.AddScoped<BaseEffectInjects>();
            appBuilder.Services.AddFluxor(options =>
            {
                options.ScanAssemblies(appBuilder.MainAssembly, appBuilder.AdditionalAssemblies);
                if (appBuilder.IsDevelopment())
                    options.UseReduxDevTools();
            });

            appBuilder.Services.AddScoped<DayStateFacade>();
            appBuilder.Services.AddScoped<MealStateFacade>();
            appBuilder.Services.AddScoped<ProductStateFacade>();
            appBuilder.Services.AddScoped<ViewerStateFacade>();

            return appBuilder;
        }
    }
}

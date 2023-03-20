using Clients.Maui.Implementations;
using EatCalculator.UI.App;
using EatCalculator.UI.Shared.Api.LocalDatabase.Context;
using EatCalculator.UI.Shared.Configs;
using EatCalculator.UI.Shared.Lib.AppBuilder;
using Microsoft.Extensions.Logging;

namespace Clients.Maui
{
    public static class MauiProgram
    {
        private const ClientAppEnvironment _environment = ClientAppEnvironment.Development;
        private const string _baseWebViewAddress = "https://0.0.0.0/";
        private static readonly ClientAppBuilderSettings _clientAppBuilderSettings = new()
        {
            Domain = new Uri(_baseWebViewAddress).Host,
            MainAssembly = MauiAppConfiguration.MainAssembly,
            AdditionalAssemblies = MauiAppConfiguration.TargetAssemblies
        };

        public static MauiApp CreateMauiApp(ClientAppPlatform platform)
        {
            GlobalConstants.Environment = _environment;
            MauiAppConfiguration.ClientAppConfiguration = MauiAppConfiguration.ClientAppConfiguration with
            {
                Platform = platform,
            };

            var defaultBuilder = MauiApp.CreateBuilder();
            defaultBuilder.UseMauiApp<App>();
            defaultBuilder.AddBaseConfiguration(_environment.ToString());

            // Blazor

            var builder = defaultBuilder.ToClientAppBuilder(
                platform,
                _environment,
                _baseWebViewAddress,
                _clientAppBuilderSettings);

            builder.Services.AddMauiBlazorWebView();
            if (builder.IsDevelopment())
            {
                defaultBuilder.Services.AddBlazorWebViewDeveloperTools();
                defaultBuilder.Logging.AddDebug();
            }

            builder.Services.AddSingleton<IEatCalculatorDbContextFactory, MauiEatCalculatorDbContextFactory>();
            builder.Services.AddSingleton<IEatCalculatorDbContextPathResolver, MauiEatCalculatorDbContextPathResolver>();

            builder.ConfigureAppLayer();

            // TODO: переделать на неблокирующий вызов
            builder.Services.AddScoped<EatCalculatorDbContext>(sp =>
            {
                var eatCalculatorDbContextFactory = sp.GetRequiredService<IEatCalculatorDbContextFactory>();
                return eatCalculatorDbContextFactory.CreateContextAsync().Result;
            });

            return defaultBuilder.Build();
        }
    }
}
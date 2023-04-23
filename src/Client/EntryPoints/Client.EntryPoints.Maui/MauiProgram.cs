using Client.Core.App;
using Client.Core.Shared.Api.LocalDatabase.Context;
using Client.Core.Shared.Configs;
using Client.EntryPoints.Maui.Implementations;
using Microsoft.Extensions.Logging;

namespace Client.EntryPoints.Maui
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
            ClientGlobalConstants.Environment = _environment;
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

            builder.Services.AddSingleton<IClientEatCalculatorDbContextFactory, MauiClientEatCalculatorDbContextFactory>();
            builder.Services.AddSingleton<IClientEatCalculatorDbContextPathHelper, MauiClientEatCalculatorDbContextPathHelper>();

            builder.ConfigureAppLayer();

            // TODO: переделать на неблокирующий вызов
            builder.Services.AddScoped<ClientEatCalculatorDbContext>(sp =>
            {
                var eatCalculatorDbContextFactory = sp.GetRequiredService<IClientEatCalculatorDbContextFactory>();
                return eatCalculatorDbContextFactory.CreateContextAsync().Result;
            });

            return defaultBuilder.Build();
        }
    }
}
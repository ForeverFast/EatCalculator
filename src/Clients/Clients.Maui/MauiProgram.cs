using EatCalculator.UI.App;
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

        public static MauiApp CreateMauiApp()
        {
            var defaultBuilder = MauiApp.CreateBuilder();
            defaultBuilder.UseMauiApp<App>();
            defaultBuilder.AddBaseConfiguration(_environment.ToString());

            // Blazor

            var builder = defaultBuilder.ToClientAppBuilder(
                _environment,
                _baseWebViewAddress,
                _clientAppBuilderSettings);

            builder.Services.AddMauiBlazorWebView();
            if (builder.IsDevelopment())
            {
                defaultBuilder.Services.AddBlazorWebViewDeveloperTools();
                defaultBuilder.Logging.AddDebug();
            }

            builder.ConfigureAppLayer();

            return defaultBuilder.Build();
        }
    }
}
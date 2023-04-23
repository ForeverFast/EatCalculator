using Blazored.LocalStorage;
using Client.Core.Shared.Api.HttpClient;
using Client.Core.Shared.Api.LocalDatabase;
using Client.Core.Shared.Lib.Calculator;
using Client.Core.Shared.Lib.FrameworkAbstractions;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace Client.Core.Shared
{
    internal static class Configure
    {
        public static ClientAppBuilder ConfigureSharedLayer(this ClientAppBuilder appBuilder)
        {
            appBuilder.ConfigureDataAccessLayer();
            appBuilder.ConfigureHttpClient();

            // Auth 

            appBuilder.Services
                .AddAuthorizationCore()
                .AddScoped<AuthenticationStateProvider, ClientAppAuthenticationStateProvider>();

            // UI

            appBuilder.Services.AddMudServices(opt =>
            {
                opt.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
            });

            // Other

            appBuilder.Services.AddBlazoredLocalStorage();
            appBuilder.Services.AddLocalization();
            appBuilder.Services.AddValidators(appBuilder.FullTargetAssemblies);

            appBuilder.Services.AddScoped<ICalculatorService, CalculatorService>();

            return appBuilder;
        }
    }
}

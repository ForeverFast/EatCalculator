using Client.Core.Shared.Api.LocalDatabase.Context;
using Client.Core.Shared.Lib.Calculator;
using DALQueryChain.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MudBlazor.Services;

namespace Client.Core.Shared
{
    internal static class Configure
    {
        public static ClientAppBuilder ConfigureSharedLayer(this ClientAppBuilder appBuilder)
        {
            var services = appBuilder.Services;
            var targetAssemblies = appBuilder.AdditionalAssemblies;
            var fullTargetAssemblies = targetAssemblies.Append(appBuilder.MainAssembly).ToArray();

            services.AddScoped<ICalculatorService, CalculatorService>();

            // UI

            services.AddMudServices(opt =>
            {
                opt.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
            });

            // Other

            services.AddValidators(fullTargetAssemblies);

            return appBuilder;
        }
    }
}

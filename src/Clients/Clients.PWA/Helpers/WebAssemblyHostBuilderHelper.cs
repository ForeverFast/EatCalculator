using EatCalculator.UI.Shared.Lib.AppBuilder;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Clients.PWA.Helpers
{
    public static class WebAssemblyHostBuilderHelper
    {
        public static ClientAppEnvironment GetEnvironment(this WebAssemblyHostBuilder webAssemblyHostBuilder)
            => webAssemblyHostBuilder switch
            {
                { } when webAssemblyHostBuilder.HostEnvironment.IsDevelopment() => ClientAppEnvironment.Development,
                { } when webAssemblyHostBuilder.HostEnvironment.IsStaging() => ClientAppEnvironment.Staging,
                { } when webAssemblyHostBuilder.HostEnvironment.IsProduction() => ClientAppEnvironment.Production,
                _ => throw new NotImplementedException(),
            };
    }
}

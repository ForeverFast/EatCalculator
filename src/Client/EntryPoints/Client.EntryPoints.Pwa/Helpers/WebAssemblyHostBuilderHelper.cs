using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using UI.Lib.AppBuilder;

namespace Client.EntryPoints.Pwa.Helpers
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

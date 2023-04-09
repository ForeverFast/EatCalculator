using Client.EntryPoints.Pwa.Helpers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration.Json;
using UI.Lib.AppBuilder;

namespace Client.EntryPoints.Pwa
{
    public static class Configure
    {
        public static async Task AddBaseConfiguration(this WebAssemblyHostBuilder app)
        {
            var configurationFiles = new string[]
            {
                $"_content/Client.Core/basesettings.json",
                $"_content/Client.Core/basesettings.{app.HostEnvironment.Environment}.json",
            };

            var httpClient = new HttpClient { BaseAddress = new Uri(app.HostEnvironment.BaseAddress) };

            foreach (var configurationFile in configurationFiles)
            {
                using var response = await httpClient.GetAsync(configurationFile);
                using var stream = await response.Content.ReadAsStreamAsync();
                app.Configuration.Add<JsonStreamConfigurationSource>(s => s.Stream = stream);
            }
        }

        public static ClientAppBuilder ToClientAppBuilder(this WebAssemblyHostBuilder builder,
                                                          ClientAppBuilderSettings clientAppBuilderSettings)

            => new ClientAppBuilder
            {
                Platform = ClientAppPlatform.Web,
                Environment = builder.GetEnvironment(),
                Domain = clientAppBuilderSettings.Domain,
                BaseAddress = builder.HostEnvironment.BaseAddress,
                Services = builder.Services,
                Configuration = builder.Configuration,
                ConfigurationBuilder = builder.Configuration,
                Logging = builder.Logging,
                MainAssembly = clientAppBuilderSettings.MainAssembly,
                AdditionalAssemblies = clientAppBuilderSettings.AdditionalAssemblies,
            };
    }
}

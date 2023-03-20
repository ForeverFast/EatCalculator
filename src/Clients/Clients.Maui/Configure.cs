using EatCalculator.UI.Shared.Lib.AppBuilder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Clients.Maui
{
    internal static class Configure
    {
        public static void AddBaseConfiguration(this MauiAppBuilder app, string environment)
        {
            var configurationFiles = new string[]
            {
                $"appsettings.json",
                $"appsettings.{environment}.json",
                $"_content/EatCalculator.UI/basesettings.json",
                $"_content/EatCalculator.UI/basesettings.{environment}.json",
            };

            foreach (var configurationFile in configurationFiles)
            {
                using var stream = FileSystem.OpenAppPackageFileAsync(Path.Combine("wwwroot", configurationFile)).Result;
                app.Configuration.Add<JsonStreamConfigurationSource>(s => s.Stream = stream);
            }
        }

        public static ClientAppBuilder ToClientAppBuilder(this MauiAppBuilder builder,
                                                          ClientAppPlatform platform,
                                                          ClientAppEnvironment environment,
                                                          string baseAddress,
                                                          ClientAppBuilderSettings clientAppBuilderSettings)
            => new ClientAppBuilder
            {
                Platform = platform,
                Environment = environment,
                Domain = clientAppBuilderSettings.Domain,
                BaseAddress = baseAddress,
                Services = builder.Services,
                Configuration = builder.Configuration,
                ConfigurationBuilder = builder.Configuration,
                Logging = builder.Logging,
                MainAssembly = clientAppBuilderSettings.MainAssembly,
                AdditionalAssemblies = clientAppBuilderSettings.AdditionalAssemblies,
            };
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace UI.Lib.AppBuilder
{
    public record ClientAppBuilder
    {
        public required ClientAppPlatform Platform { get; init; }
        public required ClientAppEnvironment Environment { get; init; }
        public required string Domain { get; init; }
        public required string BaseAddress { get; init; }
        public required IServiceCollection Services { get; init; }
        public required IConfiguration Configuration { get; init; }
        public required IConfigurationBuilder ConfigurationBuilder { get; init; }
        public required ILoggingBuilder Logging { get; init; }
        public required Assembly MainAssembly { get; init; }
        public required Assembly[] AdditionalAssemblies { get; init; }

        public Assembly[] FullTargetAssemblies
            => AdditionalAssemblies.Append(MainAssembly).ToArray();
    }
}

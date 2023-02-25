using System.Reflection;

namespace EatCalculator.UI.Shared.Lib.AppBuilder
{
    public record ClientAppBuilderSettings
    {
        public required string Domain { get; init; }
        public required Assembly MainAssembly { get; init; }
        public required Assembly[] AdditionalAssemblies { get; init; }
    }
}

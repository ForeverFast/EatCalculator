using System.Reflection;

namespace UI.Lib.AppBuilder
{
    public record ClientAppBuilderSettings
    {
        public required string Domain { get; init; }
        public required Assembly MainAssembly { get; init; }
        public required Assembly[] AdditionalAssemblies { get; init; }
    }
}

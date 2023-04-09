using Client.Core.App;
using Client.Core.App.Models;
using System.Reflection;

namespace Client.EntryPoints.Maui
{
    internal static class MauiAppConfiguration
    {
        internal static ClientAppConfiguration ClientAppConfiguration = new()
        {

        };

        internal static readonly Assembly MainAssembly = typeof(MauiProgram).Assembly;

        internal static readonly Assembly[] TargetAssemblies = new Assembly[]
        {
            typeof(ClientApp).Assembly,
        };

        internal static readonly Assembly[] TargetUIAssemblies = new Assembly[]
        {
            MainAssembly,
        };

        internal static readonly IDictionary<string, object?> ClientAppParameters = new Dictionary<string, object?>
        {
            { nameof(ClientApp.AppConfiguration), ClientAppConfiguration },
            { nameof(ClientApp.Assemblies), TargetUIAssemblies },
        };
    }
}

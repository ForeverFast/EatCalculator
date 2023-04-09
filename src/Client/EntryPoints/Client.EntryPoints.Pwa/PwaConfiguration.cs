using Client.Core.App;
using Client.Core.App.Models;
using System.Reflection;
using UI.Lib.AppBuilder;

namespace Client.EntryPoints.Pwa
{
    internal static class PWAConfiguration
    {
        internal static readonly ClientAppConfiguration ClientAppConfiguration = new()
        {
            Platform = ClientAppPlatform.Web
        };

        internal static readonly Assembly MainAssembly = typeof(Program).Assembly;

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

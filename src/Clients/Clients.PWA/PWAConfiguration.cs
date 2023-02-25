using EatCalculator.UI.App.Models;
using EatCalculator.UI.App;
using System.Reflection;

namespace Clients.PWA
{
    internal static class PWAConfiguration
    {
        internal static readonly ClientAppConfiguration ClientAppConfiguration = new()
        {
            IsWeb = true,
            IsDesktop = false,
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

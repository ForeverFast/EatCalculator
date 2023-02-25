using Clients.PWA;
using EatCalculator.UI.App;
using EatCalculator.UI.Shared.Lib.AppBuilder;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var defaultBuilder = WebAssemblyHostBuilder.CreateDefault(args);
defaultBuilder.RootComponents.Add<ClientApp>("#app");
defaultBuilder.RootComponents.Add<HeadOutlet>("head::after");
await defaultBuilder.AddBaseConfiguration();

var clientAppBuilderSettings = new ClientAppBuilderSettings
{
    Domain = new Uri(defaultBuilder.HostEnvironment.BaseAddress).Host,
    MainAssembly = PWAConfiguration.MainAssembly,
    AdditionalAssemblies = PWAConfiguration.TargetAssemblies
};

var builder = defaultBuilder.ToClientAppBuilder(clientAppBuilderSettings);

builder.ConfigureAppLayer();

await defaultBuilder.Build().RunAsync();

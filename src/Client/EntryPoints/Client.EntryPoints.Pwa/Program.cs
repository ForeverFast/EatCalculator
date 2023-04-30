using Client.Core.App;
using Client.Core.Shared.Api.LocalDatabase.Context;
using Client.EntryPoints.Pwa;
using Client.EntryPoints.Pwa.Implementations;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using UI.Lib.AppBuilder;

var defaultBuilder = WebAssemblyHostBuilder.CreateDefault(args);
defaultBuilder.RootComponents.Add((typeof(ClientApp)), "#app", ParameterView.FromDictionary(PwaConfiguration.ClientAppParameters));
defaultBuilder.RootComponents.Add<HeadOutlet>("head::after");
await defaultBuilder.AddBaseConfiguration();

var clientAppBuilderSettings = new ClientAppBuilderSettings
{
    Domain = new Uri(defaultBuilder.HostEnvironment.BaseAddress).Host,
    MainAssembly = PwaConfiguration.MainAssembly,
    AdditionalAssemblies = PwaConfiguration.TargetAssemblies
};

var builder = defaultBuilder.ToClientAppBuilder(clientAppBuilderSettings);

builder.Services.AddScoped<PwaClientEatCalculatorDbContextCacheSynchronizer>();
builder.Services.AddScoped<IClientEatCalculatorDbContextFileProvider, PwaClientEatCalculatorDbContextFileProvider>();

builder.ConfigureAppLayer();

await defaultBuilder.Build().RunAsync();
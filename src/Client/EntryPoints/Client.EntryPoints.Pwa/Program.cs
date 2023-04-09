using Client.Core.App;
using Client.Core.Shared.Api.LocalDatabase.Context;
using Client.EntryPoints.Pwa;
using Client.EntryPoints.Pwa.Implementations;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using UI.Lib.AppBuilder;

ClientEatCalculatorDbContext? GlobalEatCalculatorDbContext = null!;

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

builder.Services.AddSingleton<IClientEatCalculatorDbContextFactory, PwaClientEatCalculatorDbContextFactory>();
builder.Services.AddSingleton<IClientEatCalculatorDbContextPathResolver, PwaClientEatCalculatorDbContextPathResolver>();
builder.Services.AddSingleton<ClientEatCalculatorDbContext>((sp) => GlobalEatCalculatorDbContext);

builder.ConfigureAppLayer();



var app = defaultBuilder.Build();

var eatCalculatorDbContextFactory = app.Services.GetRequiredService<IClientEatCalculatorDbContextFactory>();
GlobalEatCalculatorDbContext = await eatCalculatorDbContextFactory.CreateContextAsync();

await app.RunAsync();
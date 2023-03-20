using Clients.Wasm;
using Clients.Wasm.Implementations;
using EatCalculator.UI.App;
using EatCalculator.UI.Shared.Api.LocalDatabase.Context;
using EatCalculator.UI.Shared.Lib.AppBuilder;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

EatCalculatorDbContext? GlobalEatCalculatorDbContext = null!;

var defaultBuilder = WebAssemblyHostBuilder.CreateDefault(args);
defaultBuilder.RootComponents.Add<ClientApp>("#app");
defaultBuilder.RootComponents.Add<HeadOutlet>("head::after");
await defaultBuilder.AddBaseConfiguration();

var clientAppBuilderSettings = new ClientAppBuilderSettings
{
    Domain = new Uri(defaultBuilder.HostEnvironment.BaseAddress).Host,
    MainAssembly = WasmConfiguration.MainAssembly,
    AdditionalAssemblies = WasmConfiguration.TargetAssemblies
};

var builder = defaultBuilder.ToClientAppBuilder(clientAppBuilderSettings);

builder.Services.AddSingleton<IEatCalculatorDbContextFactory, WasmEatCalculatorDbContextFactory>();
builder.Services.AddSingleton<EatCalculatorDbContext>((sp) => GlobalEatCalculatorDbContext);

builder.ConfigureAppLayer();



var app = defaultBuilder.Build();

var eatCalculatorDbContextFactory = app.Services.GetRequiredService<IEatCalculatorDbContextFactory>();
GlobalEatCalculatorDbContext = await eatCalculatorDbContextFactory.CreateContextAsync();

await app.RunAsync();

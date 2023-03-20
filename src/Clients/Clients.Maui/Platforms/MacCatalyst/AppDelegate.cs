using Foundation;

namespace Clients.Maui
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp(EatCalculator.UI.Shared.Lib.AppBuilder.ClientAppPlatform.MacCatalyst);
    }
}
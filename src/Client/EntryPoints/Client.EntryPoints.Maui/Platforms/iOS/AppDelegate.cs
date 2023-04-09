using Foundation;

namespace Client.EntryPoints.Maui
{
    [Register("AppDelegate")]
    public class AppDelegate : MauiUIApplicationDelegate
    {
        protected override MauiApp CreateMauiApp()
            => MauiProgram.CreateMauiApp(ClientAppPlatform.IOS);
    }
}
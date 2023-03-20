using Android.App;
using Android.Runtime;
using EatCalculator.UI.Shared.Lib.AppBuilder;

namespace Clients.Maui
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp(ClientAppPlatform.Android);
    }
}
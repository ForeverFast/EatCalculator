using EatCalculator.UI.Shared;
using EatCalculator.UI.Shared.Lib.AppBuilder;

namespace EatCalculator.UI.App
{
    public static class Configure
    {
        public static ClientAppBuilder ConfigureAppLayer(this ClientAppBuilder appBuilder)
        {
            appBuilder.ConfigureSharedLayer();

            //var services = appBuilder.Services;

            return appBuilder;
        }
    }
}

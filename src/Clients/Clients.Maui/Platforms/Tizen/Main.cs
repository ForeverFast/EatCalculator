using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using System;
using EatCalculator.UI.Shared.Lib.AppBuilder;

namespace Clients.Maui
{
    internal class Program : MauiApplication
    {
        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp(ClientAppPlatform.Tizen);

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using System;

namespace Client.EntryPoints.Maui
{
    internal class Program : MauiApplication
    {
        protected override MauiApp CreateMauiApp()
            => MauiProgram.CreateMauiApp(ClientAppPlatform.Tizen);

        static void Main(string[] args)
        {
            var app = new Program();
            app.Run(args);
        }
    }
}
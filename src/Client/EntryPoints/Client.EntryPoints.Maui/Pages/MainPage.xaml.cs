namespace Client.EntryPoints.Maui.Pages;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();

        BlazorWebViewRoot.Parameters = MauiAppConfiguration.ClientAppParameters;
    }
}
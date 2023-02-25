namespace Clients.Maui.Pages;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();

        BlazorWebViewRoot.Parameters = MauiAppConfiguration.ClientAppParameters;
    }
}
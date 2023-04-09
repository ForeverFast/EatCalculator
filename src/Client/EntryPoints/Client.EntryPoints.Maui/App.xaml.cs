using Client.EntryPoints.Maui.Pages;

namespace Client.EntryPoints.Maui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }
    }
}
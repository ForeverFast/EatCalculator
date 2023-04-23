using Client.Core.Shared.Configs;

namespace Client.Core.Shared.Lib
{
    internal static class NavigationManagerExtensions
    {
        public static void NavigateToIndexPage(this NavigationManager navigationManager)
            => navigationManager.NavigateTo($"{Routes.Main}");

        public static void NavigateToSignInPage(this NavigationManager navigationManager)
            => navigationManager.NavigateTo($"{Routes.Identity.BasePath}/{Routes.Identity.SignIn}");

        public static void NavigateToSignUpPage(this NavigationManager navigationManager)
            => navigationManager.NavigateTo($"{Routes.Identity.BasePath}/{Routes.Identity.SignUp}");

        public static void NavigateToProductsPage(this NavigationManager navigationManager)
            => navigationManager.NavigateTo($"{Routes.Products.BasePath}");

        public static void NavigateToDayPage(this NavigationManager navigationManager, int dayId)
            => navigationManager.NavigateTo($"{Routes.Days.BasePath}/{dayId}");
    }
}

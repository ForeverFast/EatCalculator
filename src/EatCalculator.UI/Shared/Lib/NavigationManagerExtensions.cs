using EatCalculator.UI.Shared.Configs;

namespace EatCalculator.UI.Shared.Lib
{
    internal static class NavigationManagerExtensions
    {
        public static void NavigateToIndexPage(this NavigationManager navigationManager)
            => navigationManager.NavigateTo($"{Routes.Main}");

        public static void NavigateToProductsPage(this NavigationManager navigationManager)
            => navigationManager.NavigateTo($"{Routes.Products.BasePath}");

        public static void NavigateToDayPage(this NavigationManager navigationManager, int dayId)
            => navigationManager.NavigateTo($"{Routes.Days.BasePath}/{dayId}");
    }
}

using EatCalculator.UI.Shared.Configs;

namespace EatCalculator.UI.Shared.Lib
{
    internal static class NavigationManagerExtensions
    {
        public static void NavigateToProductsPage(this NavigationManager navigationManager)
            => navigationManager.NavigateTo($"{Routes.Products.BasePath}");

        public static void NavigateToCreateProductPage(this NavigationManager navigationManager)
            => navigationManager.NavigateTo($"{Routes.Products.BasePath}/{Routes.Products.Create}");



        public static void NavigateToUpdateDayPage(this NavigationManager navigationManager, int dayId)
            => navigationManager.NavigateTo($"{Routes.Days.BasePath}/{dayId}/{Routes.Days.Update}");
    }
}

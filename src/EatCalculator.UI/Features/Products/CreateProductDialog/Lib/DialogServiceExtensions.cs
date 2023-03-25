using EatCalculator.UI.Shared.Configs;

namespace EatCalculator.UI.Features.Products.CreateProductDialog
{
    internal static class DialogServiceExtensions
    {
        public static IDialogReference OpenCreateProductDialog(this IDialogService dialogService)
            => dialogService.Show<CreateProductDialog>(
                "",
                DialogsConfig.GetDialogDefaultOptions());
    }
}

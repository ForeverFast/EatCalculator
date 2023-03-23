namespace EatCalculator.UI.Features.Products.CreateProductDialog.Lib
{
    internal static class DialogServiceExtensions
    {
        public static IDialogReference OpenCreateProductDialog(this IDialogService dialogService)
            => dialogService.Show<Components.CreateProductDialog>(
                "",
                Components.CreateProductDialog.DialogOptions);
    }
}

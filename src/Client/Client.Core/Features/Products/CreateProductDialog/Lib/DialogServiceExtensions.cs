using Client.Core.Shared.Configs;

namespace Client.Core.Features.Products.CreateProductDialog
{
    internal static class DialogServiceExtensions
    {
        public static IDialogReference OpenCreateProductDialog(this IDialogService dialogService)
            => dialogService.Show<CreateProductDialog>(
                "",
                DialogsConfig.GetDialogDefaultOptions());
    }
}

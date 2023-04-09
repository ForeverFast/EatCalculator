using Client.Core.Shared.Configs;

namespace Client.Core.Features.Products.UpdateProductDialog
{
    internal static class DialogServiceExtensions
    {
        public static IDialogReference OpenUpdateProductDialog(this IDialogService dialogService, Product product)
            => dialogService.Show<UpdateProductDialog>(
                "",
                new DialogParameters
                {
                    { nameof(UpdateProductDialog.Product), product },
                },
                DialogsConfig.GetDialogDefaultOptions());
    }
}

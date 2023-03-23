using EatCalculator.UI.Shared.Api.Models;

namespace EatCalculator.UI.Features.Products.UpdateProductDialog.Lib
{
    internal static class DialogServiceExtensions
    {
        public static IDialogReference OpenUpdateProductDialog(this IDialogService dialogService, Product product)
            => dialogService.Show<Components.UpdateProductDialog>(
                "",
                new DialogParameters
                {
                    { nameof(Components.UpdateProductDialog.Product), product },
                },
                Components.UpdateProductDialog.DialogOptions);
    }
}

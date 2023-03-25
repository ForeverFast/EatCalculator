using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Configs;

namespace EatCalculator.UI.Features.Meals.UpdateMealDialog
{
    internal static class DialogServiceExtensions
    {
        public static IDialogReference OpenUpdateMealDialog(this IDialogService dialogService, Meal meal)
            => dialogService.Show<UpdateMealDialog>(
                "",
                new DialogParameters
                {
                    { nameof(UpdateMealDialog.Meal), meal },
                },
                DialogsConfig.GetDialogDefaultOptions());
    }
}

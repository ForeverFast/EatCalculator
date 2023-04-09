using Client.Core.Shared.Configs;

namespace Client.Core.Features.Meals.UpdateMealDialog
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

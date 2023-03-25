using EatCalculator.UI.Shared.Configs;

namespace EatCalculator.UI.Features.Days.CreateDayDialog
{
    internal static class DialogServiceExtensions
    {
        public static IDialogReference OpenCreateDayDialog(this IDialogService dialogService)
            => dialogService.Show<CreateDayDialog>(
                "",
                DialogsConfig.GetDialogDefaultOptions());
    }
}

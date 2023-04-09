using Client.Core.Shared.Configs;

namespace Client.Core.Features.Days.CreateDayDialog
{
    internal static class DialogServiceExtensions
    {
        public static IDialogReference OpenCreateDayDialog(this IDialogService dialogService)
            => dialogService.Show<CreateDayDialog>(
                "",
                DialogsConfig.GetDialogDefaultOptions());
    }
}

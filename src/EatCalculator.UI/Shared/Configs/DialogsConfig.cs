namespace EatCalculator.UI.Shared.Configs
{
    internal static class DialogsConfig
    {
        public static DialogOptions GetDialogDefaultOptions()
            => new()
            {
                FullScreen = true,
                FullWidth = true,
                NoHeader = true,
                MaxWidth = MaxWidth.False
            };
    }
}

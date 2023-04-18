namespace Client.Core.App.Models.Store
{
    internal record InitializeAppAction
    {
        public required ClientAppPlatform Platform { get; init; }
    }
}

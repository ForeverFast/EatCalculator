namespace Client.Core.App.Models.Store
{
    internal record AppState
    {
        public ClientAppPlatform Platform { get; init; }

        public bool IsWeb
            => Platform == ClientAppPlatform.Web;
    }
}

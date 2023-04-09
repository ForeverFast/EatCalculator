namespace Client.Core.App.Models
{
    public sealed record ClientAppConfiguration
    {
        public ClientAppPlatform Platform { get; init; }
    }
}

namespace Client.Core.Shared.Api.HttpClient.Models
{
    internal record HttpEndpointsClientSettings
    {
        public required string BaseAddress { get; init; }
    }
}

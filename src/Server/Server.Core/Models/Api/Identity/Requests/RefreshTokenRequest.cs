using Server.Core.Base.Requests;

namespace Server.Core.Models.Api.Identity.Requests
{
    public record RefreshTokenRequest : BaseRequest
    {
        [FromBody] public required RefreshTokenRequestData Data { get; init; }
    }

    public record RefreshTokenRequestData
    {
        public required string AccessToken { get; init; }
        public required string RefreshToken { get; init; }
    }
}

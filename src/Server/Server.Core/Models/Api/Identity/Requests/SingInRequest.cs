using Server.Core.Base.Requests;

namespace Server.Core.Models.Api.Identity.Requests
{
    public record SignInRequest : BaseRequest
    {
        [FromBody] public required SignInRequestData Data { get; init; }
    }

    public record SignInRequestData
    {
        public required string Login { get; init; }
        public required string Password { get; init; }
    }
}

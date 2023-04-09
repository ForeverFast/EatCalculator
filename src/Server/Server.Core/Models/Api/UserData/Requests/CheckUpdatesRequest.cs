using Server.Core.Base.Requests;

namespace Server.Core.Models.Api.UserData.Requests
{
    public record CheckUpdatesRequest : BaseRequest
    {
        [FromQuery] public DateTime? LastUpdateDate { get; init; }
    }
}

using Microsoft.AspNetCore.Http;
using Server.Core.Base.Requests;

namespace Server.Core.Models.Api.UserData.Requests
{
    public record UploadUserEatDataRequest : BaseRequest
    {
        [FromForm(Name = "File")] public required IFormFile File { get; init; }
    }
}

using Server.Core.Models.Api.UserData.Requests;
using Server.Core.Models.Api.UserData.Responses;

namespace Server.Core.Interfaces.Services
{
    public interface IUserDataService
    {
        ValueTask<IResult<CheckUpdatesResponse>> CheckUpdatesAsync(CheckUpdatesRequest request, CancellationToken ctn);
        ValueTask<IResult<LoadUserEatDataResponse>> LoadUserEatDataAsync(LoadUserEatDataRequest request, CancellationToken ctn);
        ValueTask<IResult<UploadUserEatDataResponse>> UploadUserEatDataAsync(UploadUserEatDataRequest request, CancellationToken ctn);
    }
}

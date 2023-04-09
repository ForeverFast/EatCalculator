using Server.Core.Models.Api.UserData.Requests;
using Server.Core.Models.Api.UserData.Responses;

namespace Server.Core.Interfaces.Services
{
    public interface IUserDataService
    {
        ValueTask<CheckUpdatesResponse> CheckUpdatesAsync(CheckUpdatesRequest request, CancellationToken ctn);
        ValueTask<LoadUserEatDataResponse> LoadUserEatDataAsync(LoadUserEatDataRequest request, CancellationToken ctn);
        ValueTask<UploadUserEatDataResponse> UploadUserEatDataAsync(UploadUserEatDataRequest request, CancellationToken ctn);
    }
}

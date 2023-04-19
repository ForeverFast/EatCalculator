using Client.Core.Shared.Api.HttpClient.Models;
using Client.Core.Shared.Api.HttpClient.Requests.Identity;
using Client.Core.Shared.Api.HttpClient.Requests.UserData;
using Client.Core.Shared.Api.HttpClient.Responses.Identity;
using Client.Core.Shared.Api.HttpClient.Responses.UserData;
using Common;
using Common.Wrappers;
using RestSharp;

namespace Client.Core.Shared.Api.HttpClient.Endpoints
{
    internal class UserEatDataHttpEndpoints : EndpointNode
    {
        #region Ctors

        public UserEatDataHttpEndpoints(RestClient restClient) : base(restClient)
        {
        }

        #endregion

        private const string _controllerRoute = "/user-data";

        public Task<IResult<CheckUpdatesResponse>> CheckUpdatesAsync(CheckUpdatesRequest request, CancellationToken ctn = default)
        {
            var restRequest = new RestRequest($"{_controllerRoute}/check-updates");

            restRequest.AddParameter(nameof(request.LastUpdateDate), request.LastUpdateDate?.ToString());

            return _restClient.GetAsync<IResult<CheckUpdatesResponse>>(restRequest, ctn)!;
        }

        public Task<IResult<LoadUserEatDataResponse>> LoadUserEatDataAsync(LoadUserEatDataRequest _, CancellationToken ctn = default)
        {
            var restRequest = new RestRequest($"{_controllerRoute}/load-user-eat-data");

            return _restClient.GetAsync<IResult<LoadUserEatDataResponse>>(restRequest, ctn)!;
        }

        public Task<IResult<UploadUserEatDataResponse>> UploadUserEatDataAsync(UploadUserEatDataRequest request, CancellationToken ctn = default)
        {
            var restRequest = new RestRequest($"{_controllerRoute}/upload-user-eat-data");

            restRequest.AddFile("File", request.DbFileData, GlobalConstants.LocalUserDbFileName, "multipart/form-data");

            return _restClient.PostAsync<IResult<UploadUserEatDataResponse>>(restRequest, ctn)!;
        }
    }
}

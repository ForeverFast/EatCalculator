using Client.Core.Shared.Api.HttpClient.Models;
using Client.Core.Shared.Api.HttpClient.Requests.Identity;
using Client.Core.Shared.Api.HttpClient.Responses.Identity;
using Common.Extensions;
using Common.Wrappers;
using RestSharp;

namespace Client.Core.Shared.Api.HttpClient.Endpoints
{
    internal class AccountHttpEndpoints : EndpointNode
    {
        #region Ctors
        
        public AccountHttpEndpoints(RestClient restClient) : base(restClient)
        {
        }

        #endregion

        private const string _controllerRoute = "/account";

        public Task<IResult<SignInResponse>> SignInAsync(SignInRequest request, CancellationToken ctn = default)
        {
            var restRequest = new RestRequest($"{_controllerRoute}/sign-in", Method.Post);

            restRequest.AddBody(request.Body);

            return _restClient.ExecuteWithResultAsync<SignInResponse>(restRequest, ctn);
        }

        public Task<IResult<SignUpResponse>> SignUpAsync(SignUpRequest request, CancellationToken ctn = default)
        {
            var restRequest = new RestRequest($"{_controllerRoute}/sign-up", Method.Post);

            restRequest.AddBody(request.Body);

            return _restClient.ExecuteWithResultAsync<SignUpResponse>(restRequest, ctn);
        }
    }
}

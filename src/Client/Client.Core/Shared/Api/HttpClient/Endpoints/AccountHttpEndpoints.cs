using Client.Core.Shared.Api.HttpClient.Models;
using Client.Core.Shared.Api.HttpClient.Requests.Identity;
using Client.Core.Shared.Api.HttpClient.Responses.Identity;
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

        public Task<SignInResponse> SignInAsync(SignInRequest request, CancellationToken ctn = default)
        {
            var restRequest = new RestRequest($"{_controllerRoute}/sign-in");

            restRequest.AddBody(request.Body);

            return _restClient.PostAsync<SignInResponse>(restRequest, ctn)!;
        }

        public Task<SignUpResponse> SignUpAsync(SignUpRequest request, CancellationToken ctn = default)
        {
            var restRequest = new RestRequest($"{_controllerRoute}/sign-up", Method.Get);

            restRequest.AddBody(request.Body);

            return _restClient.PostAsync<SignUpResponse>(restRequest, ctn)!;
        }
    }
}

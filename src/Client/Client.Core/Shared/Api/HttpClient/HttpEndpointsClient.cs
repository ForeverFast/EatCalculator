using Client.Core.Shared.Api.HttpClient.Endpoints;
using RestSharp;
using RestSharp.Authenticators;

namespace Client.Core.Shared.Api.HttpClient
{
    internal class HttpEndpointsClient
    {
        #region Injects

        private readonly RestClient _restClient;

        #endregion

        #region Ctors

        public HttpEndpointsClient(
            RestClient restClient,
            AccountHttpEndpoints account,
            UserEatDataHttpEndpoints userEatData)
        {
            _restClient = restClient;

            Account = account;
            UserEatData = userEatData;
        }

        #endregion

        #region Areas

        public AccountHttpEndpoints Account { get; }
        public UserEatDataHttpEndpoints UserEatData { get; }

        #endregion

        #region Public methods
        public void SetToken(string? token)
            => _restClient.Authenticator = !string.IsNullOrEmpty(token) ? new JwtAuthenticator(token) : null;

        #endregion
    }
}

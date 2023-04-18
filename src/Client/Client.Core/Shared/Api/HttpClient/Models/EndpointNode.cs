using RestSharp;

namespace Client.Core.Shared.Api.HttpClient.Models
{
    internal class EndpointNode
    {
        #region Injects

        protected readonly RestClient _restClient;

        #endregion

        #region Ctors

        public EndpointNode(RestClient restClient)
        {
            _restClient = restClient;
        }

        #endregion
    }
}

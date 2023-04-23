using Blazored.LocalStorage;
using Client.Core.Shared.Api.HttpClient;
using Client.Core.Shared.Configs;
using Common.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Client.Core.Shared.Lib.FrameworkAbstractions
{
    internal class ClientAppAuthenticationStateProvider : AuthenticationStateProvider
    {
        #region Injects

        private readonly HttpEndpointsClient _httpEndpointsClient;
        private readonly ILocalStorageService _localStorage;

        #endregion

        #region Ctors

        public ClientAppAuthenticationStateProvider(
            HttpEndpointsClient httpEndpointsClient,
            ILocalStorageService localStorage)
        {
            _httpEndpointsClient = httpEndpointsClient;
            _localStorage = localStorage;
        }

        #endregion

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedToken = await _localStorage.GetItemAsync<string>(LocalStorageKeys.AccessToken);
            _httpEndpointsClient.SetToken(savedToken);

            var claimsPrincipal = new ClaimsPrincipal(string.IsNullOrWhiteSpace(savedToken)
                ? new ClaimsIdentity()
                : new ClaimsIdentity(IdentityHelper.ParseClaimsFromJwt(savedToken), "jwt"));

            var newAuthState = new AuthenticationState(claimsPrincipal);

            NotifyAuthenticationStateChanged(Task.FromResult(newAuthState));

            return newAuthState;
        }
    }
}

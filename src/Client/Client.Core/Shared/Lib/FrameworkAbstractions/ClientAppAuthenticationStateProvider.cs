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

        public void MarkUserAsAuthenticated(string userName)
        {
            var authenticatedUser = new ClaimsPrincipal(
                new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userName)
                }, "apiauth"));

            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));

            NotifyAuthenticationStateChanged(authState);
        }

        public void MarkUserAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));

            NotifyAuthenticationStateChanged(authState);
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var savedToken = await _localStorage.GetItemAsync<string>(LocalStorageKeys.AccessToken);
            _httpEndpointsClient.SetToken(savedToken);

            if (string.IsNullOrWhiteSpace(savedToken))
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(IdentityHelper.ParseClaimsFromJwt(savedToken), "jwt")));
        }
    }
}

using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Core.Shared.Lib.FrameworkAbstractions
{
    internal class ClientAppAuthenticationStateProviderWrapper
    {
        #region Injects

        private readonly ClientAppAuthenticationStateProvider _authenticationStateProvider;

        #endregion

        public ClientAppAuthenticationStateProviderWrapper(AuthenticationStateProvider authenticationStateProvider)
        {
            _authenticationStateProvider = (ClientAppAuthenticationStateProvider)authenticationStateProvider;
        }

        public void MarkUserAsAuthenticated(string userName)
            => _authenticationStateProvider.MarkUserAsAuthenticated(userName);

        public void MarkUserAsLoggedOut()
            => _authenticationStateProvider.MarkUserAsLoggedOut();

        public Task<AuthenticationState> GetAuthenticationStateAsync()
            => _authenticationStateProvider.GetAuthenticationStateAsync();
    }
}

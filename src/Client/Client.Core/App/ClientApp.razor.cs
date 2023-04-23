using Client.Core.App.Models;
using Client.Core.App.Models.Store;
using Client.Core.Entities.Viewer.Models.Store;
using Client.Core.Shared.Configs;
using Microsoft.AspNetCore.Components.Authorization;
using System.Reflection;
using System.Text.Encodings.Web;

namespace Client.Core.App
{
    public partial class ClientApp
    {
        #region Params

        [Parameter] public required ClientAppConfiguration AppConfiguration { get; set; }
        [Parameter] public Assembly[]? Assemblies { get; set; }
        [Parameter] public Type LayoutComponent { get; set; } = typeof(ClientAppLayout);

        #endregion

        #region Injects

        [Inject] IDispatcher _dispatcher { get; init; } = null!;
        [Inject] NavigationManager _navigationManager { get; init; } = null!;
        [Inject] ViewerStateFacade _viewerStateFacade { get; init; } = null!;
        [Inject] AuthenticationStateProvider _authenticationStateProvider { get; init; } = null!;

        #endregion

        #region LC Methods

        protected override void OnInitialized()
        {
            base.OnInitialized();

            _authenticationStateProvider.AuthenticationStateChanged += OnAuthenticationStateChanged;

            _dispatcher.Dispatch(new InitializeAppAction
            {
                Platform = AppConfiguration.Platform,
            });
        }

        #endregion

        #region External events

        private void OnAuthenticationStateChanged(Task<AuthenticationState> authenticationStateTask)
            => _viewerStateFacade.InitializeViewer(authenticationStateTask);

        #endregion

        #region Private methods

        private string GetLoginUri()
            => _navigationManager.GetUriWithQueryParameters($"{Routes.Identity.BasePath}/{Routes.Identity.SignIn}", new Dictionary<string, object?>
            {
                ["RedirectUri"] = $"{UrlEncoder.Default.Encode(_navigationManager.Uri)}",
            });

        #endregion
    }
}

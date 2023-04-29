using Client.Core.Entities.Days.Models.Store;
using Client.Core.Entities.Viewer.Models;
using Client.Core.Entities.Viewer.Models.Store;
using Client.Core.Shared.Configs;
using Client.Core.Shared.Lib;
using Client.Core.Shared.Lib.BaseComponents;

namespace Client.Core.Pages.Identity
{
    [Route($"{Routes.Identity.BasePath}/{Routes.Identity.UserProfile}")]
    public partial class UserProfilePage : BasePageComponent
    {
        #region Injects

        [Inject] ViewerStateFacade _viewerStateFacade { get; init; } = null!;

        #endregion

        #region Selectors

        private ISelectorSubscription<ViewerModel?> _viewer
            => _viewerStateFacade.Viewer;

        #endregion

        #region UI Fields

        private LoadingState _loadingState = LoadingState.Loading;

        #endregion

        #region LC Methods

        protected override void OnInitialized()
        {
            base.OnInitialized();

            _loadingState = LoadingState.Content;
        }

        #endregion
    }
}
